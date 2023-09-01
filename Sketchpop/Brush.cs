using SkiaSharp;

namespace Sketchpop
{
    /// <summary>
    /// A class for creating brushes.
    /// </summary>
    public class Brush
    {
        private string _name;
        private int _stroke;
        private SKColor _color;
        private SKPaint _paint;

        /// <summary>
        /// Constructor. A new brush will contain a 'name' to identify it, a 'stroke' to define the
        /// size of the stroke when drawing, and SKColor and SKPaint for determining the color of 
        /// the brush.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stroke"></param>
        /// <param name="color"></param>
        /// <param name="blend"></param>
        public Brush(string name, int stroke, SKColor color, SKBlendMode blend = SKBlendMode.SrcOver)
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
                BlendMode = blend,
                StrokeWidth = _stroke
            };
        }

        /* Setters */

        /// <summary>
        /// Sets the color of the brush to the specified color 'c'.
        /// </summary>
        /// <param name="c">the color to set the brush to</param>
        public void Set_Color(SKColor c)
        {
            _color = c;
            _paint.Color = _color;
        }

        /// <summary>
        /// Sets the stroke of the brush to the specified stroke 'stroke'.
        /// </summary>
        /// <param name="stroke">int size to set stroke to</param>
        public void Set_Stroke(int stroke)
        {
            _stroke = stroke;
            _paint.StrokeWidth = _stroke;
        }

        /* Getters */

        /// <summary>
        /// Returns the color of the brush.
        /// </summary>
        /// <returns>color of brush</returns>
        public SKColor Color()
        {
            return _color;
        }

        /// <summary>
        /// Returns the SKPaint object needed for painting using SK
        /// </summary>
        /// <returns>SKPaint object</returns>
        public SKPaint Paint()
        {
            return _paint;
        }

        /// <summary>
        /// Returns name of the brush.
        /// </summary>
        /// <returns>name of the brush</returns>
        public string Name()
        {
            return _name;
        }

        /// <summary>
        /// Returns stroke of the brush.
        /// </summary>
        /// <returns>stroke of the brush</returns>
        public int Stroke()
        {
            return _stroke;
        }
    }
}
