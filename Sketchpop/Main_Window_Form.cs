using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Sketchpop.Image_Layer_Options_Form;
using static Sketchpop.Image_Search_Form;

namespace Sketchpop
{
    public partial class main_window : Form
    {
        public Point middle_drawing_start = new Point(0, 0);
        public Timer draw_timer = new Timer();
        bool mouse_down = false;
        List<Panel> layers_ui;

        // reference image variables
        private string _author_link;
        private byte[] _ref_image_data;

        private int[] paint_brush_values = { 30, 50, 80, 100 };
        private bool eraser_selected = false;

        private Unsplash_Manager _um;
        private Canvas_Manager.SketchPopTool stored_brush = Canvas_Manager.SketchPopTool.brush;
        private ColorDialog _colorDialog = new ColorDialog();

        // Tip notification variables
        private bool _tips_toggled = false;
        private bool _tips_highlighted = false;
        private bool _starter_tip_shown;
        private Tip _curr;
        private List<Control> _tip_elements = new List<Control>();

        // random exercise variables
        private Random r = new Random();
        private List<string> _prompts;
        private string[] exercise_labels = { "Muscle Memory", "Red Lining", "Random Prompt", "Values" };

        // Sketchpop Exercise Images
        private List<byte[]> _value_examples = new List<byte[]>();
        private int examples_idx = 0;

        private bool bg_layer_added = false;
        private float drawing_box_stored_width, drawing_box_stored_height;

        public void draw_timer_method(object my_object, EventArgs my_event_args)
        {
            if (mouse_down)
            {
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.Mouse_Is_Still_Down_Handler(click_pos);
            }

            if (pen_button.Text.Equals("Pen") || eraser_selected)
                Program.canvas_manager.Draw_Path_Points(new object());
            else
                Program.canvas_manager.Draw_With_Brush(canvas_frame.PointToClient(MousePosition));
            Program.canvas_manager.Draw_Bitmap(drawing_picture_box, -1, false, true, Program.canvas_manager.stored_scale);
            Program.canvas_manager.Draw_Bitmap(main_preview_picturebox, -1, true);
            drawing_picture_box.Location = new Point(
                Program.canvas_manager.hand_difference.X + middle_drawing_start.X,
                Program.canvas_manager.hand_difference.Y + middle_drawing_start.Y);
        }

        public void render_layer_previews(object my_object, EventArgs my_event_args)
        {
            for (int idx = 0; idx < layers_ui.Count; idx++)
                foreach (Control c in layers_ui[idx].Controls)
                    if (c.Name == "preview_panel")
                    {
                        Program.canvas_manager.Draw_Bitmap((PictureBox)c, idx, true);
                    }
        }


        public main_window()
        {
            InitializeComponent();
            Program.canvas_manager = new Canvas_Manager();
            Application.AddMessageFilter(new PenPressureMessageFilter(Program.canvas_manager));
            int result = PenPressureMessageFilter.EnableMouseInPointer(true);

            layers_ui = new List<Panel>();
            _um = new Unsplash_Manager();
            this.redoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Enabled = false;
            canvas_frame.MouseWheel += mouse_scrolled;

            drawing_picture_box.Width = Program.canvas_manager.canvas_info.Width;
            drawing_picture_box.Height = Program.canvas_manager.canvas_info.Height;

            drawing_box_stored_width = drawing_picture_box.Width;
            drawing_box_stored_height = drawing_picture_box.Height;

            Program.canvas_manager.stored_scale = (float)canvas_frame.Width / (float)Program.canvas_manager.canvas_info.Width - 0.2f;
            Program.canvas_manager.Set_Main_Window(this);

            //Console.WriteLine(Program.canvas_manager.stored_scale);

            // add BG layer
            layer_add_button_Click(null, null);
            bg_layer_added = true;

            // add layer to make debugging easier
            layer_add_button_Click(null, null);

            // load exercise data
            Load_Prompts();
            Load_Exercise_Images();
            Palette_Setup();

            //middle_drawing_start = new Point(
            //    (int)((Program.canvas_manager.stored_scale * (canvas_panel.Width / 2)) - (Program.canvas_manager.stored_scale * (Program.canvas_manager.canvas_info.Width / 2))),
            //    (int)((Program.canvas_manager.stored_scale * (canvas_panel.Height / 2)) - (Program.canvas_manager.stored_scale * (Program.canvas_manager.canvas_info.Height / 2))) - 28);

            middle_drawing_start = new Point((int)((canvas_panel.Width / 2) - (Program.canvas_manager.stored_scale * (Program.canvas_manager.canvas_info.Width / 2))),
                (int)((canvas_panel.Width / 2) - (Program.canvas_manager.stored_scale * (Program.canvas_manager.canvas_info.Height / 2))) - 28 * 2);

            Program.canvas_manager.middle_drawing_start = middle_drawing_start;

            draw_timer.Tick += new EventHandler(draw_timer_method);
            draw_timer.Tick += new EventHandler(render_layer_previews);
            draw_timer.Interval = 8; // 11.111... ms is 90 fps
            draw_timer.Start();
        }

