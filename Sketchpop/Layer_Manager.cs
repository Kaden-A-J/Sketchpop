using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class Layer_Manager
    {
        private List<Layer> _layers = new List<Layer>();
        public int selected_layer = -1;

        public int count { get { return _layers.Count; } }

        public void rename_layer(int idx, String name)
        {
            _layers[idx].Name = name;
        }

        public void set_layer_opacity(int idx, int opacity)
        {
            _layers[idx].Opacity = opacity;
        }

        public float get_layer_opacity(int idx)
        {
            return _layers[idx].Opacity;
        }

        public void add_layer(SKImageInfo canvas_info)
        {
            Layer t_layer = new Layer(_layers.Count, SKImage.Create(canvas_info), 255);
            _layers.Add(t_layer);

            // if no layer -> auto select it
            selected_layer = (selected_layer == -1) ? 0 : selected_layer;
        }

        public void delete_layer(int idx)
        {
            _layers.RemoveAt(idx);

            // decrement selection if deleting current selection
            selected_layer = (selected_layer == idx) ? selected_layer - 1 : selected_layer;
        }

        public SKImage get_image(int idx)
        {

            return (idx == -1) ? null : _layers[idx].Img;
        }
    }
}
