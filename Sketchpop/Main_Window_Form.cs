using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sketchpop.Image_Layer_Options_Form;
using static Sketchpop.Image_Search_Form;

namespace Sketchpop
{
    public partial class main_window : Form
    {
        public Timer draw_timer = new Timer();
        bool mouse_down = false;
        List<Panel> layers_ui;

        // reference image variables
        private string _author_link;
        private byte[] _ref_image_data;
        private Unsplash_Manager _um;

        public void draw_timer_method(Object my_object, EventArgs my_event_args)
        {
            if (mouse_down)
            {
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.Mouse_Is_Still_Down_Handler(click_pos);
            }

            Program.canvas_manager.Draw_Path_Points(new object());
            Program.canvas_manager.Draw_Bitmap(canvas_frame, -1);
            Program.canvas_manager.Draw_Bitmap(main_preview_picturebox, -1);
        }

        public void render_layer_previews(Object my_object, EventArgs my_event_args)
        {
            for (int idx = 0; idx < layers_ui.Count; idx++)
                foreach (Control c in layers_ui[idx].Controls)
                    if (c.Name == "preview_panel")
                    {
                        Program.canvas_manager.Draw_Bitmap((PictureBox)c, idx);
                    }
        }

        public main_window()
        {
            InitializeComponent();
            Program.canvas_manager = new Canvas_Manager();

            layers_ui = new List<Panel>();
            _um = new Unsplash_Manager();

            // TODO make it so stuff works with no layers, right now it breaks so i'm restricting it to always have atleast one
            layer_add_button_Click(null, null);

            draw_timer.Tick += new EventHandler(draw_timer_method);
            draw_timer.Tick += new EventHandler(render_layer_previews);
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
                    Program.canvas_manager.Mouse_Down_Handler(click_pos);
                }
            }
        }

        private void canvas_frame_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouse_down = false;
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.Mouse_Up_Handler(click_pos);
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

        public void clear_canvas_button_Click(object sender, EventArgs e)
        {
            layers_ui.Clear();
            layer_panel.Controls.Clear();

            // TODO make it so stuff works with no layers, right now it breaks so i'm restricting it to always have atleast one
            layer_add_button_Click(null, null);

            Program.canvas_manager.Reset_Canvas_State();
        }

        private void eraser_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
            Program.canvas_manager.Change_Brush("eraser", stroke_size_input_box);
        }

        private void pen_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
            Program.canvas_manager.Change_Brush("basic", stroke_size_input_box);
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.selector;
        }

        private void repeatedCirclesPracticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options_form = new Repeated_Circles_Options_Form(this);
            options_form.ShowDialog();
        }


        private void RegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login_window login = new login_window();
            login.Show();
        }

        private void temp_transparency_num_up_down_ValueChanged(object sender, EventArgs e)
        {
            Program.canvas_manager.layer_manager.set_layer_opacity((float)temp_transparency_num_up_down.Value);
        }

        public void layer_visible_button_clicked(Object my_object, EventArgs my_event_args)
        {
            RadioButton c_button = (RadioButton)(my_object);
            Panel c_panel = (Panel)(c_button).Parent;
            int c_idx = Int32.Parse(c_panel.Name);

            // uncheck old selection
            foreach (Control c in layers_ui[Program.canvas_manager.layer_manager.selected_layer].Controls)
                if (c.GetType() == typeof(RadioButton))
                    ((RadioButton)c).Checked = false;

            c_button.Checked = true;

            Program.canvas_manager.layer_manager.selected_layer = c_idx;
        }

        public void layer_add_button_Click(object sender, EventArgs e)
        {
            int buffer = 4;
            Panel t_panel = new Panel();
            // panel name is its layer's index to help with layer selection
            t_panel.Name = layers_ui.Count.ToString();
            t_panel.BackColor = Color.FromArgb(255, 157, 157, 157);
            t_panel.Size = new Size(this.layer_panel.Width - buffer - 20, 60);
            t_panel.Location = new Point(4, (t_panel.Height + buffer) * layers_ui.Count);

            RadioButton t_visible_button = new RadioButton();
            t_visible_button.Size = new Size(20, 20);
            t_visible_button.Location = new Point(buffer, t_panel.Height / 2 - t_visible_button.Height / 2);
            t_panel.Controls.Add(t_visible_button);

            PictureBox t_preview_panel = new PictureBox();
            t_preview_panel.BackColor = Color.FromArgb(255, 167, 167, 167);
            t_preview_panel.Size = new Size((t_panel.Height - buffer * 2) / 9 * 16, t_panel.Height - buffer * 2);
            t_preview_panel.Location = new Point(t_visible_button.Width + buffer, buffer);
            t_preview_panel.Name = "preview_panel";
            t_panel.Controls.Add(t_preview_panel);

            Label t_name_label = new Label();
            //t_name_label.Font = new Font("Microsoft Sans Serif", 6);
            t_name_label.Location = new Point(t_preview_panel.Location.X + t_preview_panel.Width + buffer, t_panel.Height / 4);
            t_name_label.Text = "layer (" + layers_ui.Count.ToString() + ")";
            t_panel.Controls.Add(t_name_label);


            layers_ui.Add(t_panel);
            this.layer_panel.Controls.Add(t_panel);

            Program.canvas_manager.layer_manager.add_layer(Program.canvas_manager.canvas_info);

            // autocheck the first layer
            if (layers_ui.Count == 1)
                t_visible_button.Checked = true;

            t_visible_button.Click += new EventHandler(layer_visible_button_clicked);
        }

        // temp just delete the last layer, eventually change to delete a selection
        private void layer_delete_button_Click(object sender, EventArgs e)
        {
            if (layers_ui.Count < 2) { return; }

            int last_idx = layers_ui.Count - 1;

            // if selected layer is deleted -> shift it one down
            if (last_idx == Program.canvas_manager.layer_manager.selected_layer)
            {
                Program.canvas_manager.layer_manager.selected_layer -= 1;

                foreach (Control c in layers_ui[Program.canvas_manager.layer_manager.selected_layer].Controls)
                    if (c.GetType() == typeof(RadioButton))
                        ((RadioButton)c).Checked = true;
            }

            Program.canvas_manager.layer_manager.delete_layer(last_idx);

            this.layer_panel.Controls.Remove(layers_ui[last_idx]);
            layers_ui.RemoveAt(last_idx);
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

        /*
         *  Start of -- Image Selection and Database Related Code
         */

        /// <summary>
        /// When the user clicks this button, a new form is created, with an event
        /// handler to signify the main form when the user selects an image.
        /// </summary>
        /// <param name="sender">the user clicks button to open new ref img form</param>
        /// <param name="e">n/a</param>
        private void img_form_buttonClick(object sender, EventArgs e)
        {
            var _ims = new Image_Search_Form();

            _ims._image_selected += image_selected; // subscribe to the selected image even            
            _ims.StartPosition = FormStartPosition.CenterParent;

            _ims.ShowDialog();
        }

        /// <summary>
        /// An event handler for when the user selects an image from the Image Selection Form.
        /// 
        /// Once an image has been selected, the relevant data such as the Image selected, the
        /// photographer's name that took the image, and the photographer's profile page are all
        /// encapsulated into an object that can be accessed by the Main Form. The event also 
        /// triggers some labels to be shown to the user displaying this data.
        /// </summary>
        /// <param name="sender">the form that is invoking this event</param>
        /// <param name="data">the photographer's name/link to profile and the image that was selected</param>
        private void image_selected(object sender, SelectedImageData data)
        {
            // extract data
            reference_img.Image = data.Image;
            _author_link = data.Link;
            _ref_image_data = data.Bytes;

            // truncate the lenght of the string to 8 chars
            if (data.Name.Length > 8)
                author_link_label.Text = data.Name.Substring(0, 8);
            else
            {
                author_link_label.Text = data.Name;
            }

            // make image description labels visible if unsplash image chosen
            if (!data.Name.Equals("user"))
            {
                pb_label.Visible = true;
                author_link_label.Visible = true;
                on_label.Visible = true;
                unsplash_link.Visible = true;
            }

            // create tool tips for the links
            ToolTip auth_tt = new ToolTip();
            auth_tt.SetToolTip(author_link_label, $"Photographer: {data.Name} - - Profile: {_author_link}");
            auth_tt.AutoPopDelay = 5000;
            ToolTip unsp_tt = new ToolTip();
            unsp_tt.SetToolTip(unsplash_link, "https://www.unsplash.com/?utm_source=Sketchpop&utm_medium=referral");
            unsp_tt.AutoPopDelay = 5000;

            // enable options for image
            ref_img_options.Visible = true;
        }

        /// <summary>
        /// The user clicks the LinkLabel associated with the Unsplash API website.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void unsplash_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.unsplash.com/?utm_source=Sketchpop&utm_medium=referral");
        }

        /// <summary>
        /// The user clicks the LinkLabel associated with the photographer of the Image selected.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void author_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(_author_link);
        }

        /// <summary>
        /// Menustrip item for adding an image to a layer. Opens an ImageLayerOptions Form and sets its
        /// event handlers to be notified by the actions done by the user in the form.
        /// </summary>
        /// <param name="sender">menustrip item</param>
        /// <param name="e">n/a</param>
        private void addImageToLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp = new Image_Layer_Options_Form(_ref_image_data, layers_ui.Count);//Program.canvas_manager.DrawImageWithOpacity(_ref_image_data, canvas_frame, (float)0.5);
            tmp.saved += Add_Image_To_Layer;
            tmp.add_layer += layer_add_button_Click;
            //tmp.delete_layer += layer_delete_button_Click;
            tmp.StartPosition = FormStartPosition.CenterParent;
            tmp.ShowDialog();

            ref_img_menustrip.Visible = false;
        }

        /// <summary>
        /// Event Handler for adding an image to a layer. When this method is invoked it
        /// obtains the Save_Data, which contains the changes made by the user in the form,
        /// and applys them to the image when drawing the image to the canvas.
        /// </summary>
        /// <param name="sender">user triggers event</param>
        /// <param name="e">user changes</param>
        private void Add_Image_To_Layer(object sender, Save_Data e)
        {
            // get the modified image and layer index
            byte[] modified_img = e.Image_Data;
            int layer_index = e.Layer;

            // add the image to the layer
            Program.canvas_manager.DrawImageWithOpacity(modified_img, canvas_frame, layer_index);

            ref_img_menustrip.Visible = false;
        }

        /// <summary>
        /// Event Handler for when the user presses the button on the selected image in the 
        /// ref_img_thumbnail. When pressed, the menustrip becomes visible.
        /// </summary>
        /// <param name="sender">user presses button</param>
        /// <param name="e">n/a</param>
        private void ref_img_options_Click(object sender, EventArgs e)
        {
            ref_img_menustrip.Location = new Point(canvas_frame.Location.X, canvas_frame.Location.Y);
            ref_img_menustrip.Visible = !ref_img_menustrip.Visible;
        }

        // Image Option Menu Logic
        private void viewImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp = new Fullscreen_Image_Form(_ref_image_data);
            tmp.StartPosition = FormStartPosition.CenterParent;
            tmp.ShowDialog();

            ref_img_menustrip.Visible = false;
        }
        private void saveAsExcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save_file_window = new SaveFileDialog())
            {
                save_file_window.InitialDirectory = ".\\";
                save_file_window.Filter = "exc files (*.exc)|*.exc|All files (*.*)|*.*";
                save_file_window.FilterIndex = 1;
                save_file_window.RestoreDirectory = true;
                if (save_file_window.ShowDialog() == DialogResult.OK)
                {
                    new File_Manager().Save_as_EXC(save_file_window.FileName, Program.canvas_manager.layer_manager);
                }
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int layer_count = 0;
            String path = "";
            using (OpenFileDialog open_file_window = new OpenFileDialog())
            {
                open_file_window.InitialDirectory = ".\\";
                open_file_window.Filter = "exc files (*.exc)|*.exc|png files (*.png)|*.png";
                open_file_window.FilterIndex = 1;
                open_file_window.RestoreDirectory = true;
                if (open_file_window.ShowDialog() == DialogResult.OK)
                {
                    path = open_file_window.FileName;
                }
            }
            if (path.EndsWith(".exc"))
            {
                clear_canvas_button_Click(null, null);
                layer_count = new File_Manager().Load_as_EXC(path, Program.canvas_manager.layer_manager);
                for (int i = 0; i < layer_count; i++)
                {
                    layer_add_button_Click(null, null);
                }
            }
            else if (path.EndsWith(".png"))
            {
                //
            }

        }
        /// <summary>
        /// Displays a dialog box that prompts the user to save the selected reference image locally.
        /// </summary>
        /// <param name="sender">menustrip item</param>
        /// <param name="e">n/a</param>
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    File.WriteAllBytes(filePath, _ref_image_data);
                    MessageBox.Show("Image saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving the image: {ex.Message}");
                }
            }

            ref_img_menustrip.Visible = false;
        }

        /// <summary>
        /// This method is an exercise option for the user. This exercise is  
        /// 'Red-Lining', and the canvas is automatically set up for the user
        /// to begin drawing. A random reference image is chosen and placed on 
        /// the canvas with opacity reduced, a new layer is selected for drawing,
        /// and a red pen selected. TODO: add a hint/tip/directions for the practice.
        /// </summary>
        /// <param name="sender">menu strip item</param>
        /// <param name="e">n/a</param>
        private void redLiningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get a random image for the line drawing
            UnsplashImage img = _um.GetRandomImage();

            // clear canvas, add ref image to layer, add new layer for drawing
            clear_canvas_button_Click(null, null);
            Program.canvas_manager.DrawImageWithOpacity(img.Get_Image_Data(), canvas_frame, 0);
            layer_add_button_Click(null, null);

            // change pen settings
            red_input_box.Value = 255;
            green_input_box.Value = 0;
            blue_input_box.Value = 0;


            // change layer opacity settings
            temp_transparency_num_up_down.Value = (decimal)0.5;
            Program.canvas_manager.layer_manager.set_layer_opacity((float)temp_transparency_num_up_down.Value);

            // select the new layer
            foreach (Control c in layers_ui[layers_ui.Count - 1].Controls)
            {
                if (c is RadioButton r)
                {
                    layer_visible_button_clicked(r, new EventArgs());
                }
            }
        }

        /* 
         *  End of -- Image Selection and Database Related Code
         */
    }
}
