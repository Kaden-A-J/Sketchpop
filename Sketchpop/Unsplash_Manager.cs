using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class Unsplash_Manager
    {
        private string user_query;

        private readonly string ACESS_KEY = "";
        public Unsplash_Manager(string query) { 
            user_query = query;
        }

        public List<UnsplashImage> GetImages()
        {
            string queryURL;

            List<UnsplashImage> images = JsonConvert.DeserializeObject<List<UnsplashImage>>(json_data);
            return null;
        }
    }
}
