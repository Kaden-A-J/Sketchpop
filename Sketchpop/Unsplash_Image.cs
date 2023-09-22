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

        // used for associating user-uploaded image with unsplash images
        private byte[] _img_data;

        /// <summary>
        /// Constructor. Creates an Unsplash Image containing various information
        /// from the Unsplash API. Some information is needed for displaying the 
        /// image, some information is required by Unsplash (documentation/citation
        /// for the photographers, etc.).
        /// </summary>
        /// <param name="id">unsplash image id</param>
        /// <param name="description">photographer desc. of the image</param>
        /// <param name="author">photographer's name</param>
        /// <param name="author_profile_link">link to photographer's profile on Unsplash</param>
        /// <param name="image_url">url representation of the Unsplash image</param>
        /// <param name="unsplash_url">uurl to the Unsplash Website -- linked to this application</param>
        public UnsplashImage(string id, string description, string author, string author_profile_link, string image_url, string unsplash_url)
        {
            _id = id;
            _description = description;
            _author = author;
            _author_profile_link = author_profile_link;
            _image_url = image_url;
            _unsplash_url = unsplash_url;
        }

        // important getters
        public string Get_ID() { return _id; }
        public string Get_Description() { return _description; }
        public string Get_Author() { return _author; }
        public string Get_Author_Profile() { return _author_profile_link; }
        public string Get_Image_URL() { return _image_url; }
        public string Get_Unsplash_URL() { return _unsplash_url; }
        public byte[] Get_Image_Data() { return _img_data; }

        // setter for setting an image's data ( necessary for unsplash images that are only stored as urls)
        public void Set_Image(byte[] img) { _img_data = img; }
    }
}
