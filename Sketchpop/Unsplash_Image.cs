using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    /// <summary>
    /// Class for the necessary data required to represent an Unsplash Image. These
    /// images come from the Unsplash API.
    /// </summary>
    public class UnsplashImage
    {
        // data from the Unsplash API call
        private string _id;
        private string _description;
        private string _author;
        private string _author_profile_link;
        private string _image_url; // image url from unsplash
        private string _unsplash_url;

        public UnsplashImage(string id, string description, string author, string author_profile_link, string image_url, string unsplash_url)
        {
            _id = id;
            _description = description;
            _author = author;
            _author_profile_link = author_profile_link;
            _image_url = image_url;
            _unsplash_url = unsplash_url;
        }

        public string Get_ID() { return _id; }
        public string Get_Description() { return _description; }
        public string Get_Author() { return _author; }
        public string Get_Author_Profile() { return _author_profile_link; }
        public string Get_Image_URL() { return _image_url; }
        public string Get_Unsplash_URL() { return _unsplash_url; }
    }
}
