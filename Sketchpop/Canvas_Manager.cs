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
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private PictureBox picture_box;
        private SKImage current_frame;
        private SKImageInfo canvas_info;

        private Brush_Manager brush_manager;
        public Canvas_Manager(ref PictureBox canvas_frame) {
            brush_manager = new Brush_Manager();
            picture_box = canvas_frame;
            canvas_info = new SKImageInfo(906, 625);
            Reset_Canvas_State();
    }


        public void AddPointToDraw(Point point)
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
        public void Draw_Bitmap(object state)
        {
            picture_box.Image = current_frame.ToBitmap();
        }

        public void Draw_Path_Points(object state)
        {
            if (current_path != null)
            {
                using (SKSurface surface = SKSurface.Create(current_frame.PeekPixels()))
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

        public void Reset_Canvas_State()
        {
            using (SKSurface surface = SKSurface.Create(canvas_info))
            {
                surface.Canvas.Clear();
                current_frame = SKImage.FromPixelCopy(surface.PeekPixels());
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
            Reset_Canvas_State();
            SKPaint exercise_paint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.RoyalBlue,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5
            };

            int length = (current_frame.PeekPixels().Width + current_frame.PeekPixels().Height) * 10;
            double angle_radians = -angle * (Math.PI / 180); 
            double orth_angle_radians = angle_radians + (Math.PI / 2); // used to create parallel lines
            SKPoint vector = new SKPoint((float)(length * Math.Cos(angle_radians)), (float)(length * Math.Sin(angle_radians))); // direction and magnitude of all lines

            // We draw two lines at a time to make it easier to keep lines on the screen with any angle input
            // You can think of line one as the line below our previous line, and line two as the line above our previous line.
            // (this comes in handy if the angle is high enough, it makes it so we don't have to adjust our starting position or any angles or anything)
            SKPoint line_one_middle = new SKPoint(-current_frame.PeekPixels().Width, 3);
            SKPoint line_two_middle = new SKPoint(-current_frame.PeekPixels().Width, 3);

            // don't worry this doesn't really affect performance from my testing on my wimpy laptop at all
            for (int i = 0; i < 100; i++)
            {
                SKPath path = new SKPath();

                path.MoveTo(line_one_middle);
                using (SKSurface surface = SKSurface.Create(current_frame.PeekPixels()))
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
    }
}
