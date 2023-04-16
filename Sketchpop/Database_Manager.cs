using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace Sketchpop
{
    public class Database_Manager
    {
        private string _connection_string = "server=store-for-images.ce93uhqibbvf.us-east-2.rds.amazonaws.com;database=Sketchpop;uid=admin;pwd=SketchPop;";
        private Unsplash_Manager _um = new Unsplash_Manager();

        public List<UnsplashImage> ExecuteImageRequestQuery(string query)
        {
            // first check to see if db contains images of query
            var db_images = GetDbImages(query);
            if (db_images.Count > 0)
            {
                return db_images;
            }
            else // otherwise, make an API call to get images
            {
                // get images from the Unsplash Manager
                List<UnsplashImage> ret_images = _um.Get_Images(query);

                // insert the images into the database for the next time
                InsertNewImages(ret_images);

                return ret_images;
            }
        }

        public void InsertNewImages(List<UnsplashImage> images)
        {
            string sql_query = "INSERT INTO images (image_id, image_description, image_author, image_author_profile, image_url,url) " +
                                          "VALUES (@image_id, @image_description, @image_author, @image_author_profile, @image_url, @url)";

            using (MySqlConnection conn = new MySqlConnection(_connection_string))
            {
                conn.Open();

                foreach (UnsplashImage image in images)
                {
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    cmd.Parameters.AddWithValue("@image_id", image.Get_ID());
                    cmd.Parameters.AddWithValue("@image_description", image.Get_Description());
                    cmd.Parameters.AddWithValue("@image_author", image.Get_Author());
                    cmd.Parameters.AddWithValue("@image_author_profile", image.Get_Author_Profile());
                    cmd.Parameters.AddWithValue("@image_url", image.Get_Image_URL());
                    cmd.Parameters.AddWithValue("@url", image.Get_Unsplash_URL());

                    cmd.ExecuteNonQuery();
                }
            }        
        }

        public List<UnsplashImage> GetDbImages(string query)
        {
            var db_images = new List<UnsplashImage>();
            try
            {
                string sql_query = $"SELECT * FROM images WHERE image_id LIKE '%{query}%'";
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    // create an sql command to be executed
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    // create a reader to read the image data
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        string image_id = rdr["image_id"].ToString();
                        string image_description = rdr["image_description"].ToString();
                        string image_author = rdr["image_author"].ToString();
                        string image_author_profile = rdr["image_author_profile"].ToString();
                        string image_url = rdr["image_url"].ToString();
                        string url = rdr["url"].ToString();

                        UnsplashImage image = new UnsplashImage(image_id, image_description, image_author, image_author_profile, image_url, url);
                        db_images.Add(image);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message);
            }
            return db_images;
        }

        public void ExecuteLocalPictureUploadQuery(string _picture_name, string _picture_path)
        {
            // Read image file
            Image image = Image.FromFile(_picture_path);

            // Convert image to byte array
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = stream.ToArray();

            // Insert image into database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connection_string)) {
                    conn.Open();

                    string query = "INSERT INTO local_pictures (Name, ImageData) VALUES (@Name, @ImageData)";
                    MySqlCommand comm = new MySqlCommand(query, conn);
                    comm.Parameters.AddWithValue("@Name", _picture_name);
                    comm.Parameters.AddWithValue("@ImageData", imageBytes);
                    comm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex);
            }
        }

        public void ExecuteImageUploadQuery(string query, string name)
        {
            // for testing purposes, the query to insert the butterfly image is:
            query = "INSERT INTO images (name, image) VALUES (@name, @image)";

            // for testing purposes, enter name of the file here,
            // the file must also be in the Images folder.
            string name_of_file = "butterfly.jpg";
            var x = Directory.GetCurrentDirectory();
            string img_path = Path.Combine(x, @"..\..\..\Sketchpop\Images", name_of_file);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name_of_file);
                    cmd.Parameters.AddWithValue("@image", File.ReadAllBytes(img_path));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex);
            }
        }
    }
}