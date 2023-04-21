using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class Brush
    {
        private string _name;
        private int _stroke;
        private SKColor _color;
        private SKPaint _paint;
        public Brush(string name, int stroke, SKColor color)
        {
            _name = name;
            _stroke = stroke;
            _color = color;
            _paint = new SKPaint
            {
                IsAntialias = false,
                Color = _color,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = _stroke
            };
        }

        // Setters
        public void Set_Color(SKColor c)
        {
            _color = c;
            _paint.Color = _color;
        }
        public void Set_Stroke(int stroke)
        {
            _stroke = stroke;
            _paint.StrokeWidth = _stroke;
        }

        // Getters 
        public SKColor Color()
        {
            return _color;
        }

        public SKPaint Paint()
        {
            return _paint;
        }

        public string Name()
        {
            return _name;
        }
    }
}