        //private void Add_Tip_Elements()
        //{
        //    clear_canvas_button.Paint += buttonToHighlight_Paint;
        //}

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
            // close menus that are open
            if (brush_menustrip.Visible) { brush_menustrip.Visible = false; }
            if (ref_img_menustrip.Visible) { ref_img_menustrip.Visible = false; }

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (!mouse_down)
                {
                    mouse_down = true;
                    var click_pos = canvas_frame.PointToClient(MousePosition);
                    Program.canvas_manager.Mouse_Down_Handler(click_pos);
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                //Console.WriteLine("middle down");
                stored_brush = Program.canvas_manager.current_tool; // this technically shouldn't be done here, but whatever
                Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.hand;

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
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                mouse_down = false;
                var click_pos = canvas_frame.PointToClient(MousePosition);
                Program.canvas_manager.Mouse_Up_Handler(click_pos);
                if (Program.canvas_manager.operation_manager.undo_stack_empty)
                    undoToolStripMenuItem.Enabled = false;
                else
                    undoToolStripMenuItem.Enabled = true;
                if (!Program.canvas_manager.operation_manager.redo_stack_empty)
                    redoToolStripMenuItem.Enabled = true;
                else
                    redoToolStripMenuItem.Enabled = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                //Console.WriteLine("middle up");
                Program.canvas_manager.current_tool = stored_brush;

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

            Program.canvas_manager.Reset_Canvas_State();

            // adds base layer
            bg_layer_added = false;
            layer_add_button_Click(null, null);
            bg_layer_added = true;

            // add drawable layer
            layer_add_button_Click(null, null);
        }

        private void eraser_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
            Program.canvas_manager.Change_Brush("eraser", stroke_size_input_box);
            eraser_selected = true;
        }

        private void pen_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;

            if (pen_button.Text.Equals("Paint Brush"))
                Program.canvas_manager.Change_Brush("paint");
            else if (pen_button.Text.Equals("Pen"))
                Program.canvas_manager.Change_Brush("basic");

