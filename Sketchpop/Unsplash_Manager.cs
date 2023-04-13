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



        class UnsplashImage {
            // data from the Unsplash API call
            private string id;
            private string description;
            private string author;
            private string author_profile_link;
            private string image_url; // image url from unsplash
            private string unsplash_url;

            public UnsplashImage(string id, string description, string author, string author_profile_link, string image_url, string unsplash_url)
            {
                this.id = id;
                this.description = description;
                this.author = author;
                this.author_profile_link = author_profile_link;
                this.image_url = image_url;
                this.unsplash_url = unsplash_url;
            }

            public string Get_ID() { return id; }
            public string Get_Description() { return description; }
            public string Get_Author() { return author; }
            public string Get_Author_Profile() { return author_profile_link; }
            public string Get_Image_URL() { return image_url; }
            public string Get_Unsplash_URL() { return unsplash_url; }
        }
    }
}
