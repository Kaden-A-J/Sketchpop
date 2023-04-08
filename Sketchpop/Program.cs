using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Extensions = SkiaSharp.Views.Desktop.Extensions;

namespace Sketchpop
{
    internal static class Program
    {
        private static SKPath current_path = null;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main_window());


        }

        public static void Begin_Draw_Path(Point click_position)
        {
            current_path = new SKPath();
            current_path.FillType = SKPathFillType.EvenOdd;
            current_path.MoveTo(click_position.X, click_position.Y);
        }

        public static void End_Draw_Path()
        {
            current_path.Close();
            current_path = null;
        }


        public static Bitmap Continue_Draw_Path(Point click_position, Bitmap previous_canvas = null)
        {
            if (current_path != null)
            {
                var info = new SKImageInfo(906, 625); // TODO: this is hardcoded
                using (var surface = SKSurface.Create(info))
                {
                    SKCanvas canvas = surface.Canvas;

                    Maintain_Canvas(canvas, previous_canvas);

                    // set up drawing tools
                    var paint = new SKPaint
                    {
                        IsAntialias = true,
                        Color = SKColors.Red,
                        StrokeCap = SKStrokeCap.Round,
                        Style = SKPaintStyle.Stroke
                    };

                    current_path.LineTo(click_position.X, click_position.Y);
                    canvas.DrawPath(current_path, paint);

                    return Draw_Bitmap(surface);
                } 
            }
            else
            {
                return previous_canvas;
            }
        }

       
        public static Bitmap DrawSquare(Point click_position, Bitmap previous_canvas = null)
        {
            var info = new SKImageInfo(906, 625); // TODO: this is hardcoded, ideally we'd get and update the size somehow
            using (var surface = SKSurface.Create(info))
            {
                SKCanvas canvas = surface.Canvas;

                Maintain_Canvas(canvas, previous_canvas);

                // set up drawing tools
                var paint = new SKPaint
                {
                    IsAntialias = true,
                    Color = SKColors.Red,
                    StrokeCap = SKStrokeCap.Round
                };

                // draw a square by the mouse
                var rectangle = new SKRect(click_position.X - 5, click_position.Y - 5, click_position.X + 5, click_position.Y + 5);
                canvas.DrawRect(rectangle, paint);

                return Draw_Bitmap(surface);
            }
        }


        /// <summary>
        /// Keep previously drawn content on the canvas
        /// </summary>
        /// <param name="new_canvas"></param>
        /// <param name="previous_canvas"></param>
        private static void Maintain_Canvas(SKCanvas new_canvas, Bitmap previous_canvas = null)
        {
            // keep previously drawn content
            if (previous_canvas == null)
            {
                new_canvas.Clear(SKColors.White);
            }
            else
            {
                new_canvas.DrawBitmap(Extensions.ToSKBitmap(previous_canvas), 0, 0);
            }
        }

        /// <summary>
        /// Draws the surface onto a Bitmap
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        private static Bitmap Draw_Bitmap(SKSurface surface)
        {
            using (SKImage image = surface.Snapshot())
            using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (MemoryStream mStream = new MemoryStream(data.ToArray()))
            {
                return new Bitmap(mStream, false);
            }
        }

    }
}
