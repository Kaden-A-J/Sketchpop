using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sketchpop
{
    internal class File_Manager
    {
    }
}
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace Sketchpop
{
    public class File_Manager
    {
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
                                layers.selected_layer = selectedLayer;
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

        public void Load_as_PNG(string path, Layer_Manager layers)
        {
            // rebuild canvas

        }
    }
}
