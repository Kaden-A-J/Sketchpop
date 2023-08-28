using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    internal class Layer
    {
        public SKImage img { get; set; }
        public float transparency { get; set; }
        
        public Layer(SKImage img, float transparency)
        {
            this.img = img;
            this.transparency = transparency;
        }
    }
}
