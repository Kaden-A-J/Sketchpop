using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class File_Manager
    {

        public void Save_as_EXC(string path, Layer_Manager layers) {
            Console.WriteLine(path);
            for(int i = 0; i < layers.count; i++)
            {
                // save each layer as a separate image
                SkiaSharp.SKData encodedData = layers.get_image(i).EncodedData;
                //System.IO.File.WriteAllBytes(path + i + ".png", encodedData.ToArray());
               
            }
        }

        public void Save_as_PNG(string path, Layer_Manager layers)
        {

        }

        public void Load_as_EXC(string path, Layer_Manager layers ) {
            // rebuild canvas
            
        }

        public void Load_as_PNG(string path, Layer_Manager layers)
        {
            // rebuild canvas

        }
    }
}
