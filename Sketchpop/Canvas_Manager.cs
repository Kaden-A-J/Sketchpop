using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SkiaSharp;
using System.IO;
using Extensions = SkiaSharp.Views.Desktop.Extensions;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        public SKPath current_path = null;
        public Canvas_Manager() {
            
        }

        public void Begin_Draw_Path(Point click_position)
        {
            current_path = new SKPath();
            current_path.FillType = SKPathFillType.EvenOdd;
            current_path.MoveTo(click_position.X, click_position.Y);
        }

        public void End_Draw_Path()
        {
            current_path.Close();
            current_path = null;
        }

        public Bitmap Continue_Draw_Path(Point click_position, Bitmap previous_canvas = null)
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
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 5
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

        /// <summary>
        /// Keep previously drawn content on the canvas
        /// </summary>
        /// <param name="new_canvas"></param>
        /// <param name="previous_canvas"></param>
        private  void Maintain_Canvas(SKCanvas new_canvas, Bitmap previous_canvas = null)
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
