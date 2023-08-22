using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Image = System.Drawing.Image;

namespace Sketchpop
{
    public partial class main_window : Form
    {
        public System.Windows.Forms.Timer draw_timer = new System.Windows.Forms.Timer();
        bool mouse_down = false;

        private Database_Manager dbm = new Database_Manager();
        private List<UnsplashImage> _current_images = new List<UnsplashImage>();

        private PictureBox selected_picturebox;
        private PictureBox prev_selected_picturebox;
        private string last_searched_query;

        public void draw_timer_method(Object my_object, EventArgs my_event_args)
        {
            if (mouse_down)
            {
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.AddPointToDraw(click_pos);
            }

            Program.canvas_manager.Draw_Path_Points(new object());
            Program.canvas_manager.Draw_Bitmap(new object());
        }

        public main_window()
        {
            InitializeComponent();
            Program.canvas_manager = new Canvas_Manager(ref canvas_frame);

            draw_timer.Tick += new EventHandler(draw_timer_method);
            draw_timer.Interval = 8; // 11.111... ms is 90 fps
            draw_timer.Start();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private const int
            HTCAPTION = 2,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        private const int
            _grip_size = 5, // window resize click border
            _caption_size = 20; // window drag click border

        private void get_ref_button_Click(object sender, EventArgs e)
        {
            dbm.ExecuteImageRequestQuery("");
        }

        private void put_ref_button_Click(object sender, EventArgs e)
        {
            dbm.ExecuteImageUploadQuery("", "");
        }

        private void canvas_frame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!mouse_down)
                {
                    mouse_down = true;
                    var click_pos = canvas_frame.PointToClient(MousePosition);
                    Program.canvas_manager.Begin_Draw_Path(click_pos);
                }
            }
        }

        private void canvas_frame_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouse_down = false;
                Program.canvas_manager.End_Draw_Path();
            }


        }

        private void canvas_frame_MouseMove(object sender, MouseEventArgs e)
        {
            //Program.canvas_manager.Draw_Path_Points(new object());
            //Program.canvas_manager.Draw_Bitmap(new object());
            // slower than timer
            //if (mouse_down)
            //{
            //    var click_pos = canvas_frame.PointToClient(MousePosition);
            //    Program.canvas_manager.AddPointToDraw(click_pos);
            //}
        }

        private async void search_button_Click(object sender, EventArgs e)
        {
            // display to the user that a search is being done
            db_status_label.Visible = true;
            db_status_label.Text = "Gathering Images...";

            // show the panel to the user
            ref_img_thumbnails.Visible = true;
            back_panel.Visible = true;
            db_status_label.Visible = false;

            string query = ref_img_search_query.Text; // get the user's query

            // if the user searches the same string, don't query the database
            if (query.Equals(last_searched_query) && _current_images.Count > 0)
            {
                // display the pre-queued selection view to the user
                back_panel.Visible = true;
                ref_img_thumbnails.Visible = true;
                last_searched_query = query; // save the last searched query
            }
            else
            {
                ref_img_thumbnails.Controls.Clear(); // clear the selection view so that only the searched query shows 

                _current_images = dbm.ExecuteImageRequestQuery(query);

                // get images from database -- empty string searches all entries of the database, if no entries exist in db, count will be 0
                if (_current_images.Count > 0)
                {
                    if (!select_button.Enabled)
                        select_button.Enabled = true;

                    // loop through all returned images and display them in the selection view
                    foreach (UnsplashImage image in _current_images)
                    {
                        // create pictureboxes for each image
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Image = await convert_to_imageAsync(image.Get_Image_URL());
                        pictureBox.Width = 100;
                        pictureBox.Height = 100;
                        pictureBox.Margin = new Padding(5);
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                        // each picturebox gets an event to detect user selection
                        pictureBox.Click += (s, args) =>
                        {
                            // logic for displaying the selected image to the user
                            if (selected_picturebox == null)
                            {
                                selected_picturebox = pictureBox;
                                prev_selected_picturebox = pictureBox;

                                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                pictureBox.BackColor = Color.Transparent;
                            }
                            else
                            {
                                if (prev_selected_picturebox != pictureBox)
                                {
                                    prev_selected_picturebox.BorderStyle = BorderStyle.None;
                                    prev_selected_picturebox.BackColor = Color.Transparent;

                                    selected_picturebox = pictureBox;
                                    prev_selected_picturebox = pictureBox;

                                    selected_picturebox.BorderStyle = BorderStyle.FixedSingle;
                                    selected_picturebox.BackColor = Color.Transparent;
                                }
                            }
                        };

                        ref_img_thumbnails.Controls.Add(pictureBox); // add images to the panel
                    }
                    last_searched_query = ref_img_search_query.Text; // save the search query in case the user uses the same search again
                }
                else
                {
                    db_status_label.Text = "No Entries in the Database.";
                }
            }
        }


        private async Task<Image> convert_to_imageAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var image = Image.FromStream(stream);

                    // Do something with the image object
                    return image;
                }
            }
        }
        public Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void ref_img_search_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                search_button_Click(sender, e);
            }
        }

        private void red_input_box_ValueChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.Update_Color((byte)red_input_box.Value, (byte)green_input_box.Value, (byte)blue_input_box.Value, (byte)255);
            color_display_box.BackColor = Color.FromArgb(255, (int)red_input_box.Value, (int)green_input_box.Value, (int)blue_input_box.Value);
        }

        private void green_input_box_ValueChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.Update_Color((byte)red_input_box.Value, (byte)green_input_box.Value, (byte)blue_input_box.Value, (byte)255);
            color_display_box.BackColor = Color.FromArgb(255, (int)red_input_box.Value, (int)green_input_box.Value, (int)blue_input_box.Value);
        }

        private void blue_input_box_ValueChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.Update_Color((byte)red_input_box.Value, (byte)green_input_box.Value, (byte)blue_input_box.Value, (byte)255);
            color_display_box.BackColor = Color.FromArgb(255, (int)red_input_box.Value, (int)green_input_box.Value, (int)blue_input_box.Value);
        }

        private void stroke_size_input_box_ValueChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.Update_Stroke_Size((int)stroke_size_input_box.Value);
        }

        private void clear_canvas_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Reset_Canvas_State();
        }

        private void eraser_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Change_Brush("eraser", stroke_size_input_box);
        }

        private void pen_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Change_Brush("basic", stroke_size_input_box);
        }

        private void repeatedCirclesPracticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options_form = new Repeated_Circles_Options_Form();
            options_form.ShowDialog();
        }

        private void upload_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.SafeFileName; // Gets the file name
                string filePath = openFileDialog.FileName; // Gets the full file path
                dbm.ExecuteLocalPictureUploadQuery(fileName, filePath);
            }
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            try
            {
                reference_img.Image = selected_picturebox.Image;
            }
            catch (Exception ex)
            {
                db_status_label.Visible = true;
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            back_panel.Visible = false;
            ref_img_thumbnails.Visible = false;
            db_status_label.Visible = false;
        }

        private void clear_database_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear the database? This will permanently delete all existing images in the database.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dbm.ExecuteDeleteDatabase();
                ref_img_thumbnails.Controls.Clear();
                select_button.Enabled = false;
                _current_images.Clear();
            }
        }

        private Rectangle top { get { return new Rectangle(0, 0, this.ClientSize.Width, _grip_size); } }
        private Rectangle left { get { return new Rectangle(0, 0, _grip_size, this.ClientSize.Height); } }
        private Rectangle bottom { get { return new Rectangle(0, this.ClientSize.Height - _grip_size, this.ClientSize.Width, _grip_size); } }
        private Rectangle right { get { return new Rectangle(this.ClientSize.Width - _grip_size, 0, _grip_size, this.ClientSize.Height); } }

        private Rectangle top_left { get { return new Rectangle(0, 0, _grip_size, _grip_size); } }
        private Rectangle top_right { get { return new Rectangle(this.ClientSize.Width - _grip_size, 0, _grip_size, _grip_size); } }
        private Rectangle bottom_left { get { return new Rectangle(0, this.ClientSize.Height - _grip_size, _grip_size, _grip_size); } }
        private Rectangle bottom_right { get { return new Rectangle(this.ClientSize.Width - _grip_size, this.ClientSize.Height - _grip_size, _grip_size, _grip_size); } }

        // From https://stackoverflow.com/q/2575216
        // To allow dragging and resizing the window without the default Window's form border
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {

                // Dragging the window's position
                Point pos = new Point(message.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < _caption_size)
                {
                    message.Result = (IntPtr)HTCAPTION;
                }


                // Resizing the window
                var cursor = this.PointToClient(Cursor.Position);

                Console.WriteLine(cursor);

                if (top_left.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (top_right.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (bottom_left.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (bottom_right.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
