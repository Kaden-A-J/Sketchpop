using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SkiaSharp;
using System.IO;
using Extensions = SkiaSharp.Views.Desktop.Extensions;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;
using SkiaSharp.Views.Desktop;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private SKPaint current_paint;
        private PictureBox picture_box;
        private volatile SKImage previous_frame;

        // note: this is System.Threading.Timer specifically because those timers run on their own threads by default, and are very simple
        Timer path_timer; // must be kept in this scope, or else timers get GC'd
        Timer draw_frame_timer;
        
        public Canvas_Manager() {

            // set up drawing tools
            current_paint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Red,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1
            };

            SKImageInfo info = new SKImageInfo(906, 625);
            using (SKSurface surface = SKSurface.Create(info))
            {
                surface.Canvas.Clear();
                previous_frame = SKImage.FromPixelCopy(surface.PeekPixels());
            }

            path_timer = new Timer(Draw_Path_Points, new object(), 0, 8);
            draw_frame_timer = new Timer(Draw_Bitmap, new object(), 0, 10);
    }


        public void AddPointToDraw(Point point)
        {
            pointsToDraw.Enqueue(new Point_Operation(point, Point_Operation.OperationType.line_to));
        }

        public void Begin_Draw_Path(Point click_position, ref PictureBox picture_box)
        {
            current_path = new SKPath();
            // todo: there's probably a better place to set picture_box
            // or better yet, a way to let the Main_Window_Form actually set it. I'll worry about it later though
            if (this.picture_box == null)
                this.picture_box = picture_box;
            current_path.FillType = SKPathFillType.EvenOdd;
            pointsToDraw.Enqueue(new Point_Operation(click_position, Point_Operation.OperationType.jump));
        }

        public void End_Draw_Path()
        {
            //current_path = null;
        }

        /// <summary>
        /// Draws the surface onto a Bitmap
        /// </summary>
        /// <param name = "surface" ></ param >
        /// < returns ></ returns >
        public void Draw_Bitmap(object state)
        {
            // stop timer (with how this is currently implemented, we don't wanna try drawing another frame until this one is done)
            draw_frame_timer.Change(Timeout.Infinite, Timeout.Infinite);

            if (picture_box != null)
            {
                using (SKSurface surface = SKSurface.Create(previous_frame.PeekPixels()))
                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                    // todo: verify if this line still sometimes crashes from memory corruption (let Parker know if it ever happens to you)
                    previous_frame = SKImage.FromPixelCopy(surface.PeekPixels());

                    // todo: verify if this line still sometimes crashes with "System.InvalidOperationException: 'Object is currently in use elsewhere.'"
                    // (may have fixed with no resetting picture_box, but let Parker know if it ever happens again)
                    picture_box.Image = new Bitmap(mStream, false);


                    // todo: sometimes it crashes near startup with lots of sporadic drawing with no stack trace...
                    // "The program '[29432] Sketchpop.exe' has exited with code 3221226356 (0xc0000374)."

                    // todo: if you're holding down mouse, and then without letting go hit another mouse button, the line goes to 0, 0 and back
                }
            }

            // restart timer
            draw_frame_timer.Change(0, 10);
        }

        private void Draw_Path_Points(object state)
        {
            if (current_path != null)
            {
                using (SKSurface surface = SKSurface.Create(previous_frame.PeekPixels()))
                {
                    if (current_path != null && pointsToDraw.TryDequeue(out Point_Operation po))
                    {
                        if (po.operationType == Point_Operation.OperationType.line_to)
                        {
                            current_path.LineTo(po.point.X, po.point.Y);
                            surface.Canvas.DrawPath(current_path, current_paint);
                        }
                        else if (po.operationType == Point_Operation.OperationType.jump)
                        {
                            current_path.MoveTo(po.point.X, po.point.Y);
                            surface.Canvas.DrawPath(current_path, current_paint);
                        }
                    }
                }
            }
        }
    }
}
