using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class File_Manager
    {

        public void Save_as_EXC(string path, Layer_Manager layers) {
            Console.WriteLine(path);
            using (FileStream fs = File.Create(path))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // write header   
                writer.Write("EXC");  
                Console.WriteLine("layers count = "+layers.count);
                writer.Write(layers.count);
                Console.WriteLine("selected layer = "+layers.selected_layer);
                writer.Write(layers.selected_layer);
                for (int i = 1; i < layers.count; i++)
                {
                    Console.WriteLine("saving layer " + i);
                    // save each layer as a separate image
                    SKData encodedData = layers.get_image(i).EncodedData;
                    writer.Write(encodedData.ToArray().Length);
                    writer.Write(encodedData.ToArray());
                    writer.Write(layers.get_layer_opacity(i));
                }
            }
     
        }

        public void Save_as_PNG(string path, Layer_Manager layers)
        {

        }

        public void Load_as_EXC(string path, Layer_Manager layers ) {
            // rebuild canvas
            layers.reset();
            using (FileStream fs = File.OpenRead(path))
            using(BinaryReader reader = new BinaryReader(fs))
            {
                  // read header
                String header = new String(reader.ReadChars(3));
                if (header != "EXC")
                {
                    throw new Exception("Invalid file format");
                }
                int layer_count = reader.ReadInt32();
                int selected_layer = reader.ReadInt32();
                for (int i = 0; i < layer_count; i++)
                {
                    int img_size = reader.ReadInt32();
                    byte[] img_data = reader.ReadBytes(img_size);
                    float opacity = reader.ReadSingle();
                    SKImage image = SKImage.FromEncodedData(img_data);
                    layers.add_layer(image, opacity);
                }
            }
            
        }

        public void Load_as_PNG(string path, Layer_Manager layers)
        {
            // rebuild canvas

        }
    }
}
