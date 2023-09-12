using System;
using System.Drawing;
using SkiaSharp;
using System.Collections.Concurrent;
using System.Windows.Forms;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private PictureBox picture_box;
        public SKImageInfo canvas_info;
        private Brush_Manager brush_manager;

        public Layer_Manager layer_manager;

        public Canvas_Manager(ref PictureBox canvas_frame)
        {
            brush_manager = new Brush_Manager();
            layer_manager = new Layer_Manager();

            picture_box = canvas_frame;
            canvas_info = new SKImageInfo(767, 625);
         

            Reset_Canvas_State();
        }        

        public void Add_Point_To_Draw(Point point)
        {
            pointsToDraw.Enqueue(new Point_Operation(point, Point_Operation.OperationType.line_to));
        }

        public void Begin_Draw_Path(Point click_position)
        {
            current_path = new SKPath();
            current_path.FillType = SKPathFillType.EvenOdd;
            pointsToDraw.Enqueue(new Point_Operation(click_position, Point_Operation.OperationType.jump));
        }

        public void End_Draw_Path()
        {

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
        public void Draw_Bitmap(PictureBox target, int layer = -1)
        {
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

                if (target.Image != null)
                    target.Image.Dispose();
                SKImage t_image = surface.Snapshot();

                SKBitmap t_bitmap = SKBitmap.FromImage(t_image);


                SKSizeI target_size = new SKSizeI(target.Width, target.Height);
                SKBitmap t_r_bitmap = t_bitmap.Resize(target_size, SKFilterQuality.High);

                target.Image = t_r_bitmap.ToBitmap();
                t_image.Dispose();
                t_bitmap.Dispose();
                t_r_bitmap.Dispose();

            }
        }

        public void Draw_Path_Points(object state)
        {
            if (current_path != null)
            {
                using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
                {
                    if (pointsToDraw.TryDequeue(out Point_Operation po))
                    {
                        if (po.operationType == Point_Operation.OperationType.line_to)
                        {
                            current_path.LineTo(po.point.X, po.point.Y);
                            surface.Canvas.DrawPath(current_path, brush_manager.Get_Current_Brush().Paint());
                        }
                        else if (po.operationType == Point_Operation.OperationType.jump)
                        {
                            current_path.MoveTo(po.point.X, po.point.Y);
                            surface.Canvas.DrawPath(current_path, brush_manager.Get_Current_Brush().Paint());
                        }
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

                // remove when everything works with no layers
                layer_manager.add_layer(canvas_info);
            }
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
                        paint.Color = new SKColor(paint.Color.Red, paint.Color.Green, paint.Color.Blue); // Set the opacity

                        // center the image
                        float x = (pb.Width - canvas_info.Width) / 2;
                        float y = (pb.Height - canvas_info.Height) / 2;

                        // fit the image into the picturebox
                        float scaleX = pb.Width / (float)image.Width;
                        float scaleY = pb.Height / (float)image.Height;
                        float scale = Math.Min(scaleX, scaleY);

                        // create the rectangle that fits the image to draw on
                        SKRect skR = new SKRect(x, y, x + image.Width * scale, y + image.Height * scale);

                        surface.Canvas.DrawImage(image, skR, paint);
                    }
                }
            }
        }
    }
}
