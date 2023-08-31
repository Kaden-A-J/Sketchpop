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

        private Database_Manager _dbm = new Database_Manager();
        private List<UnsplashImage> _current_images = new List<UnsplashImage>();

        private PictureBox _selected_picturebox;
        private PictureBox _prev_selected_picturebox;
        private string _last_searched_query;

        public void draw_timer_method(Object my_object, EventArgs my_event_args)
        {
            if (mouse_down)
            {
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.Add_Point_To_Draw(click_pos);
            }

            Program.canvas_manager.Draw_Path_Points(new object());
            Program.canvas_manager.Draw_Bitmap(new object());
        }

        public main_window()
        {
            InitializeComponent();
            Program.canvas_manager = new Canvas_Manager(ref canvas_frame);

            layers_box.Items.AddRange(Program.canvas_manager.Layers.ToArray());
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


        /*
         *  Start of -- Image Selection and Database Related Code
         */

        public Image set_image_opacity(Image image, float opacity)
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

                    //draw the image  
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

        private void img_form_buttonClick(object sender, EventArgs e)
        {
            var _ims = new Image_Search_Form(reference_img);

            _ims.Location = canvas_frame.Location;
            _ims.Show();
        }

        /* 
         *  End of -- Image Selection and Database Related Code
         */

        private void b_layer_1_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Select_Layer(0);
        }

        private void b_layer_2_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Select_Layer(1);
        }

        private void b_layer_3_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Select_Layer(2);
        }

        private void layers_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.Select_Layer(layers_box.SelectedIndex);
        }

        private void RegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login_window login = new login_window();
            login.Show();
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

                //Console.WriteLine(cursor);

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
