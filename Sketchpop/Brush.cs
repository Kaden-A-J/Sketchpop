using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Sketchpop
{
    /// <summary>
    /// A class for creating brushes.
    /// </summary>
    public class Brush
    {
        private string _name;
        private int _stroke;
        private int _last_pressurized_stroke;
        private SKColor _color;
        private SKPaint _paint;

        // texture brush variables
        private Dictionary<string, SKBitmap> _textures;
        private Dictionary<string, string> _texture_filepaths;
        private SKBitmap _current_texture;

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
            _last_pressurized_stroke = 0;
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

        public Brush(string name, int stroke, SKColor color, Dictionary<string, SKBitmap> textures)
        {
            _name = name;
            _stroke = stroke;
            _color = color;
            _paint = new SKPaint
            {
                IsAntialias = false,
                Color = _color,
                ColorFilter = null,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                BlendMode = SKBlendMode.SrcOver,
                StrokeWidth = _stroke
            };
            _textures = textures;            
        }

        public SKImage Resize_Texture(string imagePath, int width, int height)
        {
            using (SKBitmap originalBitmap = SKBitmap.Decode(imagePath))
            {
                // Calculate the new size while preserving the aspect ratio
                int newWidth, newHeight;
                if (originalBitmap.Width > originalBitmap.Height)
                {
                    newWidth = width;
                    newHeight = (int)((float)originalBitmap.Height / originalBitmap.Width * width);
                }
                else
                {
                    newHeight = height;
                    newWidth = (int)((float)originalBitmap.Width / originalBitmap.Height * height);
                }

                // Resize the original bitmap
                SKBitmap resizedBitmap = originalBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);

                // Create an SKImage from the resized bitmap
                SKImage resizedImage = SKImage.FromBitmap(resizedBitmap);

                return resizedImage;
            }
        }

        /* Setters */

        /// <summary>
        /// Sets the color of the brush to the specified color 'c'.
        /// </summary>
        /// <param name="c">the color to set the brush to</param>
        public void Set_Color(SKColor c)
        {
            if (this._textures != null && this._current_texture != null)
            {
                this._color = c;
                this._paint = new SKPaint
                {
                    Color = c,
                    ColorFilter = SKColorFilter.CreateBlendMode(c, SKBlendMode.SrcIn)
                };
            }
            else
            {
                this._color = c;
                this._paint.Color = _color;
            }
        }

        /// <summary>
        /// Sets the stroke of the brush to the specified stroke 'stroke'.
        /// </summary>
        /// <param name="stroke">int size to set stroke to</param>
        public void Set_Stroke(int stroke)
        {
            _stroke = stroke;
            _last_pressurized_stroke = 0;
            _paint.StrokeWidth = _stroke;
            if (_textures != null)
            {
                if (stroke > 1)
                {
                    var resizedTexture = Resize_Texture(_texture_filepaths["brush"], _stroke, _stroke);
                    _textures["brush"] = SKBitmap.FromImage(resizedTexture);
                }
                else
                {
                    var resizedTexture = Resize_Texture(_texture_filepaths["brush"], 2, 2);
                    _textures["brush"] = SKBitmap.FromImage(resizedTexture);
                }
            }
        }

        public void Set_Pressurized_Stroke(int pressure)
        {
            // pressure is 0 - 1024
            int modifier = ((pressure * _stroke) / 1024) + _last_pressurized_stroke / 2; // super basic interpolation, barely noticeable
            _last_pressurized_stroke = modifier;
            _paint.StrokeWidth = modifier;
        }

        public void Reset_Pressurized_Stroke()
        {
            _last_pressurized_stroke = 0;
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

        public void Set_Texture(SKBitmap value)
        {
            _current_texture = value;
        }

        internal void Set_Filepaths(Dictionary<string, string> flpths)
        {
            _texture_filepaths = flpths;
        }

        public Dictionary<string, SKBitmap> Textures => _textures;
        public Dictionary<string, string> Filepaths => _texture_filepaths;
        public SKBitmap Texture => _current_texture;
    }
}
