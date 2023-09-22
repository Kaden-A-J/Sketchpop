using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchpop
{
    public class Layer
    {
        public int idx = -1;
        public SKImage Img { get; set; }
        public bool locked { get; set; }
        private float _opacity;
        /// <summary>
        /// value between 0 and 1; 0 being fully transparent, 1 being fully opaque
        /// </summary>
        public float Opacity 
        {
            get { return _opacity; }
            set
            {
                if (value < 0)
                {
                    _opacity = 0;
                }
                else if (value > 1)
                {
                    _opacity = 1;
                }
                else
                {
                    _opacity = value;
                }
            }
        }

        public String Name { get; set; }
        
        public Layer(int idx, SKImage img, float opacity)
        {
            this.idx = idx;
            this.Img = img;
            this.Opacity = opacity;
        }

        public Layer(String name, SKImage img, float opacity)
        {
            this.Name = name;
            this.Img = img;
            this.Opacity = opacity;
        }
    }
}
