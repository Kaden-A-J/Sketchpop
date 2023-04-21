using SkiaSharp;
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
        private Dictionary<string, Brush> _brushes;
        private Brush _current_brush;

        public Brush_Manager()
        {
            _brushes = new Dictionary<string,Brush>();
            _current_brush = new Brush("basic", 2, new SKColor(0,0,0,255));
            
            // add basic brush and eraser
            _brushes.Add(_current_brush.Name(), _current_brush);
            _brushes.Add("eraser", new Brush("eraser", 50, new SKColor(167, 167, 167, 255)));
        }

        public void Set_Brush(string brush_name)
        {
            if(_brushes.ContainsKey(brush_name))
            {
                _current_brush = _brushes[brush_name];
            }
        }

        public Brush Get_Current_Brush()
        {
            return _current_brush;
        }

        public void Add_Brush(Brush new_brush)
        {
            _brushes.Add(new_brush.Name(),new_brush);
        }

        public void Remove_Brush(Brush brush)
        {
            _brushes.Remove(brush.Name());
        }
    }
}
