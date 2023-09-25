using System;
using System.Drawing;
using SkiaSharp;
using System.Collections.Concurrent;
using System.Windows.Forms;
using SkiaSharp.Views.Desktop;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        public SketchPopTool current_tool { get; set; } = SketchPopTool.brush;
        public SKImageInfo canvas_info;
        public Layer_Manager layer_manager;
        public Point hand_difference = new Point(0, 28);
        public Point middle_drawing_start = new Point(0, 0);
        private Brush_Manager brush_manager;
        private Selection_Manager selection_manager;
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private Point hand_start = new Point(-1, -1);

        public Canvas_Manager()
        {
            brush_manager = new Brush_Manager();
            layer_manager = new Layer_Manager();
            selection_manager = new Selection_Manager();

            //picture_box = canvas_frame;
            canvas_info = new SKImageInfo(500, 500);
         

            Reset_Canvas_State();
        }

        public void Mouse_Is_Still_Down_Handler(Point point)
        {
            if (current_tool == SketchPopTool.brush)
            {
                Add_Point_To_Draw(point);
            }
            else if (current_tool == SketchPopTool.selector)
            {
                selection_manager.Continue_Selection(point);
            }
            else if (current_tool == SketchPopTool.hand)
            {
                hand_difference = new Point(hand_difference.X - (hand_start.X - point.X), hand_difference.Y - (hand_start.Y - point.Y));
                hand_start = point;
                //Console.WriteLine(hand_difference);
            }
        }

        public Point Adjust_Point_To_Hand(Point point)
        {
            return new Point(
                point.X - hand_difference.X - middle_drawing_start.X,
                point.Y - (hand_difference.Y - 28) - middle_drawing_start.Y
                ); // 28 is the size of the topstrip maybe should fix this later
        }

        public void Add_Point_To_Draw(Point point)
        {
            if (layer_manager.count == 0 || layer_manager.get_layer_locked(layer_manager.selected_layer))
                return;

            point = Adjust_Point_To_Hand(point);

            pointsToDraw.Enqueue(new Point_Operation(point, Point_Operation.OperationType.line_to));
        }

        public void Mouse_Down_Handler(Point click_position)
        {
            if (current_tool == SketchPopTool.brush)
            {
                Begin_Draw_Path(click_position);
            }    
            else if (current_tool == SketchPopTool.selector)
            {
                selection_manager.Begin_Selection(click_position);
            }
            else if (current_tool == SketchPopTool.hand)
            {
                hand_start = click_position;
                //hand_difference = new Point(0, 0);
            }
        }

        internal void Mouse_Up_Handler(Point click_position)
        {
            if (current_tool == SketchPopTool.selector)
            {
                selection_manager.End_Selection();
            }
        }

        public void Begin_Draw_Path(Point click_position)
        {
            current_path = new SKPath();
            current_path.FillType = SKPathFillType.EvenOdd;

            click_position = Adjust_Point_To_Hand(click_position);

            pointsToDraw.Enqueue(new Point_Operation(click_position, Point_Operation.OperationType.jump));
        }

        public void Update_Color(byte red, byte green, byte blue, byte alpha)
        {
            brush_manager.Get_Current_Brush().Set_Color(new SKColor(red, green, blue, alpha));
        }

        public void Update_Stroke_Size(int size)
        {
            brush_manager.Get_Current_Brush().Set_Stroke(size);
        }

        /// <summary>
        /// Draws the surface onto a Bitmap
        /// </summary>
        /// <param name = "surface" ></ param >
        /// < returns ></ returns >
        public void Draw_Bitmap(PictureBox target, int layer = -1, bool resize = false)
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
                    }
                else
                {
                    SKImage c_image = layer_manager.get_image(layer);
                    paint.Color = paint.Color.WithAlpha((byte)(0xFF * layer_manager.get_layer_opacity(layer)));
                    surface.Canvas.DrawImage(c_image, c_image.Info.Rect, paint);
                }

                if (selection_manager.active)
                {
                    // white part of outline
                    SKRect selection = selection_manager.CalculateSelectionRect();
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = Color.White.ToSKColor().WithAlpha(0xAA);
                    surface.Canvas.DrawRect(selection, paint);

                    // black part of outline
                    paint.Color = Color.Black.ToSKColor().WithAlpha(0xAA);
                    paint.PathEffect = SKPathEffect.CreateDash(new float[] {4f, 6f}, selection_manager.selection_animation_offset);
                    surface.Canvas.DrawRect(selection_manager.CalculateSelectionRect(), paint);
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
                    if (pointsToDraw.TryDequeue(out Point_Operation po))
                    {
                        if (po.operationType == Point_Operation.OperationType.line_to)
                        {
                            current_path.LineTo(po.point.X, po.point.Y);
                        }
                        else if (po.operationType == Point_Operation.OperationType.jump)
                        {
                            current_path.MoveTo(po.point.X, po.point.Y);
                        }
                        surface.Canvas.DrawPath(current_path, brush_manager.Get_Current_Brush().Paint());
                    }
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
        public void DrawImageWithOpacity(byte[] image_data, PictureBox pb, int layer_idx)
        {
            using (SKImage image = SKImage.FromEncodedData(image_data))
            {
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_idx).PeekPixels()))
                {
                    using (SKPaint paint = new SKPaint())
                    {                        
                        // choose the min scaling factor
                        float scaleX = pb.Width / (float)image.Width;
                        float scaleY = pb.Height / (float)image.Height;
                        float scale = Math.Min(scaleX, scaleY);

                        // calculate new width and height using scale
                        float newWidth = image.Width * scale;
                        float newHeight = image.Height * scale;

                        // center the image
                        float x = (pb.Width - newWidth) / 2;
                        float y = (pb.Height - newHeight) / 2;

                        // create a rect with new dimensions to place the image in
                        SKRect skR = new SKRect(x, y, x + newWidth, y + newHeight);

                        surface.Canvas.DrawImage(image, skR, paint);
                    }
                }
            }
        }

        public enum SketchPopTool
        {
            brush, // this one is actually brush and eraser, currently
            selector,
            fill,
            hand
        }
    }
}
