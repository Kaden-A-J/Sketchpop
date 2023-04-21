using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class Brush_Manager
    {
        internal class Brush
        {
            private string _name;
            private int _stroke;
            private Color _color;

            public Brush(string name, int stroke_size, Color color)
            {
                _name = name;
                _stroke = stroke_size;
                _color = color;
            }
        }

        List<Brush> _brushes;

        public Brush_Manager()
        {
            _brushes = new List<Brush>();
        }

    }
}
