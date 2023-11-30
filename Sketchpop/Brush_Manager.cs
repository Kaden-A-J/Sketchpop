using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _current_brush = new Brush("basic", 3, new SKColor(0, 0, 0, 255));

            // add basic brush and eraser
            _brushes.Add(_current_brush.Name(), _current_brush);
            _brushes.Add("eraser", new Brush("eraser", 50, SKColor.Empty, SKBlendMode.Clear));
            _brushes.Add("hand", new Brush("hand", 0, SKColor.Empty, SKBlendMode.Clear));

            // load textures
            Load_Textures();
        }

        private void Load_Textures()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            List<string> pngFiles;
            try
            {
                var folderDirectory = Path.Combine(baseDirectory, "..\\..\\Textures");
                 pngFiles = Directory.GetFiles(folderDirectory, "*.png").ToList();
            }
            catch (DirectoryNotFoundException e)
            {
                var folderDirectory = Path.Combine(baseDirectory, "Textures");
                pngFiles = Directory.GetFiles(folderDirectory, "*.png").ToList();
            }

            Dictionary<string, SKBitmap> txtrs = new Dictionary<string, SKBitmap>();
            Dictionary<string, string> flpths = new Dictionary<string, string>();

            foreach (string png in pngFiles)
            {
                string textureName = Path.GetFileNameWithoutExtension(png); // extract the file name without extension
                var texture = Resize_Texture(png,_current_brush.Stroke(), _current_brush.Stroke());
                var t_Bitmap = SKBitmap.FromImage(texture);

                txtrs.Add(textureName, t_Bitmap);
                flpths.Add(textureName, png);
            }

            _brushes.Add("paint", new Brush("paint", 2, new SKColor(0, 0, 0, 255), txtrs));
            _brushes["paint"].Set_Texture(txtrs.First().Value);
            _brushes["paint"].Set_Filepaths(flpths);
        }

        public SKImage Resize_Texture(string imagePath, int width, int height)
        {
            using (SKBitmap originalBitmap = SKBitmap.Decode(imagePath))
            {
                // calculate the new size while preserving the aspect ratio
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

                // resize the original bitmap
                SKBitmap resizedBitmap = originalBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);

                // create an SKImage from the resized bitmap
                SKImage resizedImage = SKImage.FromBitmap(resizedBitmap);

                return resizedImage;
            }
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
        /// useful for getting the color, since the current brush might be set to the eraser
        /// </summary>
        /// <param name="brush_name"></param>
        /// <returns></returns>
        public SKColor Get_Last_Selected_Color()
        {
            return _brushes["basic"].Color();
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
