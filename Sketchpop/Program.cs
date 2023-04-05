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

        public static Bitmap DrawSquare(Point click_position, Bitmap previous_canvas = null)
        {
            var info = new SKImageInfo(640, 480);
            using (var surface = SKSurface.Create(info))
            {
                SKCanvas canvas = surface.Canvas;
                
                // keep previously drawn content
                if (previous_canvas == null)
                {
                    canvas.Clear(SKColors.White);
                }
                else
                {
                    canvas.DrawBitmap(Extensions.ToSKBitmap(previous_canvas), 0, 0);
                }

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

                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                    return new Bitmap(mStream, false);
                }
            }

        }
    }
}
