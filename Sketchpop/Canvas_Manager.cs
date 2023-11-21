using System;
using System.Drawing;
using SkiaSharp;
using System.Collections.Concurrent;
using System.Windows.Forms;
using SkiaSharp.Views.Desktop;
using System.IO;
using SkiaSharp.Views.WPF;
using System.Collections.Generic;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        private SketchPopTool _current_tool = SketchPopTool.brush;
        private SketchPopTool previous_tool { get; set; } = SketchPopTool.brush; // used for getting off of hand tool or move_pasted tool
        public SketchPopTool current_tool
        {
            get { return _current_tool; }
            set
            {
                if (current_tool == SketchPopTool.move_pasted && paste_manager.pasting)
                {
                    paste_manager.Commit_Pasted(layer_manager);
                }

                if (current_tool != SketchPopTool.move_pasted && current_tool != SketchPopTool.hand)
                    previous_tool = current_tool;

                _current_tool = value;
            }
        }

        private bool _use_pressure = false;
        public bool use_pressure { get { return _use_pressure; }  
            set
            {
                brush_manager.Get_Current_Brush().Reset_Pressurized_Stroke();
                _use_pressure = value;
            }
        }
        private uint _pressure;
        public uint pressure { private get { return _pressure; } 
            set
            {
                if (use_pressure)
                {
                    _pressure = value;
                }
                else
                {
                    _pressure = 0;
                }
            }
        }


        public SKImageInfo canvas_info;
        public Layer_Manager layer_manager;
        public Point hand_difference = new Point(0, 28);
        public Point middle_drawing_start = new Point(0, 0);
        public float stored_scale = 1;
        private Brush_Manager brush_manager;
        private Selection_Manager selection_manager;
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private Point hand_start = new Point(-1, -1);
        private Point prevMousePosition = new Point(0, 0);
        private Paste_Manager paste_manager;
        private main_window main_window;
        // when enter the undo process, set it true
        private bool is_undo = false;
        public Operation_Manager operation_manager;
        public Canvas_Manager()
        {
            brush_manager = new Brush_Manager();
            layer_manager = new Layer_Manager();
            selection_manager = new Selection_Manager();
            paste_manager = new Paste_Manager();
            operation_manager = new Operation_Manager();
            //picture_box = canvas_frame;
            canvas_info = new SKImageInfo(750, 750);


            Reset_Canvas_State();
        }

        public void Set_Main_Window(main_window main_window)
        {
            this.main_window = main_window;
        }



        // used for the fill tool, fills an area of similar color with a different color
        public void fill(Point click_point, SKColor replacement_color)
        {
            if (layer_manager.get_image(layer_manager.selected_layer).Info.Rect.Contains(click_point.X, click_point.Y))
            {
                using (SKBitmap replacement_image = SKBitmap.FromImage(layer_manager.get_image(layer_manager.selected_layer)))
                {
                    SKColor[] color_array = replacement_image.Pixels;
                    SKColor initial_color = replacement_image.GetPixel(click_point.X, click_point.Y);
                    Queue<Point> points_to_check = new Queue<Point>();
                    points_to_check.Enqueue(click_point);
                    while (points_to_check.Count >= 1)
                    {
                        Point p = points_to_check.Dequeue();
                        SKColor color_to_check = color_array[p.X + (p.Y * replacement_image.Info.Width)];
                        int difference = Math.Abs(color_to_check.Red - initial_color.Red);
                        difference += Math.Abs(color_to_check.Green - initial_color.Green);
                        difference += Math.Abs(color_to_check.Blue - initial_color.Blue);
                        if (difference <= 80 && color_to_check != replacement_color)
                        {
                            color_array[p.X + (p.Y * replacement_image.Info.Width)] = replacement_color;
                            Point left = new Point(p.X - 1, p.Y);
                            if (p.X - 1 >= 0)
                            {
                                points_to_check.Enqueue(left);
                            }
                            Point up = new Point(p.X, p.Y - 1);
                            if (p.Y - 1 >= 0)
                            {
                                points_to_check.Enqueue(up);
                            }
                            Point right = new Point(p.X + 1, p.Y);
                            if (p.X + 1 <= canvas_info.Width - 1)
                            {
                                points_to_check.Enqueue(right);
                            }
                            Point down = new Point(p.X, p.Y + 1);
                            if (p.Y + 1 <= canvas_info.Height - 1)
                            {
                                points_to_check.Enqueue(down);
                            }
                        }
                    }
                    replacement_image.Pixels = color_array;
                    layer_manager.set_image(layer_manager.selected_layer, SKImage.FromBitmap(replacement_image));
                }
            }
        }

        // called when the mouse is pressed down, invokes the current tool
        public void Mouse_Down_Handler(Point click_position)
        {

            Point adjusted = Adjust_Point_To_Hand(click_position);

            if (current_tool == SketchPopTool.brush)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;

                Begin_Draw_Path(adjusted);
            }
            else if (current_tool == SketchPopTool.selector_rect)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;
                selection_manager.Begin_Selection(adjusted, Selection_Manager.Selection_Tools.Rectangle);
            }
            else if (current_tool == SketchPopTool.selector_lasso)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;
                selection_manager.Begin_Selection(adjusted, Selection_Manager.Selection_Tools.Lasso);
            }
            else if (current_tool == SketchPopTool.hand)
            {
                hand_start = click_position;
                //hand_difference = new Point(0, 0);
            }
            else if (current_tool == SketchPopTool.move_pasted)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;
                paste_manager.start_moving(adjusted);
            }
            else if (current_tool == SketchPopTool.fill)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;
                fill(adjusted, brush_manager.Get_Current_Brush().Color());
            }
            // if the canvas changed during the undo process and then the users draw somthing on the canvas
            // we need to save the present layer into the stack
            if (is_undo)
            {
                operation_manager.clear_redo_stack();
                is_undo = false;
            }
        }


        // called when the mouse is held down, invokes the current tool
        public void Mouse_Is_Still_Down_Handler(Point point)
        {
            Point adjusted = Adjust_Point_To_Hand(point);

            if (current_tool == SketchPopTool.brush)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;

                Add_Point_To_Draw(adjusted);
            }
            else if (current_tool == SketchPopTool.selector_rect || current_tool == SketchPopTool.selector_lasso)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;

                SKImageInfo info = layer_manager.get_image(layer_manager.selected_layer).Info;
                selection_manager.Continue_Selection(adjusted, info.Width, info.Height);
            }
            else if (current_tool == SketchPopTool.hand)
            {
                hand_difference = new Point(hand_difference.X - (hand_start.X - point.X), hand_difference.Y - (hand_start.Y - point.Y));
                hand_start = point;
                //Console.WriteLine(hand_difference);
            }
            else if (current_tool == SketchPopTool.move_pasted)
            {
                if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                    return;
                paste_manager.move(adjusted);
            }
        }


        // projects the given point onto the visible adjusted canvas
        public Point Adjust_Point_To_Hand(Point point)
        {
            return new Point(
                (int)((point.X - hand_difference.X - middle_drawing_start.X) / stored_scale),
                (int)((point.Y - (hand_difference.Y - 28) - middle_drawing_start.Y) / stored_scale)
                ); // 28 is the size of the topstrip maybe should fix this later
        }


        // called when the mouse is released, finishes invoking tools
        internal void Mouse_Up_Handler(Point click_position)
        {
            Point adjusted = Adjust_Point_To_Hand(click_position);

            if (current_tool == SketchPopTool.selector_rect || current_tool == SketchPopTool.selector_lasso)
            {
                SKImageInfo info = layer_manager.get_image(layer_manager.selected_layer).Info;
                selection_manager.End_Selection(info.Width, info.Height);
            }
            else if (current_tool == SketchPopTool.move_pasted)
            {
                paste_manager.stop_moving(adjusted);
            }
            Program.canvas_manager.operation_manager.save_snapshot_into_stack(Program.canvas_manager.layer_manager.selected_layer, Program.canvas_manager.layer_manager.get_layer_opacity(Program.canvas_manager.layer_manager.selected_layer), Program.canvas_manager.layer_manager.get_image(Program.canvas_manager.layer_manager.selected_layer).Encode().ToArray());
        }


        // exit out of selection mode
        internal void Esc_Handler()
        {
            if (current_tool == SketchPopTool.hand || current_tool == SketchPopTool.move_pasted)
            {
                current_tool = previous_tool;
            }
            
            if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
            {
                selection_manager.Reset_Selection();
            }

            if (current_tool == SketchPopTool.move_pasted)
            {
                paste_manager.Commit_Pasted(layer_manager);
                current_tool = previous_tool;
            }
        }


        // start the skiasharp path to draw for a mouse event
        public void Begin_Draw_Path(Point point)
        {
            current_path = new SKPath();
            current_path.FillType = SKPathFillType.EvenOdd;

            pointsToDraw.Enqueue(new Point_Operation(point, Point_Operation.OperationType.jump));
        }


        // keeps track of all the registered inputs from the mouse while a line is beign drawn
        public void Add_Point_To_Draw(Point point)
        {
            pointsToDraw.Enqueue(new Point_Operation(point, Point_Operation.OperationType.line_to));
        }


        // changes the currently used brush color
        public void Update_Color(byte red, byte green, byte blue, byte alpha)
        {
            brush_manager.Get_Current_Brush().Set_Color(new SKColor(red, green, blue, alpha));
        }


        // change the currently used brush size
        public void Update_Stroke_Size(int size)
        {
            brush_manager.Get_Current_Brush().Set_Stroke(size);
        }


        /// <summary>
        /// Draws the surface onto a Bitmap
        /// </summary>
        /// <param name = "surface" ></ param >
        /// < returns ></ returns >
        public void Draw_Bitmap(PictureBox target, int layer = -1, bool resize = false, bool scale_bool = false,  float scale = 1)
        {
            if (layer_manager.count == 0)
                return;

            using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).Info))
            using (SKPaint paint = new SKPaint())
            {
                if (layer == -1)
                    for (int idx = 0; idx < layer_manager.count; idx++)
                    {
                        SKImage c_image = layer_manager.get_image(idx);
                        paint.Color = paint.Color.WithAlpha((byte)(0xFF * layer_manager.get_layer_opacity(idx)));
                        surface.Canvas.DrawImage(c_image, c_image.Info.Rect, paint);

                        if (paste_manager.pasting && idx == layer_manager.selected_layer)
                        {
                            paste_manager.Draw_Pasted_Temporarily(surface, true, paint);
                        }
                    }
                else
                {
                    SKImage c_image = layer_manager.get_image(layer);
                    paint.Color = paint.Color.WithAlpha((byte)(0xFF * layer_manager.get_layer_opacity(layer)));
                    surface.Canvas.DrawImage(c_image, c_image.Info.Rect, paint);

                    if (paste_manager.pasting && layer == layer_manager.selected_layer)
                    {
                        paste_manager.Draw_Pasted_Temporarily(surface, true, paint);
                    }
                }

                if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
                {
                    selection_manager.Draw_Outline(surface, paint);
                }

                
                if (target.Image != null)
                    target.Image.Dispose();
                SKImage t_image = surface.Snapshot();


                SKBitmap t_bitmap = SKBitmap.FromImage(t_image);

                if (resize)
                {
                    SKSizeI target_size = new SKSizeI(target.Width, target.Height);
                    SKBitmap c_bitmap = Resize_Canvas_To_Preview(t_bitmap, target_size);//t_bitmap.Resize(target_size, SKFilterQuality.High);
                    t_bitmap.Dispose();
                    t_bitmap = c_bitmap;
                }

                if (scale_bool)
                {
                    //Console.WriteLine(scale);
                    SKBitmap c_t_bitmap = t_bitmap.Resize(new SKImageInfo((int)(t_bitmap.Width * scale), (int)(t_bitmap.Height * scale)), SKFilterQuality.High);
                    t_bitmap.Dispose();
                    t_bitmap = c_t_bitmap;
                }


                target.Image = t_bitmap.ToBitmap();
                t_image.Dispose();
                t_bitmap.Dispose();

            }
        }


        /// <summary>
        /// Resizes the Bitmap to fit the target PictureBox by calculating the ratios
        /// between the SKBitmap and SKSizeI and applies the smaller ratio to the new
        /// size of the bitmap. The smaller size is chosen so that the bitmap does not 
        /// resize to be larger than the SKSizeI.
        /// 
        /// Code Source: https://stackoverflow.com/questions/1940581/c-sharp-image-resizing-to-different-size-while-preserving-aspect-ratio
        /// </summary>
        /// <param name="t_bitmap">original bitmap</param>
        /// <param name="targetSize">target size of PictureBox</param>
        /// <returns>resized bitmap</returns>
        private SKBitmap Resize_Canvas_To_Preview(SKBitmap t_bitmap, SKSizeI targetSize)
        {
            float ratioX = targetSize.Width / (float)t_bitmap.Width;
            float ratioY = targetSize.Height / (float)t_bitmap.Height;

            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(t_bitmap.Width * ratio);
            int newHeight = (int)(t_bitmap.Height * ratio);

            return t_bitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);
        }


        public void Draw_Path_Points(object state)
        {
            if (layer_manager.count == 0)
                return;

            if (current_path != null)
            {
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
                {
                    if (use_pressure)
                    {
                        brush_manager.Get_Current_Brush().Set_Pressurized_Stroke((int)pressure);
                    }
                    foreach (Point_Operation po in pointsToDraw)
                    {
                        pointsToDraw.TryDequeue(out _);
                        if (po.operationType == Point_Operation.OperationType.line_to)
                        {
                            current_path.LineTo(po.point.X, po.point.Y);
                            if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
                            {
                                surface.Canvas.ClipRegion(selection_manager.selected_area);
                            }
                        }
                        else if (po.operationType == Point_Operation.OperationType.jump)
                        {
                            current_path.MoveTo(po.point.X, po.point.Y);
                        }
                    }
                    surface.Canvas.DrawPath(current_path, brush_manager.Get_Current_Brush().Paint()); // todo: set size
                    SKPoint end = current_path.LastPoint;
                    current_path.Reset();
                    current_path.MoveTo(end);
                }
            }
        }



        /// <summary>
        /// Resets the entire canvas back to its starting state; 1 empty layer
        /// </summary>
        public void Reset_Canvas_State()
        {
            using (SKSurface surface = SKSurface.Create(canvas_info))
            {
                surface.Canvas.Clear();
                layer_manager.reset();
                selection_manager.Reset_Selection();
                paste_manager.reset();

                // remove when everything works with no layers
                //layer_manager.add_layer(canvas_info);
            }
        }


        public void Change_Brush(string brush_name)
        {
            brush_manager.Set_Brush(brush_name);
        }


        public void Change_Brush(string brush_name, NumericUpDown stroke_input_box)
        {
            brush_manager.Set_Brush(brush_name);

            // update the UI 
            stroke_input_box.Value = brush_manager.Get_Current_Brush().Stroke();
        }


        public void Repeated_Circles_Exercise(int spacing, int angle)
        {
            SKPaint exercise_paint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.RoyalBlue,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5
            };

            int length = (layer_manager.get_image(layer_manager.selected_layer).PeekPixels().Width + layer_manager.get_image(layer_manager.selected_layer).PeekPixels().Height) * 10;
            double angle_radians = -angle * (Math.PI / 180);
            double orth_angle_radians = angle_radians + (Math.PI / 2); // used to create parallel lines
            SKPoint vector = new SKPoint((float)(length * Math.Cos(angle_radians)), (float)(length * Math.Sin(angle_radians))); // direction and magnitude of all lines

            // We draw two lines at a time to make it easier to keep lines on the screen with any angle input
            // You can think of line one as the line below our previous line, and line two as the line above our previous line.
            // (this comes in handy if the angle is high enough, it makes it so we don't have to adjust our starting position or any angles or anything)
            SKPoint line_one_middle = new SKPoint(-layer_manager.get_image(layer_manager.selected_layer).PeekPixels().Width, 3);
            SKPoint line_two_middle = new SKPoint(-layer_manager.get_image(layer_manager.selected_layer).PeekPixels().Width, 3);

            // don't worry this doesn't really affect performance from my testing on my wimpy laptop at all
            for (int i = 0; i < 100; i++)
            {
                SKPath path = new SKPath();

                path.MoveTo(line_one_middle);
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
                {
                    // draw line one
                    path.MoveTo(-vector.X + line_one_middle.X, -vector.Y + line_one_middle.Y);
                    path.LineTo(vector.X + line_one_middle.X, vector.Y + line_one_middle.Y);

                    // draw line two
                    path.MoveTo(-vector.X + line_two_middle.X, -vector.Y + line_two_middle.Y);
                    path.LineTo(vector.X + line_two_middle.X, vector.Y + line_two_middle.Y);

                    surface.Canvas.DrawPath(path, exercise_paint);
                }

                line_one_middle.X += (float)(spacing * Math.Cos(orth_angle_radians));
                line_one_middle.Y += (float)(spacing * Math.Sin(orth_angle_radians));

                line_two_middle.X -= (float)(spacing * Math.Cos(orth_angle_radians));
                line_two_middle.Y -= (float)(spacing * Math.Sin(orth_angle_radians));
            }
        }

        /// <summary>
        /// Draws a use specified image onto the canvas with a given opacity. The image is
        /// displayed over the canvas and can be drawn on. The image is drawn onto an SKRect
        /// with its dimensions set by centering and scaling the image to fit its orignal size,
        /// but be drawn onto a smaller/bigger surface (depending on the image used).
        /// </summary>
        /// <param name="image_data">the bytes of the image to be drawn</param>
        /// <param name="pb">the picturebox of the original image</param>
        /// <param name="opacity">the opacity to set the image to</param>       
        public void Draw_Image_With_Opacity(byte[] image_data, int layer_idx)
        {
            using (SKImage image = SKImage.FromEncodedData(image_data))
            {
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_idx).PeekPixels()))
                {
                    using (SKPaint paint = new SKPaint())
                    {
                        // choose the min scaling factor
                        float scaleX = canvas_info.Width / (float)image.Width;
                        float scaleY = canvas_info.Height / (float)image.Height;
                        float scale = Math.Min(scaleX, scaleY);

                        // calculate new width and height using scale
                        float newWidth = image.Width * scale;
                        float newHeight = image.Height * scale;

                        // center the image
                        float x = (canvas_info.Width - newWidth) / 2;
                        float y = (canvas_info.Height - newHeight) / 2;

                        // create a rect with new dimensions to place the image in
                        SKRect skR = new SKRect(x, y, x + newWidth, y + newHeight);

                        surface.Canvas.DrawImage(image, skR, paint);
                    }
                }
            }
        }

        public void Draw_With_Brush(Point currentMousePosition)
        {
            if (current_path != null)
            {
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
                {
                    using (SKCanvas pathCanvas = surface.Canvas)
                    {
                        using (SKPaint paint = new SKPaint())
                        {
                            paint.ColorFilter = SKColorFilter.CreateBlendMode(brush_manager.Get_Current_Brush().Color(), SKBlendMode.SrcIn);
                            paint.BlendMode = SKBlendMode.SrcOver;

                            if (pointsToDraw.TryDequeue(out Point_Operation po))
                            {
                                if (po.operationType == Point_Operation.OperationType.line_to)
                                {

                                    // Check if there is a selected area
                                    if (selection_manager.selected_area != null)
                                    {
                                        // Calculate the angle and distance
                                        float deltaX = currentMousePosition.X - prevMousePosition.X;
                                        float deltaY = currentMousePosition.Y - prevMousePosition.Y;
                                        float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                                        // Calculate the rotation angle to determine the user's mouse movement direction
                                        float angle = (float)Math.Atan2(deltaY, deltaX);
                                        float rotationAngle = (float)(angle * (180.0f / Math.PI));

                                        float step = Get_Step_Size(brush_manager.Get_Current_Brush().Stroke()); // Step size for interpolation

                                        int steps = (int)(distance / step);

                                        for (int i = 0; i < steps; i++)
                                        {
                                            float t = (float)i / steps;
                                            float x = po.point.X + t * deltaX;
                                            float y = po.point.Y + t * deltaY;

                                            // Set the pivot point to the current interpolated position
                                            SKMatrix matrix = SKMatrix.CreateRotationDegrees(rotationAngle, x, y);

                                            // Apply the matrix transformation
                                            pathCanvas.SetMatrix(matrix);

                                            if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
                                                // Clip drawing within the selected area
                                                pathCanvas.ClipRegion(selection_manager.selected_area);

                                            // Draw the brush texture at the interpolated position
                                            float left = x - brush_manager.Get_Current_Brush().Textures["brush"].Width / 2f;
                                            float top = y - brush_manager.Get_Current_Brush().Textures["brush"].Height / 2f;
                                            SKRect destRect = new SKRect(left, top, left + brush_manager.Get_Current_Brush().Textures["brush"].Width, top + brush_manager.Get_Current_Brush().Textures["brush"].Height);
                                            pathCanvas.DrawBitmap(brush_manager.Get_Current_Brush().Textures["brush"], destRect, paint);

                                            // Reset the matrix and clip for subsequent drawing
                                            pathCanvas.ResetMatrix();
                                        }
                                    }
                                    else
                                    {
                                        // No selected area, draw without clipping
                                        pathCanvas.DrawBitmap(brush_manager.Get_Current_Brush().Textures["brush"], currentMousePosition.X, currentMousePosition.Y, paint);
                                    }

                                }
                                else if (po.operationType == Point_Operation.OperationType.jump)
                                {
                                    current_path.MoveTo(po.point.X, po.point.Y);
                                    prevMousePosition = po.point;
                                }
                                // Update the previous mouse position.
                                prevMousePosition = currentMousePosition;
                            }
                        }
                    }
                }
            }
        }


        // used for the stamping brush
        private float Get_Step_Size(int stroke)
        {
            switch (stroke)
            {
                case 30:
                    {
                        return 2.25f * stored_scale;
                    }
                case 50:
                    {
                        return 3.5f * stored_scale;
                    }
                case 80:
                    {
                        return 4.25f * stored_scale;
                    }
                case 100:
                    {
                        return 5.0f * stored_scale;
                    }
            }
            return -1;
        }


        internal void Handle_Copy()
        {
            if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
            {
                SKRectI bounds = selection_manager.selected_area.Bounds;
                SKImage c_image = layer_manager.get_image(layer_manager.selected_layer).Subset(bounds);

                using (SKSurface surface = SKSurface.Create(c_image.Info)) 
                using (MemoryStream pngMemStream = new MemoryStream())
                {
                    // only copy the selected area (complicated because of lasso)
                    SKRegion pasteRegion = new SKRegion(selection_manager.selected_area);
                    pasteRegion.Translate(-selection_manager.selected_area.Bounds.Left, -selection_manager.selected_area.Bounds.Top); 
                    surface.Canvas.ClipRegion(pasteRegion);
                    surface.Canvas.DrawImage(c_image, 0, 0); 
                    surface.Canvas.Translate(selection_manager.selected_area.Bounds.Left, selection_manager.selected_area.Bounds.Top);

                    // using the example code found on this stack overflow post(https://stackoverflow.com/a/46424800/15780106), modified for our needs
                    // if an application doesn't support pasting png's, then they can use bitmap instead
                    // (but it won't have transparency, fully transparent areas will be grey instead).
                    DataObject dataObj = new DataObject();
                    dataObj.SetData(DataFormats.Bitmap, true, surface.Snapshot().ToBitmap());

                    // if an application supports pasting png's, then they can prefer this one and get the transparency
                    SKData skData = surface.Snapshot().Encode();
                    skData.SaveTo(pngMemStream);
                    dataObj.SetData("PNG", false, pngMemStream);

                    Clipboard.SetDataObject(dataObj, true);
                }

            }
        }


        internal void Handle_Cut()
        {
            if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                return;
            if (selection_manager.active_select_tool != Selection_Manager.Selection_Tools.None)
            {
                Handle_Copy();

                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
                {
                    surface.Canvas.ClipRegion(selection_manager.selected_area);
                    surface.Canvas.Clear();
                }
            }
        }


        internal void Handle_Paste()
        {
            if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                return;

            if (Clipboard.ContainsData("PNG"))
            {
                selection_manager.Reset_Selection();
                current_tool = SketchPopTool.move_pasted;
                using (MemoryStream png_stream = Clipboard.GetData("PNG") as MemoryStream)
                {
                    paste_manager.start_pasting(SKImage.FromEncodedData(png_stream));
                }
            }
            else if (Clipboard.ContainsData(DataFormats.Bitmap))
            {
                selection_manager.Reset_Selection();
                current_tool = SketchPopTool.move_pasted;
                paste_manager.start_pasting(SKImage.FromBitmap(new Bitmap(Clipboard.GetImage()).ToSKBitmap()));
            }
        }


        public enum SketchPopTool
        {
            brush, // this one is actually brush and eraser, currently
            selector_rect,
            selector_lasso,
            fill,
            move_pasted,
            hand
        }

        public void Undo_Operation()
        {
            is_undo = true;
            var (selected_layer, opacity, data, operation) = operation_manager.undo_operation();
            if (selected_layer > -2)
            {
                if (operation.Equals("canvas"))
                {
                    SKImage temp_image = SKImage.FromEncodedData(data);
                    SKBitmap bitmap = SKBitmap.FromImage(temp_image);
                    SKImage image = SKImage.FromBitmap(bitmap);
                    layer_manager.set_layer(selected_layer, opacity, image);
                }
                else if (operation.Equals("empty"))
                {
                    layer_manager.set_layer(selected_layer, opacity, SKImage.Create(canvas_info));
                }
                else if (operation.Equals("add"))
                {
                    if (main_window.InvokeRequired)
                    {
                        main_window.Invoke(new Action(() =>
                        {
                            main_window.layer_delete(selected_layer);
                        }));
                    }
                    else
                    {
                        main_window.layer_delete(selected_layer);
                    }
                }
                else
                {
                    if (main_window.InvokeRequired)
                    {
                        main_window.Invoke(new Action(() =>
                        {
                            main_window.layer_add(selected_layer);
                        }));
                    }
                    else
                    {
                        main_window.layer_add(selected_layer);
                    }
                    SKImage temp_image = SKImage.FromEncodedData(data);
                    SKBitmap bitmap = SKBitmap.FromImage(temp_image);
                    SKImage image = SKImage.FromBitmap(bitmap);
                    layer_manager.set_layer(selected_layer, opacity, image);

                }
            }
        }


        public void Redo_Operation()
        {
            var (selected_layer, opacity, data, operation) = operation_manager.redo_operation();
            if (selected_layer > -2)
            {
                if (operation.Equals("canvas"))
                {
                    SKImage temp_image = SKImage.FromEncodedData(data);
                    SKBitmap bitmap = SKBitmap.FromImage(temp_image);
                    SKImage image = SKImage.FromBitmap(bitmap);
                    layer_manager.set_layer(selected_layer, opacity, image);
                }
                else if (operation.Equals("delete"))
                {
                    if (main_window.InvokeRequired)
                    {
                        main_window.Invoke(new Action(() =>
                        {
                            main_window.layer_delete(selected_layer);
                        }));
                    }
                    else
                    {
                        main_window.layer_delete(selected_layer);
                    }
                }
                else
                {
                    if (main_window.InvokeRequired)
                    {
                        main_window.Invoke(new Action(() =>
                        {
                            main_window.layer_add(selected_layer);
                        }));
                    }
                    else
                    {
                        main_window.layer_add(selected_layer);
                    }
                }
            }
        }

        public void clear_redo_undo()
        {
            operation_manager.clear_undo();
            operation_manager.clear_redo();
        }

        // when we add a new layer, we need to add a new layer in the operation manager
        public void add_new_layer_in_operation_manager(int index)
        {
            SKBitmap empty = new SKBitmap(500, 500);
            empty.Erase(SKColors.Empty);
            SKImage emptyImage = SKImage.FromBitmap(empty);
            byte[] data = emptyImage.Encode().ToArray();
            operation_manager.setup_new_layer_into_stack(index, 1, data);
        }

        public void delete_layer_in_operation_manager(int index)
        {
            operation_manager.delete_layer(index);
        }
    }
}
