using SkiaSharp;
using System;
using System.Drawing;

namespace Sketchpop
{
    internal class Selection_Manager
    {
        /// <summary>
        /// whether there's actually a selected area being used or selected right now
        /// </summary>
        public bool active { get; set; } 
        public float selection_animation_offset
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
        private Point start { get; set; } 
        private Point end { get; set; }

        // todo: copy/paste clipboard: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.clipboard.setimage?view=windowsdesktop-7.0
        // todo: only edit in selected area.

        public Selection_Manager()
        {
            Reset_Selection();
        }
        public void Reset_Selection()
        {
            start = Point.Empty;
            active = false;
        }

        public void Begin_Selection(Point click_position)
        {
            Console.WriteLine("Selector tool: start: " + click_position);

            active = true;
            start = click_position;
        }

        public void End_Selection()
        {
            //Console.WriteLine("Selector tool: end_click: " + end_click_position);

            // clicking without moving will reset the selection
            SKRect r = CalculateSelectionRect();
            if (r.Width * r.Height <= 10)
            {
                Console.WriteLine("Selection Unselected/Reset");
                Reset_Selection();
            }
            else
            {
                active = true;
            }
        }

        public void Continue_Selection(Point current_click_position)
        {
            end = current_click_position;
        }

        public SKRect CalculateSelectionRect()
        {
            Point top_left = new Point();
            Point bottom_right = new Point();
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

            return new SKRect(top_left.X, top_left.Y, bottom_right.X, bottom_right.Y);
        }

    }
}
