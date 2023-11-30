using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Drawing;

namespace Sketchpop
{
    internal class Selection_Manager
    {
        /// <summary>
        /// whether there's actually a selected area being used or selected right now, and what type of selection it is
        /// </summary>
        public Selection_Tools active_select_tool { get; set; } 
        private float selection_animation_time_offset
        {
            get
            {
                if (_selection_animation_offset <= 10)
                    _selection_animation_offset += .2f;
                else
                    _selection_animation_offset = 0f;

                return _selection_animation_offset;
            }
        }
        private float _selection_animation_offset = 0f;
        public SKRegion selected_area { get; private set; }
        private SKPath path { get; set; }

        public Selection_Manager()
        {
            Reset_Selection();
        }
        public void Reset_Selection()
        {
            active_select_tool = Selection_Tools.None;
            selected_area = new SKRegion();
        }

        public void Begin_Selection(Point click_position, Selection_Tools tool_type, int maxX, int maxY)
        {
            active_select_tool = tool_type;
            path = new SKPath();
            path.MoveTo(normalize_point(click_position, maxX, maxY).ToSKPoint());
        }

        /// <summary>
        /// Finishes the selection by setting the selected_area based on points previously recorded. Can do rectangular or lasso select
        /// </summary>
        /// <param name="is_rect_select">true if rectangle select, false if lasso</param>
        public void End_Selection(int maxX, int maxY)
        {
            SetSelectionArea(maxX, maxY);

            // clicking without moving will reset the selection
            if (selected_area.Bounds.Width * selected_area.Bounds.Height <= 10)
            {
                Reset_Selection();
            }
        }

        public void Continue_Selection(Point current_click_position, int maxX, int maxY)
        {
            Point normalized = normalize_point(current_click_position, maxX, maxY);
            path.LineTo(normalized.ToSKPoint());
        }

        public void Draw_Outline(SKSurface surface, SKPaint paint)
        {
            SetSelectionArea(surface.PeekPixels().Info.Width, surface.PeekPixels().Info.Height);
            
            // white part of outline
            paint.Style = SKPaintStyle.Stroke;
            paint.Color = Color.White.ToSKColor().WithAlpha(0xAA);
            surface.Canvas.DrawRegion(selected_area, paint);

            // black part of outline
            paint.Color = Color.Black.ToSKColor().WithAlpha(0xAA);
            paint.PathEffect = SKPathEffect.CreateDash(new float[] { 4f, 6f }, selection_animation_time_offset);
            surface.Canvas.DrawRegion(selected_area, paint);
        }

        /// <summary>
        /// Returns true if there's no active_select_tool selection, or if the point is within the bounds of an active_select_tool selection
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Allowed(Point point)
        {
            return active_select_tool == Selection_Tools.None || selected_area.Contains(new SKPointI(point.X, point.Y));
        }

        private void SetSelectionArea(int maxX, int maxY)
        {
            if (active_select_tool == Selection_Tools.Rectangle)
            {
                Point top_left = new Point();
                Point bottom_right = new Point();
                Point start = new Point((int)Math.Round(path.GetPoint(0).X), (int)Math.Round(path.GetPoint(0).Y));
                Point end = new Point((int)Math.Round(path.LastPoint.X), (int)Math.Round(path.LastPoint.Y));
                if (end.X < start.X)
                {
                    top_left.X = end.X;
                    bottom_right.X = start.X;
                }
                else
                {
                    top_left.X = start.X;
                    bottom_right.X = end.X;
                }
                if (end.Y < start.Y)
                {

                    top_left.Y = end.Y;
                    bottom_right.Y = start.Y;
                }
                else
                {
                    top_left.Y = start.Y;
                    bottom_right.Y = end.Y;
                }


                top_left = normalize_point(top_left, maxX, maxY);
                bottom_right = normalize_point(bottom_right, maxX, maxY);
                SKRectI rect = new SKRectI(top_left.X, top_left.Y, bottom_right.X, bottom_right.Y);

                selected_area.SetRect(rect);
            }
            else if (active_select_tool == Selection_Tools.Lasso)
            {
                selected_area.SetPath(path);
            }
        }

        private Point normalize_point(Point p, int maxX, int maxY)
        {
            if (p.X < 0)
                p.X = 0;
            if (p.X >= maxX)
                p.X = maxX - 1;
            if (p.Y < 0)
                p.Y = 0;
            if (p.Y >= maxY)
                p.Y = maxY - 1;
            return p;
        }

        public enum Selection_Tools
        {
            None,
            Rectangle,
            Lasso
        }
    }
}
