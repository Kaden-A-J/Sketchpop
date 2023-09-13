using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql;
using MySql.Data.MySqlClient;
using Mysqlx;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;

namespace Sketchpop
{
    /// <summary>
    /// This class organizes the methods needed to make calls to our database. Calls for adding
    /// and removing images from our database, and methods for retrieving existing images as well.    
    /// </summary>
    public class Database_Manager
    {
        private string _connection_string = "server=store-for-images.ce93uhqibbvf.us-east-2.rds.amazonaws.com;database=Sketchpop;uid=admin;pwd=SketchPop;";
        private Unsplash_Manager _um = new Unsplash_Manager();

        /// <summary>
        /// Makes a call to the Unsplash API and returns a list of the number of images 
        /// specified by the user.
        /// </summary>
        /// <param name="query">search string entered by the user</param>
        /// <param name="num_pics">number of images to return chosen by the user</param>
        /// <returns>list of images returned by API call</returns>
        public List<UnsplashImage> Execute_Image_Request_Query(string query, int num_pics)
        {
            // get images from the Unsplash Manager
            List<UnsplashImage> ret_images = _um.Get_Images(query, num_pics);

            return ret_images;
        }

        /// <summary>
        /// Inserts images chosen by the user to the database. The database reptresents
        /// a 'favorites' list that the user can add and remove images for later use.
        /// </summary>
        /// <param name="images">list of images chosen by the user</param>
        public void Insert_New_Images(List<UnsplashImage> images)
        {
            string sql_query = "INSERT INTO images (image_id, image_description, image_author, image_author_profile, image_url,url, image_data) " +
                                          "VALUES (@image_id, @image_description, @image_author, @image_author_profile, @image_url, @url, @image_data)";

            using (MySqlConnection conn = new MySqlConnection(_connection_string))
            {
                conn.Open();

                foreach (UnsplashImage image in images)
                {
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    // columns in the database
                    cmd.Parameters.AddWithValue("@image_id", image.Get_ID());
                    cmd.Parameters.AddWithValue("@image_description", image.Get_Description());
                    cmd.Parameters.AddWithValue("@image_author", image.Get_Author());
                    cmd.Parameters.AddWithValue("@image_author_profile", image.Get_Author_Profile());
                    cmd.Parameters.AddWithValue("@image_url", image.Get_Image_URL());
                    cmd.Parameters.AddWithValue("@url", image.Get_Unsplash_URL());
                    cmd.Parameters.AddWithValue("@image_data", image.Get_Image_Data());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Removes images based on image ids. The list of ids comes from the
        /// user selecting images they would like to remove from the favorites database.
        /// </summary>
        /// <param name="ids">ids of the images to be removed</param>
        public void Remove_Images_By_Unsplash_Id(List<string> ids)
        {
            string sql_query = "DELETE FROM images WHERE image_id = @image_id";

            using (MySqlConnection conn = new MySqlConnection(_connection_string))
            {
                conn.Open();

                foreach (string id in ids)
                {
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    cmd.Parameters.AddWithValue("@image_id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Removes images based on image ids. The list of ids comes from the
        /// user selecting images they would like to remove from the user
        /// uploaded image database.
        /// </summary>
        /// <param name="ids">ids of the images to be removed</param>
        public void Remove_Images_By_Id(List<string> ids)
        {
            string sql_query = "DELETE FROM local_pictures WHERE Id = @Id";

            using (MySqlConnection conn = new MySqlConnection(_connection_string))
            {
                conn.Open();

                foreach (string id in ids)
                {
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    cmd.Parameters.AddWithValue("@Id", int.Parse(id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves existing images from the database. This method as of now
        /// gets called when the user requests to see their favorited images. A
        /// MySQL query is constructed and sent to return all of the images from
        /// the favorites DB
        /// </summary>
        /// <returns>list of images retrieved from the database</returns>
        public List<UnsplashImage> Get_Db_Images()
        {
            var db_images = new List<UnsplashImage>();

            try
            {
                string sql_query = $"SELECT * FROM images";
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

                        // check to see if entry contains image_data
                        if (!rdr.IsDBNull((rdr.GetOrdinal("image_data"))))
                        {
                            byte[] img_data = (byte[])rdr["image_data"];
                            image.Set_Image(img_data);
                        }

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

        /// <summary>
        /// Retrieves the user uploaded images from the local_images database.
        /// </summary>
        /// <returns>list of images returned from MySQL request</returns>
        public List<UnsplashImage> Get_User_Images()
        {
            var user_images = new List<UnsplashImage>();

            try
            {
                string sql_query = $"SELECT * FROM local_pictures";
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    // create an sql command to be executed
                    MySqlCommand cmd = new MySqlCommand(sql_query, conn);

                    // create a reader to read the image data
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32("Id");
                        string name = rdr.GetString("Name");
                        byte[] data = (byte[])rdr["ImageData"];
                        
                        // convert to an Unsplash_Image type
                        user_images.Add(ConvertImageToUnsplashImage(id, name, data));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message);
            }

            return user_images;
        }

        /// <summary>
        /// Converts the user's uploaded image into an 'Unsplash_Image' object so
        /// that they can be stored in the same database. The Unsplash_Image 
        /// representation of the image will contain a bytes[] so that the image
        /// data can be saved for storing and retrieving the images.
        /// </summary>
        /// <param name="id">DB id of the image</param>
        /// <param name="name">name of the file</param>
        /// <param name="img">byte[] representation of the image</param>
        /// <returns>returns an Unsplash_Image object for the user uploaded image</returns>
        public UnsplashImage ConvertImageToUnsplashImage(int id, string name, byte[] img)
        {
            // create an Unsplash representation of the image
            UnsplashImage user_img = new UnsplashImage(id.ToString(), name, "user", "", "", "");
            user_img.Set_Image(img);
            return user_img;
        }

        /// <summary>
        /// Executes a MySQL query to add a locally uploaded image to our 
        /// database. This database currently is not accessible from the UI
        /// and is planned to be accessible by the user.
        /// </summary>
        /// <param name="_picture_name">name of image to be saved</param>
        /// <param name="_picture_path">path of image to be saved</param>
        public void Execute_Local_Picture_Upload_Query(string _picture_name, string _picture_path)
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
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    string query = "INSERT INTO local_pictures (Name, ImageData) VALUES (@Name, @ImageData)";
                    MySqlCommand comm = new MySqlCommand(query, conn);
                    comm.Parameters.AddWithValue("@Name", _picture_name);
                    comm.Parameters.AddWithValue("@ImageData", imageBytes);
                    comm.ExecuteNonQuery();
                }

                MessageBox.Show("Image Uploaded Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex);
            }
        }

        /// <summary>
        /// Deletes the entire database, mainly for testing purposes.
        /// </summary>
        public void Execute_Delete_Database()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM images";

                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " rows cleared.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error::\n" + ex);
            }
        }

        /// <summary>
        /// Enters user login information to the database to be stored for future login. 
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>true if login signup was successful, false otherwise</returns>
        public bool Execute_Login_Signup_Query(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    // Check if username exists
                    string checkUsernameQuery = "SELECT password FROM users WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(checkUsernameQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            string signupQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                            using (MySqlCommand signupCmd = new MySqlCommand(signupQuery, conn))
                            {
                                signupCmd.Parameters.AddWithValue("@username", username);
                                signupCmd.Parameters.AddWithValue("@password", password);
                                signupCmd.ExecuteNonQuery();
                            }
                            return true;
                        }
                        // Username exists and check its password
                        else
                        {
                            if (result.ToString().Equals(password))
                                return true;
                            else
                                return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
    }
}