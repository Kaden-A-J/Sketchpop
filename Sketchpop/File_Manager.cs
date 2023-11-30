using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Sketchpop
{
    public class File_Manager
    {
        private Layer_Manager layers = Program.canvas_manager.layer_manager;
        public void Save_as_EXC(string path, Layer_Manager layers)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (FileStream fs = File.Create(path))
            using (XmlWriter writer = XmlWriter.Create(fs))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("EXC");
                writer.WriteAttributeString("layer_count", layers.count.ToString());
                writer.WriteAttributeString("selected_layer", layers.selected_layer.ToString());
                for (int i = 0; i < layers.count; i++)
                {
                    writer.WriteStartElement("layer");
                    writer.WriteAttributeString("img_size", layers.get_image(i).Encode().Size.ToString());
                    writer.WriteAttributeString("img_data", Convert.ToBase64String(layers.get_image(i).Encode().ToArray()));
                    writer.WriteAttributeString("opacity", layers.get_layer_opacity(i).ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }

        public void Save_as_PNG(string path, Layer_Manager layers)
        {
            var final_image = new SKBitmap(500, 500);
            var canvas = new SKCanvas(final_image);
            canvas.Clear(SKColors.Transparent);
            for (int i = 0; i < layers.count; i++)
            {
                var paint = new SKPaint
                {
                    Color = new SKColor(255, 255, 255, (byte)(255 * layers.get_layer_opacity(i))),
                    BlendMode = SKBlendMode.SrcOver
                };
                canvas.DrawImage(layers.get_image(i), 0, 0, paint);
            }
            canvas.Flush();
            using (var image = SKImage.FromBitmap(final_image))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = File.OpenWrite(path))
            {
                data.SaveTo(stream);
            }
        }

        public int Load_as_EXC(string path, Layer_Manager layers)
        {
            // rebuild canvas
            int ret = 0;
            layers.reset();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
            using (FileStream fs = File.OpenRead(path))
            using (XmlReader reader = XmlReader.Create(fs))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "EXC":
                                int selectedLayer = int.Parse(reader.GetAttribute("selected_layer"));
                                layers.selected_layer = 0;
                                break;

                            case "layer":
                                int imgSize = int.Parse(reader.GetAttribute("img_size"));
                                byte[] imgData = Convert.FromBase64String(reader.GetAttribute("img_data"));
                                SKImage temp_image = SKImage.FromEncodedData(imgData);
                                SKBitmap bitmap = SKBitmap.FromImage(temp_image);
                                SKImage image = SKImage.FromBitmap(bitmap);
                                float opacity = float.Parse(reader.GetAttribute("opacity"));
                                layers.add_layer(image, opacity);
                                break;
                        }
                    }
                }
            }
            ret = layers.count;
            return ret;

        }

        public Image convert_canvas_into_Image()
        {
            var final_bitmap = new SKBitmap(500, 500);
            var canvas = new SKCanvas(final_bitmap);
            canvas.Clear(SKColors.Transparent);
            for (int i = 0; i < layers.count; i++)
            {
                var paint = new SKPaint
                {
                    Color = new SKColor(255, 255, 255, (byte)(255 * layers.get_layer_opacity(i))),
                    BlendMode = SKBlendMode.SrcOver
                };
                canvas.DrawImage(layers.get_image(i), 0, 0, paint);
            }
            canvas.Flush();
            // convert to Image format
            using (var image = SKImage.FromBitmap(final_bitmap))
            using (var memoryStream = new MemoryStream())
            {
                image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(memoryStream);
                // Create System.Drawing.Image from MemoryStream
                var final_image = System.Drawing.Image.FromStream(memoryStream);
                return final_image;
            }

        }

        public void Load_as_PNG(string path, Layer_Manager layers)
        {
            // rebuild canvas

        }
    }
}
