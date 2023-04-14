using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Sketchpop
{
    public partial class main_window : Form
    {
        public System.Windows.Forms.Timer draw_timer = new System.Windows.Forms.Timer();
        bool mouse_down = false;

        private Database_Manager dbm = new Database_Manager();
        private List<UnsplashImage> _current_images = new List<UnsplashImage>();
        private int _index = 0;

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
            if (ref_img_search_query.Text != "")
            {
                _index = 0;

                string query = ref_img_search_query.Text;
                _current_images = dbm.ExecuteImageRequestQuery(query);

                if (_current_images.Count > 0)
                {
                    prev_img_button.Visible = true;
                    prev_img_button.Enabled = false;
                    next_img_button.Visible = true;
                    reference_img.Image = await convert_to_imageAsync(_current_images[_index].Get_Image_URL());
                }

                ref_img_search_query.Text = "";
            }
        }

        private async void prev_img_button_Click(object sender, EventArgs e)
        {
            if(_index > 0)
            {
                _index--;
                reference_img.Image = await convert_to_imageAsync(_current_images[_index].Get_Image_URL());
                
                if (_index == 0)
                {
                    prev_img_button.Enabled = false;
                }
                if (next_img_button.Enabled == false)
                {
                    next_img_button.Enabled = true;
                }
            }                  
        }

        private async void next_img_button_Click(object sender, EventArgs e)
        {
            if (_index < _current_images.Count-1)
            {
                _index++;
                reference_img.Image = await convert_to_imageAsync(_current_images[_index].Get_Image_URL());         
                
                if (_index == _current_images.Count-1)
                {
                    next_img_button.Enabled = false;
                }
                if(prev_img_button.Enabled == false) 
                {
                    prev_img_button.Enabled = true; 
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

        private void ref_img_search_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                search_button_Click(sender, e);
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
