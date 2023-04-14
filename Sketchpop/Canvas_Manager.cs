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
using Mysqlx.Crud;

namespace Sketchpop
{
    public class Canvas_Manager
    {
        private ConcurrentQueue<Point_Operation> pointsToDraw = new ConcurrentQueue<Point_Operation>();
        private SKPath current_path;
        private SKPaint current_paint;
        private PictureBox picture_box;
        private SKImage previous_frame;
        
        public Canvas_Manager(ref PictureBox canvas_frame) {

            picture_box = canvas_frame;

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

        /// <summary>
        /// Draws the surface onto a Bitmap
        /// </summary>
        /// <param name = "surface" ></ param >
        /// < returns ></ returns >
        public void Draw_Bitmap(object state)
        {
            picture_box.Image = previous_frame.ToBitmap();
        }

        public void Draw_Path_Points(object state)
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
