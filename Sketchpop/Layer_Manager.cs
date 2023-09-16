using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Sketchpop
{
    public class Layer_Manager
    {
        private List<Layer> _layers;
        public int selected_layer;

        public int count { get { return _layers.Count; } }

        public Layer_Manager()
        {
            reset();
        }

        public void reset()
        {
            _layers = new List<Layer>();
            selected_layer = -1;
        }

        public void rename_layer(int idx, String name)
        {
            _layers[idx].Name = name;
        }

        public void set_layer_opacity(int idx, float opacity)
        {
            _layers[idx].Opacity = opacity;
        }

        public void set_layer_opacity(float opacity)
        {
            set_layer_opacity(selected_layer, opacity);
        }


        public float get_layer_opacity(int idx)
        {
            return _layers[idx].Opacity;
        }

        public void add_layer(SKImageInfo canvas_info)
        {
            Layer t_layer = new Layer(_layers.Count, SKImage.Create(canvas_info), 1);
            _layers.Add(t_layer);
            // if no layer -> auto select it
            selected_layer = (selected_layer == -1) ? 0 : selected_layer;
        }

        public void add_layer(SKImage img, float opacity)
        {
            Layer t_layer = new Layer(_layers.Count, img, opacity);
            _layers.Add(t_layer);

            // if no layer -> auto select it
            selected_layer = (selected_layer == -1) ? 0 : selected_layer;


        }

        /// <summary>
        /// Clears any drawings on the selected layer
        /// </summary>
        public void clear_layer()
        {
            using (SKSurface sks = SKSurface.Create(get_image(selected_layer).PeekPixels()))
            {
                sks.Canvas.Clear();
            }
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

        /// <summary>
        /// Takes the layer at the specified old_index, and places it at the specified new_index. 0-based indexes.
        /// </summary>
        public void Reorder_Layer(int old_index, int new_index)
        {
            List<Layer> new_order = new List<Layer>(count);
            for (int i = 0; i < count; i++)
            {
                if (i == new_index)
                {
                    new_order.Add(_layers[old_index]);
                    new_order.Add(_layers[i]);
                }
                else if (i != old_index)
                {
                    new_order.Add(_layers[i]);
                }
            }
        }

    }
}
