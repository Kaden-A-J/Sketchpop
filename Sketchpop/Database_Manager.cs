using System;
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
        private string _connection_string = "server=localhost;database=test;uid=root;pwd=\"\";";

        public void ExecuteImageRequestQuery(string query, PictureBox pb)
        {
            // For testing purposes, use this string query:
            query = "SELECT image FROM images WHERE name = 'cat.jpg'";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connection_string))
                {
                    conn.Open();

                    // create an sql command to be executed
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // create a reader to read the image data
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        byte[] img_data = (byte[])rdr["image"]; // data from the query

                        // convert the bytes data into an image
                        ImageConverter ic = new ImageConverter();
                        Image req_img = (Image)ic.ConvertFrom(img_data);

                        // show requested image on canvas here (assuming there is a element named pictureBox1 in the form code)
                        pb.Image = req_img;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n\n" + ex.Message);
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