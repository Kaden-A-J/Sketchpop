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
        private SKColor current_color;
        private int current_stroke_width;
        private SKImageInfo canvas_info;

        private Brush_Manager brush_manager;
        private Brush brush;
        public Canvas_Manager(ref PictureBox canvas_frame) {

            //current_color = new SKColor(0, 0, 0, 255);
            //current_stroke_width = 2;

            // Set Brushes
            brush_manager = new Brush_Manager();
            //brush = brush_manager.Get_Current_Brush();


            //Update_Current_Paint();

            picture_box = canvas_frame;

            // set up drawing tools


            canvas_info = new SKImageInfo(906, 625);
            Reset_Canvas_State();

            // Set Brushes
            //brush_manager = new Brush_Manager();

            //// add basic brush to brushes
            //brush_manager.Add_Brush(new Brush("basic", 2, Color.Black));
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

        public SKColor Get_Current_Color()
        {
            return current_color;
        }

        public void Update_Color(byte red, byte green, byte blue, byte alpha)
        {
            brush_manager.Get_Current_Brush().Set_Color(new SKColor(red, green, blue, alpha));
            //Update_Current_Paint();
        }

        public void Update_Stroke_Size(int size)
        {
            brush_manager.Get_Current_Brush().Set_Stroke(size);
            //current_stroke_width = size;
            //Update_Current_Paint();
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
                previous_frame = SKImage.FromPixelCopy(surface.PeekPixels());
            }
        }

        private void Update_Current_Paint()
        {
            current_paint = new SKPaint
            {
                IsAntialias = false,
                Color = current_color,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = current_stroke_width
            };
        }

        public void Change_Brush(string brush_name)
        {
            brush_manager.Set_Brush(brush_name);
        }
    }
}
