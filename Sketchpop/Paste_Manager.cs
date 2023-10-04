using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Drawing;

namespace Sketchpop
{
    internal class Paste_Manager
    {
        private SKImage pasted; 
        private SKPoint mouse_start;
        private SKPoint mouse_drag_offset;
        private SKPoint pasted_pos;
        private float _paste_animation_offset = 0f;
        private bool _started_paste;
        internal bool pasting
        {
            get
            {
                return pasted != null && _started_paste;
            }
        }

        private float paste_animation_time_offset
        {
            get
            {
                if (_paste_animation_offset <= 30)
                    _paste_animation_offset += .1f;
                else
                    _paste_animation_offset = 0f;

                return _paste_animation_offset;
            }
        }

        public Paste_Manager()
        {
            reset();
        }

        internal void start_pasting(SKImage to_paste)
        {
            this.pasted = to_paste;
            _started_paste = true;
        }

        internal void start_moving(Point mouse_start)
        {
            this.mouse_start = mouse_start.ToSKPoint();
        }

        internal void move(Point new_mouse_position)
        {
            mouse_drag_offset = new_mouse_position.ToSKPoint() - mouse_start;
        }
         
        internal void stop_moving(Point end_mouse_position)
        {
            pasted_pos += mouse_drag_offset;
            mouse_drag_offset = SKPoint.Empty;
        }

        internal void reset()
        {
            mouse_start = new SKPoint();
            mouse_drag_offset = new SKPoint();
            pasted_pos = new SKPoint();
            pasted = null;
            _started_paste = false;
        }

        internal void Draw_Pasted_Temporarily(SKSurface surface, bool use_outline, SKPaint paint)
        {
            SKPoint pos = pasted_pos + mouse_drag_offset;
            surface.Canvas.DrawImage(pasted, pos);
            if (use_outline)
            {
                SKRegion outline = new SKRegion();
                outline.SetRect(pasted.Info.Rect);
                outline.Translate((int)pos.X, (int)pos.Y);

                // white part of outline
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = Color.White.ToSKColor().WithAlpha(0xAA);
                surface.Canvas.DrawRegion(outline, paint);

                // black part of outline
                paint.Color = Color.Black.ToSKColor().WithAlpha(0xAA);
                paint.PathEffect = SKPathEffect.CreateDash(new float[] { 25f, 5f}, paste_animation_time_offset);
                surface.Canvas.DrawRegion(outline, paint);
            }
        }

        internal void Commit_Pasted(Layer_Manager layer_manager)
        {
            using (SKSurface surface = SKSurface.Create(layer_manager.get_image(layer_manager.selected_layer).PeekPixels()))
            using (SKPaint paint = new SKPaint())
            {
                Draw_Pasted_Temporarily(surface, false, paint);
                reset();
            }
        }
    }
}