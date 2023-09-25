using SkiaSharp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sketchpop
{
    /// <summary>
    /// Manager for the Brush object. This class has the ability to set chosen
    /// brush, add a brush and remove a brush from a list of brushes. Additional
    /// functionality can easily be added.
    /// </summary>
    public class Brush_Manager
    {
        private Dictionary<string, Brush> _brushes;
        private Brush _current_brush;

        /// <summary>
        /// Constructor. Adds the basic brush and the eraser brush to the list of 
        /// starting brushes. The current brush is set to the basic brush.
        /// </summary>
        public Brush_Manager()
        {
            _brushes = new Dictionary<string, Brush>();
            _current_brush = new Brush("basic", 2, new SKColor(0, 0, 0, 255));

            // add basic brush and eraser
            _brushes.Add(_current_brush.Name(), _current_brush);
            _brushes.Add("eraser", new Brush("eraser", 50, SKColor.Empty, SKBlendMode.Clear));

            _brushes.Add("hand", new Brush("hand", 0, SKColor.Empty, SKBlendMode.Clear));
        }

        /// <summary>
        /// Sets the current brush to the name specified in the parameter if the brush
        /// exists in the list of existing brushes.
        /// </summary>
        /// <param name="brush_name">name of brush to set current brush to</param>
        public void Set_Brush(string brush_name)
        {
            if (_brushes.ContainsKey(brush_name))
            {
                _current_brush = _brushes[brush_name];
            }
        }

        /// <summary>
        /// Returns the current brush.
        /// </summary>
        /// <returns>current brush</returns>
        public Brush Get_Current_Brush()
        {
            return _current_brush;
        }

        /// <summary>
        /// Adds a new brush to the list of available brushes
        /// </summary>
        /// <param name="new_brush"></param>
        public void Add_Brush(Brush new_brush)
        {
            if (!_brushes.ContainsKey(new_brush.Name()))
                _brushes.Add(new_brush.Name(), new_brush);
            else
                MessageBox.Show("Brush with the same name already exists.");
        }

        /// <summary>
        /// Removes a brush of the list of available brushes.
        /// </summary>
        /// <param name="brush"></param>
        public void Remove_Brush(Brush brush)
        {
            if (_brushes.ContainsKey(brush.Name()))
                _brushes.Remove(brush.Name());
            else
                MessageBox.Show($"Brush with name: {brush.Name()} does not exist.");
        }
    }
}