            eraser_selected = false;
        }

        private void select_rect_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.selector_rect;
        }

        private void select_lasso_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.selector_lasso;
        }

        private void repeatedCirclesPracticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear the exercise controls:
            exercise_arrow_buttons.Controls.Clear();
            clear_canvas_button_Click(null, null);

            var options_form = new Repeated_Circles_Options_Form(this);
            options_form.ShowDialog();

            if (_tips_toggled && _curr == null)
            {
                if (_curr != null)
                    _curr.Close();

                var tmp = new Tip(this, canvas_frame, "Draw uniform circles between the blue lines.", 1, true, "Repeated_Circles");
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }

            // reset exercises logic (if needed)
            for (int i = 0; i < exercisesToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (!exercisesToolStripMenuItem.DropDownItems[i].Text.Equals("Muscle Memory"))
                {
                    exercisesToolStripMenuItem.DropDownItems[i].Text = exercise_labels[i];
                }
            }

            valuesToolStripMenuItem.Text = "Muscle Memory  [ACTIVE]";

            if (prompt.Visible && prompt_link.Visible)
            {
                prompt.Hide();
                prompt_link.Hide();
            }
        }


        private void RegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login_window login = new login_window();
            login.Show();
        }

        public void layer_visible_button_clicked(object my_object, EventArgs my_event_args)
        {
            RadioButton c_button = (RadioButton)(my_object);
            Panel c_panel = (Panel)(c_button).Parent;
            int c_idx = int.Parse(c_panel.Name);

            // uncheck old selection
            foreach (Control c in layers_ui[Program.canvas_manager.layer_manager.selected_layer].Controls)
                if (c.GetType() == typeof(RadioButton))
                    ((RadioButton)c).Checked = false;

            c_button.Checked = true;

            Program.canvas_manager.layer_manager.selected_layer = c_idx;
        }

        public void layer_trackbar_scrolled(object my_object, EventArgs my_event_args)
        {
            TrackBar c_trackbar = (TrackBar)(my_object);
            Panel c_panel = (Panel)(c_trackbar).Parent;
            int c_idx = int.Parse(c_panel.Name);

            //Console.WriteLine((float)c_trackbar.Value);
            Program.canvas_manager.layer_manager.set_layer_opacity(c_idx, (float)c_trackbar.Value / 100);
        }

        public void shift_layers_down()
        {
            foreach (Panel c_panel in layers_ui)
                c_panel.Location = new Point(c_panel.Location.X, c_panel.Location.Y + c_panel.Height);
        }

        public void shift_layers_up()
        {
            foreach (Panel c_panel in layers_ui)
                c_panel.Location = new Point(c_panel.Location.X, c_panel.Location.Y - c_panel.Height);
        }


        public void layer_add_button_Click(object sender, EventArgs e)
        {
            shift_layers_down();

            int buffer = 4;
            Panel t_panel = new Panel();
            // panel name is its layer's index to help with layer selection
            t_panel.Name = layers_ui.Count.ToString();
            t_panel.BackColor = Color.FromArgb(255, 157, 157, 157);
            t_panel.Size = new Size(this.layer_panel.Width - buffer - 20, 60);
            //t_panel.Location = new Point(4, (t_panel.Height + buffer) * layers_ui.Count);
            t_panel.Location = new Point(4, buffer);


            RadioButton t_visible_button = new RadioButton();
            t_visible_button.Size = new Size(20, 20);
            t_visible_button.Location = new Point(buffer, t_panel.Height / 2 - t_visible_button.Height / 2);
            if (bg_layer_added)
            {
                t_panel.Controls.Add(t_visible_button);
            }

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
            t_name_label.Size = new Size(t_panel.Width - t_name_label.Location.X, t_name_label.Height);
            t_panel.Controls.Add(t_name_label);

            TrackBar t_trackbar = new TrackBar();
            t_trackbar.Location = new Point(t_name_label.Location.X + buffer, t_panel.Height - ((t_panel.Height - buffer * 2) / 2) - buffer - 1);
            t_trackbar.Size = new Size((t_panel.Width - t_trackbar.Location.X - buffer), (t_panel.Height - buffer * 2) / 2);
            t_trackbar.Minimum = 0;
            t_trackbar.Maximum = 100;
            t_trackbar.Value = 100;
            t_trackbar.TickStyle = TickStyle.Both;
            t_panel.Controls.Add(t_trackbar);


            layers_ui.Add(t_panel);
            this.layer_panel.Controls.Add(t_panel);

            t_trackbar.ValueChanged += new EventHandler(layer_trackbar_scrolled);

            if (bg_layer_added)
            {
                Program.canvas_manager.layer_manager.add_layer(Program.canvas_manager.canvas_info);
                int layer_index = Program.canvas_manager.layer_manager.count - 1;
                Program.canvas_manager.add_new_layer_in_operation_manager(layer_index);
                // autocheck the first layer
                if (layers_ui.Count == 2)
                    t_visible_button.Checked = true;

                t_visible_button.Click += new EventHandler(layer_visible_button_clicked);
            }
            else
            {
                Program.canvas_manager.layer_manager.add_permalocked_layer(Program.canvas_manager.canvas_info);
            }

            if (Program.canvas_manager.operation_manager.undo_stack_empty)
                undoToolStripMenuItem.Enabled = false;
            else
                undoToolStripMenuItem.Enabled = true;
            if (!Program.canvas_manager.operation_manager.redo_stack_empty)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
        }
        public void layer_add(int layer_index)
        {

            shift_layers_down();

            int buffer = 4;
            Panel t_panel = new Panel();
            // panel name is its layer's index to help with layer selection
            t_panel.Name = layers_ui.Count.ToString();
            t_panel.BackColor = Color.FromArgb(255, 157, 157, 157);
            t_panel.Size = new Size(this.layer_panel.Width - buffer - 20, 60);
            //t_panel.Location = new Point(4, (t_panel.Height + buffer) * layers_ui.Count);
            t_panel.Location = new Point(4, buffer);


            RadioButton t_visible_button = new RadioButton();
            t_visible_button.Size = new Size(20, 20);
            t_visible_button.Location = new Point(buffer, t_panel.Height / 2 - t_visible_button.Height / 2);
            if (bg_layer_added)
            {
                t_panel.Controls.Add(t_visible_button);
            }

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
            t_name_label.Size = new Size(t_panel.Width - t_name_label.Location.X, t_name_label.Height);
            t_panel.Controls.Add(t_name_label);

            TrackBar t_trackbar = new TrackBar();
            t_trackbar.Location = new Point(t_name_label.Location.X + buffer, t_panel.Height - ((t_panel.Height - buffer * 2) / 2) - buffer - 1);
            t_trackbar.Size = new Size((t_panel.Width - t_trackbar.Location.X - buffer), (t_panel.Height - buffer * 2) / 2);
            t_trackbar.Minimum = 0;
            t_trackbar.Maximum = 100;
            t_trackbar.Value = 100;
            t_trackbar.TickStyle = TickStyle.Both;
            t_panel.Controls.Add(t_trackbar);


            layers_ui.Add(t_panel);
            this.layer_panel.Controls.Add(t_panel);

            t_trackbar.ValueChanged += new EventHandler(layer_trackbar_scrolled);

            if (bg_layer_added)
            {
                Program.canvas_manager.layer_manager.add_layer(Program.canvas_manager.canvas_info, layer_index);
                // autocheck the first layer
                if (layers_ui.Count == 2)
                    t_visible_button.Checked = true;

                t_visible_button.Click += new EventHandler(layer_visible_button_clicked);
            }
            else
            {
                Program.canvas_manager.layer_manager.add_permalocked_layer(Program.canvas_manager.canvas_info);
            }
        }

        // temp just delete the last layer, eventually change to delete a selection
        private void layer_delete_button_Click(object sender, EventArgs e)
        {
            if (layers_ui.Count < 2) { return; }

            shift_layers_up();

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

        public void layer_delete(int layer_index)
        {
            if (layers_ui.Count < 2) { return; }
            shift_layers_up();
            foreach (Control c in layers_ui[layer_index].Controls)
                if (c.GetType() == typeof(RadioButton))
                    ((RadioButton)c).Checked = true;
            Program.canvas_manager.layer_manager.delete_layer(layer_index);
            this.layer_panel.Controls.Remove(layers_ui[layer_index]);
            layers_ui.RemoveAt(layer_index);
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

        private void set_color_button_Click(object sender, EventArgs e)
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                red_input_box.Value = _colorDialog.Color.R;
                green_input_box.Value = _colorDialog.Color.G;
                blue_input_box.Value = _colorDialog.Color.B;
            }
        }

        private void stroke_track_bar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar track_bar = (sender as TrackBar);
            stroke_size_input_box.Value = track_bar.Value;
        }

        private void brush_selector_button_Click(object sender, EventArgs e)
        {
            brush_menustrip.Visible = true;
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pen_button.Text = "Pen";
            Program.canvas_manager.Change_Brush("basic");
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
            brush_menustrip.Visible = false;
            stroke_track_bar.Visible = true;
            paintbrush_trackbar.Visible = false;
            stroke_size_input_box.Visible = true;
            eraser_selected = false;
        }

        private void painBrushStripMenuItem_Click(object sender, EventArgs e)
        {
            pen_button.Text = "Paint Brush";
            Program.canvas_manager.Change_Brush("paint");
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
            brush_menustrip.Visible = false;
            stroke_track_bar.Visible = false;
            paintbrush_trackbar.Visible = true;
            stroke_size_input_box.Value = paint_brush_values[0];
            stroke_size_input_box.Visible = false;
            eraser_selected = false;
        }

        private void paintbrush_trackbar_ValueChanged(object sender, EventArgs e)
        {
            int index = paintbrush_trackbar.Value;
            int selectedValue = paint_brush_values[index];

            stroke_size_input_box.Value = selectedValue;

            //Program.canvas_manager.Update_Stroke_Size(selectedValue);
        }

        private void main_window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    Program.canvas_manager.Handle_Copy();
                }
                else if (e.KeyCode == Keys.X)
                {
                    Program.canvas_manager.Handle_Cut();
                }
                else if (e.KeyCode == Keys.V)
                {
                    Program.canvas_manager.Handle_Paste();
                }
                else if (e.KeyCode == Keys.Z)
                {
                    if (e.Shift)
                    {
                        if (redoToolStripMenuItem.Enabled)
                        {
                            redoToolStripMenuItem_Click(sender, e);
                        }
                    }
                    else 
                    {
                        if (undoToolStripMenuItem.Enabled)
                        {
                            undoToolStripMenuItem_Click(sender, e);
                        }
                    }
                }    
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Program.canvas_manager.Esc_Handler();
            }
        }

        /// <summary>
        /// The user clicks the LinkLabel associated with the photographer of the Image selected.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void author_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(_author_link);
        }

        /// <summary>
        /// Menustrip item for adding an image to a layer. Opens an ImageLayerOptions Form and sets its
        /// event handlers to be notified by the actions done by the user in the form.
        /// </summary>
        /// <param name="sender">menustrip item</param>
        /// <param name="e">n/a</param>
        private void addImageToLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp = new Image_Layer_Options_Form(_ref_image_data, layers_ui.Count);//Program.canvas_manager.Draw_Image_With_Opacity(_ref_image_data, canvas_frame, (float)0.5);
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
            Program.canvas_manager.Draw_Image_With_Opacity(modified_img, layer_index);

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

        private void zoom_canvas_in_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.stored_scale += 0.1f;
            if (Program.canvas_manager.stored_scale >= 1)
                Program.canvas_manager.stored_scale -= 0.1f;
            //Console.WriteLine(Program.canvas_manager.stored_scale);
            resize_drawing_picture_box();
        }

        private void zoom_canvas_out_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.stored_scale -= 0.1f;
            if (Program.canvas_manager.stored_scale <= 0.3)
                Program.canvas_manager.stored_scale += 0.1f;
            //Console.WriteLine(Program.canvas_manager.stored_scale);
            resize_drawing_picture_box();
        }

        private void mouse_scrolled(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom_canvas_in_button_Click(null, null);
            }
            else
            {
                zoom_canvas_out_button_Click(null, null);
            }
        }

        private void resize_drawing_picture_box()
        {
            float new_width = drawing_box_stored_width * Program.canvas_manager.stored_scale;
            float new_height = drawing_box_stored_height * Program.canvas_manager.stored_scale;
            drawing_picture_box.Size = new Size((int)new_width, (int)new_height);
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
            string path = "";
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
            // clear the exercise controls:
            exercise_arrow_buttons.Controls.Clear();

            // get a random image for the line drawing
            UnsplashImage img = _um.GetRandomImage();

            // clear canvas, add ref image to layer, add new layer for drawing
            clear_canvas_button_Click(null, null);
            Program.canvas_manager.Draw_Image_With_Opacity(img.Get_Image_Data(), 1);

            layer_add_button_Click(null, null);

            Program.canvas_manager.layer_manager.set_layer_opacity(0.5f);


            // select the new layer
            foreach (Control c in layers_ui[layers_ui.Count - 1].Controls)
            {
                if (c is RadioButton r)
                {
                    layer_visible_button_clicked(r, new EventArgs());
                }
            }

            // change pen settings
            red_input_box.Value = 255;
            green_input_box.Value = 0;
            blue_input_box.Value = 0;

            if (_tips_toggled && _curr == null)
            {
                if (_curr != null)
                    _curr.Close();

                var tmp = new Tip(this, canvas_frame, "Red-Line the image on the canvas.", 1, true, "Red_Lining");
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }

            // reset exercises logic (if needed)
            for (int i = 0; i < exercisesToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (!exercisesToolStripMenuItem.DropDownItems[i].Text.Equals("Red Lining"))
                {
                    exercisesToolStripMenuItem.DropDownItems[i].Text = exercise_labels[i];
                }
            }

            valuesToolStripMenuItem.Text = "Red Lining   [ACTIVE]";

            if (prompt.Visible && prompt_link.Visible)
            {
                prompt.Hide();
                prompt_link.Hide();
            }
        }

        private void randomPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear the exercise controls:
            exercise_arrow_buttons.Controls.Clear();
            clear_canvas_button_Click(null, null);

            Get_Random_Prompt();
            this.prompt_link.Show();
            this.prompt.Show();

            if (_tips_toggled && _curr == null)
            {
                if (_curr != null)
                    _curr.Close();

                var tmp = new Tip(this, prompt_link, "Click here to generate new prompts!", 1, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }

            // reset exercises logic (if needed)
            for (int i = 0; i < exercisesToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (!exercisesToolStripMenuItem.DropDownItems[i].Text.Equals("Random Prompt"))
                {
                    exercisesToolStripMenuItem.DropDownItems[i].Text = exercise_labels[i];
                }
            }

            randomPromptToolStripMenuItem.Text = "Random Prompt  [ACTIVE]";
        }

        private void Get_Random_Prompt()
        {
            string prompt = _prompts[r.Next(_prompts.Count)];

            this.prompt.Text = prompt;
        }

        private void Load_Prompts()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = Path.Combine(baseDirectory, "..\\..\\Random_Prompts\\prompts.txt");

            _prompts = File.ReadAllLines(filepath).ToList();
        }

        private void Load_Exercise_Images()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var imageFolder = Path.Combine(baseDirectory, $"..\\..\\Exercise_Images\\");

            foreach (string img_file in Directory.GetFiles(imageFolder, "*.*"))
            {
                byte[] bytes = File.ReadAllBytes(img_file);
                _value_examples.Add(bytes);
            }

            randomImageToolStripMenuItem.Click += Get_Random_Monochrome_Image;
            uploadImageToolStripMenuItem.Click += Get_Random_Monochrome_Image;
            sketchpopTutorialToolStripMenuItem.Click += Get_Random_Monochrome_Image;
        }

        /// <summary>
        /// A new Tip has been created, the current Tip gets set so that no other Tips may
        /// be created while the current one is visible.
        /// </summary>
        /// <param name="sender">a new Tip is created</param>
        /// <param name="t">the Tip that was created</param>
        public void Activate(object sender, Tip t)
        {
            _curr = t;
            _curr.closed += Deactivate_Tip;
            _curr.new_tip += Activate;
        }

        /// <summary>
        /// The Tip that is currently being shown has been closed, and the current Tip is 
        /// set to null, signaling that other Tips may now be shown.
        /// </summary>
        /// <param name="sender">a Tip has been closed</param>
        /// <param name="e">n/a</param>
        public void Deactivate_Tip(object sender, EventArgs e)
        {
            _curr = null;
        }

        private void saveAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save_file_window = new SaveFileDialog())
            {
                save_file_window.InitialDirectory = ".\\";
                save_file_window.Filter = "png files (*.png)|*.png";
                save_file_window.FilterIndex = 1;
                save_file_window.RestoreDirectory = true;
                if (save_file_window.ShowDialog() == DialogResult.OK)
                {
                    new File_Manager().Save_as_PNG(save_file_window.FileName, Program.canvas_manager.layer_manager);
                }
            }
        }

        private void saveIntoCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_Form save = new Save_Form();
            save.Show();
        }

        private void pen_pressure_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.use_pressure = !Program.canvas_manager.use_pressure;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Get_Random_Prompt();
        }

        /// <summary>
        /// When toggled, active Tips may be shown to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tips_toggled = !_tips_toggled;
            _tips_highlighted = !_tips_highlighted;

            if (_tips_toggled)
            {
                TipToolStripMenuItem.Text = "Toggle Tips    [ACTIVE]";

                if (_tips_highlighted)
                {
                    Add_Tip_Highlights();
                }
            }
            else
            {
                TipToolStripMenuItem.Text = "Toggle Tips";
                Remove_Tip_Highlights();
            }
        }

        private void Add_Tip_Highlights()
        {
            // add elements with tips
            _tip_elements.Add(clear_canvas_button);
            _tip_elements.Add(img_form_button);
            _tip_elements.Add(layer_add_button);
            _tip_elements.Add(layer_delete_button);
            _tip_elements.Add(palette_colors);

            foreach (Control c in _tip_elements)
            {
                c.Paint += buttonToHighlight_Paint;
                c.Invalidate();
            }
        }

        private void Remove_Tip_Highlights()
        {
            foreach (Control c in _tip_elements)
            {
                c.Paint -= buttonToHighlight_Paint;
                c.Invalidate();
            }
        }

        private void Color_Palette_Picker(object sender, EventArgs e)
        {
            Panel color = (Panel)sender;

            if (eraser_selected)
            {
                Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.brush;
                Program.canvas_manager.Change_Brush("basic");
            }

            red_input_box.Value = color.BackColor.R;
            green_input_box.Value = color.BackColor.G;
            blue_input_box.Value = color.BackColor.B;
        }

        private void Color_Palette_Set_Color(object sender, EventArgs e)
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                Panel p = (Panel)sender;
                p.BackColor = _colorDialog.Color;
                red_input_box.Value = p.BackColor.R;
                green_input_box.Value = p.BackColor.G;
                blue_input_box.Value = p.BackColor.B;

            }
        }

        private void Palette_Setup()
        {
            foreach (Control c in palette_colors.Controls)
            {
                c.Click += Color_Palette_Picker;
                c.DoubleClick += Color_Palette_Set_Color;
            }

            foreach (Control c in exercise_palette_colors.Controls)
            {
                c.Click += Color_Palette_Picker;
            }
        }

        private void end_exercise_button_Click(object sender, EventArgs e)
        {
            if (!color_palette.Visible)
                color_palette.Show();

            // reset exercises logic (if needed)
            for (int i = 0; i < exercisesToolStripMenuItem.DropDownItems.Count; i++)
            {
                exercisesToolStripMenuItem.DropDownItems[i].Text = exercise_labels[i];
            }            

            clear_canvas_button_Click(sender, e);
            exercise_controls.Hide();
            end_exercise_button.Hide();

            red_input_box.Value = color1.BackColor.R;
            green_input_box.Value = color1.BackColor.G;
            blue_input_box.Value = color1.BackColor.B;
        }

        private void fill_tool_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.fill;
            eraser_selected = false;
        }

        private void resize_canvas_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.canvas_info.Width = (int)(resize_canvas_input_x.Value);
            Program.canvas_manager.canvas_info.Height = (int)(resize_canvas_input_y.Value);
            clear_canvas_button_Click(sender, e);
        }

        private void hand_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.current_tool = Canvas_Manager.SketchPopTool.hand;
        }

        private void Get_Random_Monochrome_Image(object sender, EventArgs e)
        {
            valuesToolStripMenuItem.Text = "";

            ToolStripMenuItem t = (ToolStripMenuItem)sender;

            // clear the exercise controls, hide color palette:
            clear_canvas_button_Click(null, null);
            exercise_arrow_buttons.Controls.Clear();
            color_palette.Hide();
            exercise_controls.Show();
            end_exercise_button.Show();

            red_input_box.Value = ex_color1.BackColor.R;
            green_input_box.Value = ex_color1.BackColor.G;
            blue_input_box.Value = ex_color1.BackColor.B;

            switch (t.Text)
            {
                case "Random Image":
                    // get a random image for the line drawing
                    UnsplashImage img = _um.GetRandomImage();

                    byte[] black_and_white = ConvertToBlackAndWhite(img.Get_Image_Data());

                    // clear canvas, add ref image to layer, add new layer for drawing
                    clear_canvas_button_Click(null, null);
                    Program.canvas_manager.Draw_Image_With_Opacity(black_and_white, 1);
                    layer_add_button_Click(null, null);
                    Program.canvas_manager.layer_manager.set_layer_opacity(0.75f);


                    // select the new layer
                    foreach (Control c in layers_ui[layers_ui.Count - 1].Controls)
                    {
                        if (c is RadioButton r)
                        {
                            layer_visible_button_clicked(r, new EventArgs());
                        }
                    }

                    // show the monochromatic palette
                    exercise_palette.Show();

                    Button new_img_btn = new Button();
                    new_img_btn.Text = "New Image";
                    exercise_arrow_buttons.Controls.Add(new_img_btn);
                    new_img_btn.Click += (s, ev) =>
                    {
                        var new_img = _um.GetRandomImage();

                        var new_black_and_white = ConvertToBlackAndWhite(new_img.Get_Image_Data());

                        // clear canvas, add ref image to layer, add new layer for drawing
                        clear_canvas_button_Click(null, null);
                        Program.canvas_manager.Draw_Image_With_Opacity(new_black_and_white, 1);
                    };

                    break;
                case "Upload Image":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
                    openFileDialog.Multiselect = false;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName; // Gets the full file path
                        byte[] fileBytes = ConvertToBlackAndWhite(File.ReadAllBytes(filePath));

                        clear_canvas_button_Click(null, null);
                        Program.canvas_manager.Draw_Image_With_Opacity(fileBytes, 1);
                        layer_add_button_Click(null, null);
                        Program.canvas_manager.layer_manager.set_layer_opacity(0.75f);

                        // select the new layer
                        foreach (Control c in layers_ui[layers_ui.Count - 1].Controls)
                        {
                            if (c is RadioButton r)
                            {
                                layer_visible_button_clicked(r, new EventArgs());
                            }
                        }
                    }
                    break;
                case "Sketchpop Tutorial":
                    clear_canvas_button_Click(null, null);
                    Program.canvas_manager.Draw_Image_With_Opacity(_value_examples[0], 1);
                    layer_add_button_Click(null, null);
                    Program.canvas_manager.layer_manager.set_layer_opacity(0.75f);

                    // select the new layer
                    foreach (Control c in layers_ui[layers_ui.Count - 1].Controls)
                    {
                        if (c is RadioButton r)
                        {
                            layer_visible_button_clicked(r, new EventArgs());
                        }
                    }

                    Button left, right;

                    left = new Button();
                    left.Text = "←";
                    left.Dock = DockStyle.Left;
                    left.Click += (s, ev) =>
                    {
                        if (examples_idx > 0)
                        {
                            examples_idx--;
                            clear_canvas_button_Click(null, null);
                            Program.canvas_manager.Draw_Image_With_Opacity(_value_examples[examples_idx], 1);
                            layer_add_button_Click(null, null);
                        }
                    };

                    right = new Button();
                    right.Dock = DockStyle.Right;
                    right.Text = "→";
                    right.Click += (s, ev) =>
                    {
                        if (examples_idx < _value_examples.Count - 1)
                        {
                            examples_idx++;
                            clear_canvas_button_Click(null, null);
                            Program.canvas_manager.Draw_Image_With_Opacity(_value_examples[examples_idx], 1);
                            layer_add_button_Click(null, null);
                        }
                    };

                    // add controls
                    exercise_arrow_buttons.Controls.Add(left);
                    exercise_arrow_buttons.Controls.Add(right);
                    break;
            }

            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, canvas_frame, "You may only use 5 values to block in the colors of the image.", 1, true, "Values");
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }

            // show the monochromatic palette
            exercise_palette.Show();

            // reset exercises logic (if needed)
            for (int i = 0; i < exercisesToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (!exercisesToolStripMenuItem.DropDownItems[i].Text.Equals("Values"))
                {
                    exercisesToolStripMenuItem.DropDownItems[i].Text = exercise_labels[i];
                }
            }

            valuesToolStripMenuItem.Text = "Values   [ACTIVE]";

            if (prompt.Visible && prompt_link.Visible)
            {
                prompt.Hide();
                prompt_link.Hide();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Undo_Operation();
            if (Program.canvas_manager.operation_manager.undo_stack_empty)
                undoToolStripMenuItem.Enabled = false;
            if (!Program.canvas_manager.operation_manager.redo_stack_empty)
                redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Redo_Operation();
            if (Program.canvas_manager.operation_manager.redo_stack_empty)
                redoToolStripMenuItem.Enabled = false;
            if (!Program.canvas_manager.operation_manager.undo_stack_empty)
                undoToolStripMenuItem.Enabled = true;
        }

        private void muscleMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repeatedCirclesPracticeToolStripMenuItem_Click(sender, e);
        }

        public byte[] ConvertToBlackAndWhite(byte[] originalImageBytes)
        {
            using (MemoryStream ms = new MemoryStream(originalImageBytes))
            {
                using (Image originalImage = Image.FromStream(ms))
                {
                    // Create a new bitmap with the same dimensions as the original image
                    using (Bitmap grayscaleImage = new Bitmap(originalImage.Width, originalImage.Height))
                    {
                        using (Graphics g = Graphics.FromImage(grayscaleImage))
                        {
                            // Create a color matrix for grayscale conversion
                            ColorMatrix colorMatrix = new ColorMatrix(
                                new float[][] {
                            new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                            new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                            new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                            new float[] {0, 0, 0, 1, 0},
                            new float[] {0, 0, 0, 0, 1}
                                }
                            );

                            // Create an ImageAttributes object and set the color matrix
                            using (ImageAttributes attributes = new ImageAttributes())
                            {
                                attributes.SetColorMatrix(colorMatrix);

                                // Draw the original image on the new bitmap with the color matrix
                                g.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes);
                            }
                        }

                        // Save the resulting black and white image as a byte array
                        using (MemoryStream bwImageStream = new MemoryStream())
                        {
                            grayscaleImage.Save(bwImageStream, ImageFormat.Png); // You can choose a different image format if needed
                            return bwImageStream.ToArray();
                        }
                    }
                }
            }
        }

        /*
         * Add Tip Logic Here (add a mouse hover event to the element that you would like to add a Tip to).
         */
        private void main_window_Load(object sender, EventArgs e)
        {
            _starter_tip_shown = false;

            int centerX = canvas_frame.Left + canvas_frame.Width / 2;
            int centerY = canvas_frame.Top + canvas_frame.Height / 2;

            // Set the mouse position to the center of the specific control
            Cursor.Position = PointToScreen(new Point(centerX, centerY));
        }

        private void canvas_frame_MouseHover(object sender, EventArgs e)
        {
            if (!_starter_tip_shown)
            {
                _starter_tip_shown = true;

                var tmp = new Tip(this, place_holder1, "Helpful Tips Can Be Toggled Here.", 2, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void clear_canvas_button_MouseHover(object sender, EventArgs e)
        {
            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, clear_canvas_button, "Click here to clear the canvas and all layers.", 0, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void img_form_button_MouseHover(object sender, EventArgs e)
        {
            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, img_form_button, "Click here to search for reference images to draw!", 0, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void layer_add_button_MouseHover(object sender, EventArgs e)
        {
            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, layer_add_button, "Press this button to add a new top layer.", 0, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void layer_delete_button_MouseHover(object sender, EventArgs e)
        {
            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, layer_delete_button, "Press this button to delete the topmost layer.", 0, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void palette_colors_MouseHover(object sender, EventArgs e)
        {
            if (_tips_toggled && _curr == null)
            {
                var tmp = new Tip(this, color_palette, "Single-click to change pen color, Double-click to set new color.", 0, false);
                tmp.closed += Deactivate_Tip;
                tmp.new_tip += Activate;
                tmp.Show();
                _curr = tmp;
            }
        }

        private void buttonToHighlight_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Purple, 2);
            g.DrawRectangle(pen, new Rectangle(0, 0, control.Width - 1, control.Height - 1));
        }

        /*
         *  Hot Keys
         */

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                // Handle Ctrl + F logic here
                var _ims = new Image_Search_Form();

                _ims._image_selected += image_selected; // subscribe to the selected image even            
                _ims.StartPosition = FormStartPosition.CenterParent;

                _ims.ShowDialog();
                return true; // indicate that the key has been handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
