using System.Drawing;

namespace Sketchpop
{
    partial class main_window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_button = new System.Windows.Forms.Button();
            this.title_bar = new System.Windows.Forms.Panel();
            this.title_label = new System.Windows.Forms.Label();
            this.fullscreen_button = new System.Windows.Forms.Button();
            this.tool_bar = new System.Windows.Forms.Panel();
            this.hand_button = new System.Windows.Forms.Button();
            this.brush_button = new System.Windows.Forms.Button();
            this.left_settings_panel = new System.Windows.Forms.Panel();
            this.paintbrush_trackbar = new System.Windows.Forms.TrackBar();
            this.stroke_track_bar = new System.Windows.Forms.TrackBar();
            this.set_color_button = new System.Windows.Forms.Button();
            this.ref_img_options = new System.Windows.Forms.Button();
            this.green_input_box = new System.Windows.Forms.NumericUpDown();
            this.blue_input_box = new System.Windows.Forms.NumericUpDown();
            this.red_input_box = new System.Windows.Forms.NumericUpDown();
            this.stroke_size_input_box = new System.Windows.Forms.NumericUpDown();
            this.unsplash_link = new System.Windows.Forms.LinkLabel();
            this.on_label = new System.Windows.Forms.Label();
            this.author_link_label = new System.Windows.Forms.LinkLabel();
            this.pb_label = new System.Windows.Forms.Label();
            this.img_form_button = new System.Windows.Forms.Button();
            this.reference_img = new System.Windows.Forms.PictureBox();
            this.db_status_label = new System.Windows.Forms.TextBox();
            this.color_display_box = new System.Windows.Forms.Panel();
            this.blue_label = new System.Windows.Forms.Label();
            this.search_picture_label = new System.Windows.Forms.Label();
            this.green_label = new System.Windows.Forms.Label();
            this.red_label = new System.Windows.Forms.Label();
            this.stroke_label = new System.Windows.Forms.Label();
            this.quick_launch_bar = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exercisesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muscleMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatedCirclesPracticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomPromptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsEXCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas_panel = new System.Windows.Forms.Panel();
            this.ref_img_menustrip = new System.Windows.Forms.MenuStrip();
            this.viewImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brush_menustrip = new System.Windows.Forms.MenuStrip();
            this.penToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.painBrushStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear_canvas_button = new System.Windows.Forms.Button();
            this.pen_button = new System.Windows.Forms.Button();
            this.eraser_button = new System.Windows.Forms.Button();
            this.top_canvas_strip = new System.Windows.Forms.Panel();
            this.brush_selector_button = new System.Windows.Forms.Button();
            this.select_lasso_button = new System.Windows.Forms.Button();
            this.select_rect_button = new System.Windows.Forms.Button();
            this.drawing_picture_box = new System.Windows.Forms.PictureBox();
            this.canvas_frame = new System.Windows.Forms.PictureBox();
            this.right_settings_panel = new System.Windows.Forms.Panel();
            this.layer_delete_button = new System.Windows.Forms.Button();
            this.layer_add_button = new System.Windows.Forms.Button();
            this.layer_panel = new System.Windows.Forms.Panel();
            this.main_preview_picturebox = new System.Windows.Forms.PictureBox();
            this.title_bar.SuspendLayout();
            this.tool_bar.SuspendLayout();
            this.left_settings_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paintbrush_trackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_track_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_size_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).BeginInit();
            this.quick_launch_bar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.canvas_panel.SuspendLayout();
            this.ref_img_menustrip.SuspendLayout();
            this.brush_menustrip.SuspendLayout();
            this.top_canvas_strip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawing_picture_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).BeginInit();
            this.right_settings_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main_preview_picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // RegisterToolStripMenuItem
            // 
            this.RegisterToolStripMenuItem.Name = "RegisterToolStripMenuItem";
            this.RegisterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.RegisterToolStripMenuItem.Text = "Sign up/ Login";
            this.RegisterToolStripMenuItem.Click += new System.EventHandler(this.RegisterToolStripMenuItem_Click);
            // 
            // exit_button
            // 
            this.exit_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_button.Location = new System.Drawing.Point(1260, 0);
            this.exit_button.MaximumSize = new System.Drawing.Size(20, 20);
            this.exit_button.MinimumSize = new System.Drawing.Size(20, 20);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(20, 20);
            this.exit_button.TabIndex = 1;
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // title_bar
            // 
            this.title_bar.AutoSize = true;
            this.title_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.title_bar.CausesValidation = false;
            this.title_bar.Controls.Add(this.title_label);
            this.title_bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.title_bar.Enabled = false;
            this.title_bar.Location = new System.Drawing.Point(0, 0);
            this.title_bar.Name = "title_bar";
            this.title_bar.Size = new System.Drawing.Size(1280, 20);
            this.title_bar.TabIndex = 2;
            // 
            // title_label
            // 
            this.title_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_label.AutoSize = true;
            this.title_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.title_label.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.title_label.Location = new System.Drawing.Point(610, 4);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(59, 13);
            this.title_label.TabIndex = 4;
            this.title_label.Text = "Sketchpop";
            this.title_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // fullscreen_button
            // 
            this.fullscreen_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fullscreen_button.Location = new System.Drawing.Point(1240, 0);
            this.fullscreen_button.MaximumSize = new System.Drawing.Size(20, 20);
            this.fullscreen_button.MinimumSize = new System.Drawing.Size(20, 20);
            this.fullscreen_button.Name = "fullscreen_button";
            this.fullscreen_button.Size = new System.Drawing.Size(20, 20);
            this.fullscreen_button.TabIndex = 1;
            this.fullscreen_button.UseVisualStyleBackColor = true;
            this.fullscreen_button.Click += new System.EventHandler(this.maximize_button_Click);
            // 
            // tool_bar
            // 
            this.tool_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tool_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.tool_bar.Controls.Add(this.hand_button);
            this.tool_bar.Controls.Add(this.brush_button);
            this.tool_bar.Location = new System.Drawing.Point(0, 0);
            this.tool_bar.Name = "tool_bar";
            this.tool_bar.Size = new System.Drawing.Size(25, 687);
            this.tool_bar.TabIndex = 3;
            // 
            // hand_button
            // 
            this.hand_button.Location = new System.Drawing.Point(0, 25);
            this.hand_button.MaximumSize = new System.Drawing.Size(25, 25);
            this.hand_button.MinimumSize = new System.Drawing.Size(25, 25);
            this.hand_button.Name = "hand_button";
            this.hand_button.Size = new System.Drawing.Size(25, 25);
            this.hand_button.TabIndex = 12;
            this.hand_button.UseVisualStyleBackColor = true;
            this.hand_button.Click += new System.EventHandler(this.hand_button_Click);
            // 
            // brush_button
            // 
            this.brush_button.Location = new System.Drawing.Point(0, 0);
            this.brush_button.MaximumSize = new System.Drawing.Size(25, 25);
            this.brush_button.MinimumSize = new System.Drawing.Size(25, 25);
            this.brush_button.Name = "brush_button";
            this.brush_button.Size = new System.Drawing.Size(25, 25);
            this.brush_button.TabIndex = 12;
            this.brush_button.UseVisualStyleBackColor = true;
            this.brush_button.Click += new System.EventHandler(this.brush_button_Click);
            // 
            // left_settings_panel
            // 
            this.left_settings_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.left_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.left_settings_panel.Controls.Add(this.paintbrush_trackbar);
            this.left_settings_panel.Controls.Add(this.stroke_track_bar);
            this.left_settings_panel.Controls.Add(this.set_color_button);
            this.left_settings_panel.Controls.Add(this.ref_img_options);
            this.left_settings_panel.Controls.Add(this.green_input_box);
            this.left_settings_panel.Controls.Add(this.blue_input_box);
            this.left_settings_panel.Controls.Add(this.red_input_box);
            this.left_settings_panel.Controls.Add(this.stroke_size_input_box);
            this.left_settings_panel.Controls.Add(this.unsplash_link);
            this.left_settings_panel.Controls.Add(this.on_label);
            this.left_settings_panel.Controls.Add(this.author_link_label);
            this.left_settings_panel.Controls.Add(this.pb_label);
            this.left_settings_panel.Controls.Add(this.img_form_button);
            this.left_settings_panel.Controls.Add(this.reference_img);
            this.left_settings_panel.Controls.Add(this.db_status_label);
            this.left_settings_panel.Controls.Add(this.color_display_box);
            this.left_settings_panel.Controls.Add(this.blue_label);
            this.left_settings_panel.Controls.Add(this.search_picture_label);
            this.left_settings_panel.Controls.Add(this.green_label);
            this.left_settings_panel.Controls.Add(this.red_label);
            this.left_settings_panel.Controls.Add(this.stroke_label);
            this.left_settings_panel.Location = new System.Drawing.Point(32, 26);
            this.left_settings_panel.Name = "left_settings_panel";
            this.left_settings_panel.Size = new System.Drawing.Size(172, 690);
            this.left_settings_panel.TabIndex = 4;
            // 
            // paintbrush_trackbar
            // 
            this.paintbrush_trackbar.LargeChange = 1;
            this.paintbrush_trackbar.Location = new System.Drawing.Point(9, 274);
            this.paintbrush_trackbar.Maximum = 3;
            this.paintbrush_trackbar.Name = "paintbrush_trackbar";
            this.paintbrush_trackbar.RightToLeftLayout = true;
            this.paintbrush_trackbar.Size = new System.Drawing.Size(107, 45);
            this.paintbrush_trackbar.TabIndex = 22;
            this.paintbrush_trackbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.paintbrush_trackbar.Visible = false;
            this.paintbrush_trackbar.ValueChanged += new System.EventHandler(this.paintbrush_trackbar_ValueChanged);
            // 
            // stroke_track_bar
            // 
            this.stroke_track_bar.Location = new System.Drawing.Point(6, 274);
            this.stroke_track_bar.Maximum = 50;
            this.stroke_track_bar.Minimum = 1;
            this.stroke_track_bar.Name = "stroke_track_bar";
            this.stroke_track_bar.Size = new System.Drawing.Size(107, 45);
            this.stroke_track_bar.TabIndex = 19;
            this.stroke_track_bar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.stroke_track_bar.Value = 2;
            this.stroke_track_bar.ValueChanged += new System.EventHandler(this.stroke_track_bar_ValueChanged);
            // 
            // set_color_button
            // 
            this.set_color_button.Location = new System.Drawing.Point(51, 223);
            this.set_color_button.Name = "set_color_button";
            this.set_color_button.Size = new System.Drawing.Size(76, 19);
            this.set_color_button.TabIndex = 18;
            this.set_color_button.Text = "set color";
            this.set_color_button.UseVisualStyleBackColor = true;
            this.set_color_button.Click += new System.EventHandler(this.set_color_button_Click);
            // 
            // ref_img_options
            // 
            this.ref_img_options.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ref_img_options.Location = new System.Drawing.Point(146, 108);
            this.ref_img_options.Name = "ref_img_options";
            this.ref_img_options.Size = new System.Drawing.Size(22, 15);
            this.ref_img_options.TabIndex = 17;
            this.ref_img_options.Text = "...";
            this.ref_img_options.UseVisualStyleBackColor = true;
            this.ref_img_options.Visible = false;
            this.ref_img_options.Click += new System.EventHandler(this.ref_img_options_Click);
            // 
            // green_input_box
            // 
            this.green_input_box.Location = new System.Drawing.Point(57, 197);
            this.green_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.green_input_box.Name = "green_input_box";
            this.green_input_box.Size = new System.Drawing.Size(45, 20);
            this.green_input_box.TabIndex = 8;
            this.green_input_box.ValueChanged += new System.EventHandler(this.green_input_box_ValueChanged);
            // 
            // blue_input_box
            // 
            this.blue_input_box.Location = new System.Drawing.Point(108, 197);
            this.blue_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blue_input_box.Name = "blue_input_box";
            this.blue_input_box.Size = new System.Drawing.Size(45, 20);
            this.blue_input_box.TabIndex = 8;
            this.blue_input_box.ValueChanged += new System.EventHandler(this.blue_input_box_ValueChanged);
            // 
            // red_input_box
            // 
            this.red_input_box.Location = new System.Drawing.Point(6, 197);
            this.red_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.red_input_box.Name = "red_input_box";
            this.red_input_box.Size = new System.Drawing.Size(45, 20);
            this.red_input_box.TabIndex = 8;
            this.red_input_box.ValueChanged += new System.EventHandler(this.red_input_box_ValueChanged);
            // 
            // stroke_size_input_box
            // 
            this.stroke_size_input_box.Location = new System.Drawing.Point(119, 275);
            this.stroke_size_input_box.Name = "stroke_size_input_box";
            this.stroke_size_input_box.Size = new System.Drawing.Size(45, 20);
            this.stroke_size_input_box.TabIndex = 9;
            this.stroke_size_input_box.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stroke_size_input_box.ValueChanged += new System.EventHandler(this.stroke_size_input_box_ValueChanged);
            // 
            // unsplash_link
            // 
            this.unsplash_link.AutoSize = true;
            this.unsplash_link.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unsplash_link.Location = new System.Drawing.Point(110, 124);
            this.unsplash_link.Name = "unsplash_link";
            this.unsplash_link.Size = new System.Drawing.Size(44, 12);
            this.unsplash_link.TabIndex = 15;
            this.unsplash_link.TabStop = true;
            this.unsplash_link.Text = "Unsplash";
            this.unsplash_link.Visible = false;
            this.unsplash_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.unsplash_link_LinkClicked);
            // 
            // on_label
            // 
            this.on_label.AutoSize = true;
            this.on_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.on_label.Location = new System.Drawing.Point(95, 124);
            this.on_label.Name = "on_label";
            this.on_label.Size = new System.Drawing.Size(15, 12);
            this.on_label.TabIndex = 15;
            this.on_label.Text = "on";
            this.on_label.Visible = false;
            // 
            // author_link_label
            // 
            this.author_link_label.AutoSize = true;
            this.author_link_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author_link_label.Location = new System.Drawing.Point(49, 124);
            this.author_link_label.Name = "author_link_label";
            this.author_link_label.Size = new System.Drawing.Size(45, 12);
            this.author_link_label.TabIndex = 15;
            this.author_link_label.TabStop = true;
            this.author_link_label.Text = "12345678";
            this.author_link_label.Visible = false;
            this.author_link_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.author_link_label_LinkClicked);
            // 
            // pb_label
            // 
            this.pb_label.AutoSize = true;
            this.pb_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pb_label.Location = new System.Drawing.Point(7, 124);
            this.pb_label.Name = "pb_label";
            this.pb_label.Size = new System.Drawing.Size(41, 12);
            this.pb_label.TabIndex = 15;
            this.pb_label.Text = "Photo by";
            this.pb_label.Visible = false;
            // 
            // img_form_button
            // 
            this.img_form_button.Location = new System.Drawing.Point(97, 141);
            this.img_form_button.Name = "img_form_button";
            this.img_form_button.Size = new System.Drawing.Size(72, 23);
            this.img_form_button.TabIndex = 4;
            this.img_form_button.Text = "Browse";
            this.img_form_button.UseVisualStyleBackColor = true;
            this.img_form_button.Click += new System.EventHandler(this.img_form_buttonClick);
            this.img_form_button.MouseHover += new System.EventHandler(this.img_form_button_MouseHover);
            // 
            // reference_img
            // 
            this.reference_img.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reference_img.BackColor = System.Drawing.Color.Gray;
            this.reference_img.Location = new System.Drawing.Point(3, 3);
            this.reference_img.Name = "reference_img";
            this.reference_img.Size = new System.Drawing.Size(166, 121);
            this.reference_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.reference_img.TabIndex = 3;
            this.reference_img.TabStop = false;
            // 
            // db_status_label
            // 
            this.db_status_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.db_status_label.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.db_status_label.Location = new System.Drawing.Point(7, 165);
            this.db_status_label.Name = "db_status_label";
            this.db_status_label.Size = new System.Drawing.Size(157, 13);
            this.db_status_label.TabIndex = 0;
            this.db_status_label.Visible = false;
            // 
            // color_display_box
            // 
            this.color_display_box.BackColor = System.Drawing.Color.Black;
            this.color_display_box.Location = new System.Drawing.Point(133, 223);
            this.color_display_box.Name = "color_display_box";
            this.color_display_box.Size = new System.Drawing.Size(31, 19);
            this.color_display_box.TabIndex = 1;
            // 
            // blue_label
            // 
            this.blue_label.AutoSize = true;
            this.blue_label.Location = new System.Drawing.Point(109, 181);
            this.blue_label.Name = "blue_label";
            this.blue_label.Size = new System.Drawing.Size(27, 13);
            this.blue_label.TabIndex = 0;
            this.blue_label.Text = "blue";
            // 
            // search_picture_label
            // 
            this.search_picture_label.AutoSize = true;
            this.search_picture_label.Location = new System.Drawing.Point(3, 146);
            this.search_picture_label.Name = "search_picture_label";
            this.search_picture_label.Size = new System.Drawing.Size(96, 13);
            this.search_picture_label.TabIndex = 0;
            this.search_picture_label.Text = "Search for Images:";
            // 
            // green_label
            // 
            this.green_label.AutoSize = true;
            this.green_label.Location = new System.Drawing.Point(60, 181);
            this.green_label.Name = "green_label";
            this.green_label.Size = new System.Drawing.Size(34, 13);
            this.green_label.TabIndex = 0;
            this.green_label.Text = "green";
            // 
            // red_label
            // 
            this.red_label.AutoSize = true;
            this.red_label.Location = new System.Drawing.Point(6, 181);
            this.red_label.Name = "red_label";
            this.red_label.Size = new System.Drawing.Size(22, 13);
            this.red_label.TabIndex = 0;
            this.red_label.Text = "red";
            // 
            // stroke_label
            // 
            this.stroke_label.AutoSize = true;
            this.stroke_label.Location = new System.Drawing.Point(4, 258);
            this.stroke_label.Name = "stroke_label";
            this.stroke_label.Size = new System.Drawing.Size(64, 13);
            this.stroke_label.TabIndex = 0;
            this.stroke_label.Text = "Stroke Size:";
            // 
            // quick_launch_bar
            // 
            this.quick_launch_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_launch_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.quick_launch_bar.Controls.Add(this.menuStrip1);
            this.quick_launch_bar.Location = new System.Drawing.Point(210, 26);
            this.quick_launch_bar.Name = "quick_launch_bar";
            this.quick_launch_bar.Size = new System.Drawing.Size(767, 36);
            this.quick_launch_bar.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exercisesToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(767, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exercisesToolStripMenuItem
            // 
            this.exercisesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muscleMemoryToolStripMenuItem,
            this.randomPromptToolStripMenuItem});
            this.exercisesToolStripMenuItem.Name = "exercisesToolStripMenuItem";
            this.exercisesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.exercisesToolStripMenuItem.Text = "Exercises";
            // 
            // muscleMemoryToolStripMenuItem
            // 
            this.muscleMemoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repeatedCirclesPracticeToolStripMenuItem});
            this.muscleMemoryToolStripMenuItem.Name = "muscleMemoryToolStripMenuItem";
            this.muscleMemoryToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.muscleMemoryToolStripMenuItem.Text = "Muscle Memory";
            // 
            // repeatedCirclesPracticeToolStripMenuItem
            // 
            this.repeatedCirclesPracticeToolStripMenuItem.Name = "repeatedCirclesPracticeToolStripMenuItem";
            this.repeatedCirclesPracticeToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.repeatedCirclesPracticeToolStripMenuItem.Text = "Repeated Circles Practice";
            this.repeatedCirclesPracticeToolStripMenuItem.Click += new System.EventHandler(this.repeatedCirclesPracticeToolStripMenuItem_Click);
            // 
            // randomPromptToolStripMenuItem
            // 
            this.randomPromptToolStripMenuItem.Name = "randomPromptToolStripMenuItem";
            this.randomPromptToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.randomPromptToolStripMenuItem.Text = "Red Lining ";
            this.randomPromptToolStripMenuItem.Click += new System.EventHandler(this.redLiningToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegisterToolStripMenuItem,
            this.TipToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // TipToolStripMenuItem
            // 
            this.TipToolStripMenuItem.Name = "TipToolStripMenuItem";
            this.TipToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.TipToolStripMenuItem.Text = "Toggle Tips";
            this.TipToolStripMenuItem.Click += new System.EventHandler(this.TipToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsEXCToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsEXCToolStripMenuItem
            // 
            this.saveAsEXCToolStripMenuItem.Name = "saveAsEXCToolStripMenuItem";
            this.saveAsEXCToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAsEXCToolStripMenuItem.Text = "Save as EXC";
            this.saveAsEXCToolStripMenuItem.Click += new System.EventHandler(this.saveAsExcToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // canvas_panel
            // 
            this.canvas_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.canvas_panel.Controls.Add(this.ref_img_menustrip);
            this.canvas_panel.Controls.Add(this.brush_menustrip);
            this.canvas_panel.Controls.Add(this.clear_canvas_button);
            this.canvas_panel.Controls.Add(this.pen_button);
            this.canvas_panel.Controls.Add(this.eraser_button);
            this.canvas_panel.Controls.Add(this.top_canvas_strip);
            this.canvas_panel.Controls.Add(this.drawing_picture_box);
            this.canvas_panel.Controls.Add(this.canvas_frame);
            this.canvas_panel.Location = new System.Drawing.Point(210, 63);
            this.canvas_panel.Name = "canvas_panel";
            this.canvas_panel.Size = new System.Drawing.Size(767, 653);
            this.canvas_panel.TabIndex = 6;
            // 
            // ref_img_menustrip
            // 
            this.ref_img_menustrip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ref_img_menustrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ref_img_menustrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ref_img_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewImageToolStripMenuItem,
            this.addImageToLayerToolStripMenuItem,
            this.saveImageToolStripMenuItem});
            this.ref_img_menustrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.ref_img_menustrip.Location = new System.Drawing.Point(0, 71);
            this.ref_img_menustrip.Name = "ref_img_menustrip";
            this.ref_img_menustrip.Size = new System.Drawing.Size(128, 61);
            this.ref_img_menustrip.TabIndex = 11;
            this.ref_img_menustrip.Text = "menuStrip2";
            this.ref_img_menustrip.Visible = false;
            // 
            // viewImageToolStripMenuItem
            // 
            this.viewImageToolStripMenuItem.Name = "viewImageToolStripMenuItem";
            this.viewImageToolStripMenuItem.Size = new System.Drawing.Size(80, 19);
            this.viewImageToolStripMenuItem.Text = "View Image";
            this.viewImageToolStripMenuItem.Click += new System.EventHandler(this.viewImageToolStripMenuItem_Click);
            // 
            // addImageToLayerToolStripMenuItem
            // 
            this.addImageToLayerToolStripMenuItem.Name = "addImageToLayerToolStripMenuItem";
            this.addImageToLayerToolStripMenuItem.Size = new System.Drawing.Size(122, 19);
            this.addImageToLayerToolStripMenuItem.Text = "Add Image to Layer";
            this.addImageToLayerToolStripMenuItem.Click += new System.EventHandler(this.addImageToLayerToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(79, 19);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // brush_menustrip
            // 
            this.brush_menustrip.Dock = System.Windows.Forms.DockStyle.None;
            this.brush_menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.penToolStripMenuItem,
            this.painBrushStripMenuItem});
            this.brush_menustrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.brush_menustrip.Location = new System.Drawing.Point(510, 26);
            this.brush_menustrip.Name = "brush_menustrip";
            this.brush_menustrip.Size = new System.Drawing.Size(85, 42);
            this.brush_menustrip.TabIndex = 21;
            this.brush_menustrip.Visible = false;
            // 
            // penToolStripMenuItem
            // 
            this.penToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paintBrushToolStripMenuItem});
            this.penToolStripMenuItem.Name = "penToolStripMenuItem";
            this.penToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.penToolStripMenuItem.Text = "Pen";
            this.penToolStripMenuItem.Click += new System.EventHandler(this.penToolStripMenuItem_Click);
            // 
            // paintBrushToolStripMenuItem
            // 
            this.paintBrushToolStripMenuItem.Name = "paintBrushToolStripMenuItem";
            this.paintBrushToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.paintBrushToolStripMenuItem.Text = "Paint Brush";
            // 
            // painBrushStripMenuItem
            // 
            this.painBrushStripMenuItem.Name = "painBrushStripMenuItem";
            this.painBrushStripMenuItem.Size = new System.Drawing.Size(79, 19);
            this.painBrushStripMenuItem.Text = "Paint Brush";
            this.painBrushStripMenuItem.Click += new System.EventHandler(this.painBrushStripMenuItem_Click);
            // 
            // clear_canvas_button
            // 
            this.clear_canvas_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_canvas_button.Location = new System.Drawing.Point(689, 3);
            this.clear_canvas_button.Name = "clear_canvas_button";
            this.clear_canvas_button.Size = new System.Drawing.Size(75, 23);
            this.clear_canvas_button.TabIndex = 10;
            this.clear_canvas_button.Text = "Clear";
            this.clear_canvas_button.UseVisualStyleBackColor = true;
            this.clear_canvas_button.Click += new System.EventHandler(this.clear_canvas_button_Click);
            this.clear_canvas_button.MouseHover += new System.EventHandler(this.clear_canvas_button_MouseHover);
            // 
            // pen_button
            // 
            this.pen_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pen_button.Location = new System.Drawing.Point(526, 3);
            this.pen_button.Name = "pen_button";
            this.pen_button.Size = new System.Drawing.Size(75, 23);
            this.pen_button.TabIndex = 2;
            this.pen_button.Text = "Pen";
            this.pen_button.UseVisualStyleBackColor = true;
            this.pen_button.Click += new System.EventHandler(this.pen_button_Click);
            // 
            // eraser_button
            // 
            this.eraser_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eraser_button.Location = new System.Drawing.Point(608, 3);
            this.eraser_button.Name = "eraser_button";
            this.eraser_button.Size = new System.Drawing.Size(75, 23);
            this.eraser_button.TabIndex = 1;
            this.eraser_button.Text = "Eraser";
            this.eraser_button.UseVisualStyleBackColor = true;
            this.eraser_button.Click += new System.EventHandler(this.eraser_button_Click);
            // 
            // top_canvas_strip
            // 
            this.top_canvas_strip.Controls.Add(this.brush_selector_button);
            this.top_canvas_strip.Controls.Add(this.select_lasso_button);
            this.top_canvas_strip.Controls.Add(this.select_rect_button);
            this.top_canvas_strip.Location = new System.Drawing.Point(0, 0);
            this.top_canvas_strip.Name = "top_canvas_strip";
            this.top_canvas_strip.Size = new System.Drawing.Size(767, 28);
            this.top_canvas_strip.TabIndex = 20;
            // 
            // brush_selector_button
            // 
            this.brush_selector_button.Location = new System.Drawing.Point(508, 3);
            this.brush_selector_button.Name = "brush_selector_button";
            this.brush_selector_button.Size = new System.Drawing.Size(22, 23);
            this.brush_selector_button.TabIndex = 21;
            this.brush_selector_button.Text = "▼";
            this.brush_selector_button.UseVisualStyleBackColor = true;
            this.brush_selector_button.Click += new System.EventHandler(this.brush_selector_button_Click);
            // 
            // select_lasso_button
            // 
            this.select_lasso_button.Location = new System.Drawing.Point(414, 2);
            this.select_lasso_button.Name = "select_lasso_button";
            this.select_lasso_button.Size = new System.Drawing.Size(88, 23);
            this.select_lasso_button.TabIndex = 19;
            this.select_lasso_button.Text = "Lasso Select";
            this.select_lasso_button.UseVisualStyleBackColor = true;
            this.select_lasso_button.Click += new System.EventHandler(this.select_lasso_button_Click);
            // 
            // select_rect_button
            // 
            this.select_rect_button.Location = new System.Drawing.Point(333, 2);
            this.select_rect_button.Name = "select_rect_button";
            this.select_rect_button.Size = new System.Drawing.Size(75, 23);
            this.select_rect_button.TabIndex = 18;
            this.select_rect_button.Text = "Rect Select";
            this.select_rect_button.UseVisualStyleBackColor = true;
            this.select_rect_button.Click += new System.EventHandler(this.select_rect_button_Click);
            // 
            // drawing_picture_box
            // 
            this.drawing_picture_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.drawing_picture_box.Enabled = false;
            this.drawing_picture_box.Location = new System.Drawing.Point(0, 28);
            this.drawing_picture_box.Name = "drawing_picture_box";
            this.drawing_picture_box.Size = new System.Drawing.Size(767, 625);
            this.drawing_picture_box.TabIndex = 19;
            this.drawing_picture_box.TabStop = false;
            // 
            // canvas_frame
            // 
            this.canvas_frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.canvas_frame.Location = new System.Drawing.Point(0, 28);
            this.canvas_frame.Name = "canvas_frame";
            this.canvas_frame.Size = new System.Drawing.Size(767, 625);
            this.canvas_frame.TabIndex = 0;
            this.canvas_frame.TabStop = false;
            this.canvas_frame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseDown);
            this.canvas_frame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseUp);
            // 
            // right_settings_panel
            // 
            this.right_settings_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.right_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.right_settings_panel.Controls.Add(this.layer_delete_button);
            this.right_settings_panel.Controls.Add(this.layer_add_button);
            this.right_settings_panel.Controls.Add(this.layer_panel);
            this.right_settings_panel.Controls.Add(this.main_preview_picturebox);
            this.right_settings_panel.Location = new System.Drawing.Point(983, 26);
            this.right_settings_panel.Name = "right_settings_panel";
            this.right_settings_panel.Size = new System.Drawing.Size(294, 690);
            this.right_settings_panel.TabIndex = 7;
            // 
            // layer_delete_button
            // 
            this.layer_delete_button.Location = new System.Drawing.Point(36, 396);
            this.layer_delete_button.Name = "layer_delete_button";
            this.layer_delete_button.Size = new System.Drawing.Size(23, 23);
            this.layer_delete_button.TabIndex = 1;
            this.layer_delete_button.Text = "-";
            this.layer_delete_button.UseVisualStyleBackColor = true;
            this.layer_delete_button.Click += new System.EventHandler(this.layer_delete_button_Click);
            // 
            // layer_add_button
            // 
            this.layer_add_button.Location = new System.Drawing.Point(7, 396);
            this.layer_add_button.Name = "layer_add_button";
            this.layer_add_button.Size = new System.Drawing.Size(23, 23);
            this.layer_add_button.TabIndex = 1;
            this.layer_add_button.Text = "+";
            this.layer_add_button.UseVisualStyleBackColor = true;
            this.layer_add_button.Click += new System.EventHandler(this.layer_add_button_Click);
            // 
            // layer_panel
            // 
            this.layer_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layer_panel.AutoScroll = true;
            this.layer_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.layer_panel.Location = new System.Drawing.Point(7, 425);
            this.layer_panel.Name = "layer_panel";
            this.layer_panel.Size = new System.Drawing.Size(284, 258);
            this.layer_panel.TabIndex = 0;
            // 
            // main_preview_picturebox
            // 
            this.main_preview_picturebox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.main_preview_picturebox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.main_preview_picturebox.Location = new System.Drawing.Point(7, 3);
            this.main_preview_picturebox.Name = "main_preview_picturebox";
            this.main_preview_picturebox.Size = new System.Drawing.Size(278, 161);
            this.main_preview_picturebox.TabIndex = 0;
            this.main_preview_picturebox.TabStop = false;
            this.main_preview_picturebox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseDown);
            this.main_preview_picturebox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseUp);
            // 
            // main_window
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.ControlBox = false;
            this.Controls.Add(this.right_settings_panel);
            this.Controls.Add(this.canvas_panel);
            this.Controls.Add(this.quick_launch_bar);
            this.Controls.Add(this.left_settings_panel);
            this.Controls.Add(this.tool_bar);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.fullscreen_button);
            this.Controls.Add(this.title_bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(128, 128);
            this.Name = "main_window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sketchpop";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.main_window_KeyDown);
            this.title_bar.ResumeLayout(false);
            this.title_bar.PerformLayout();
            this.tool_bar.ResumeLayout(false);
            this.left_settings_panel.ResumeLayout(false);
            this.left_settings_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paintbrush_trackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_track_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_size_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).EndInit();
            this.quick_launch_bar.ResumeLayout(false);
            this.quick_launch_bar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.canvas_panel.ResumeLayout(false);
            this.canvas_panel.PerformLayout();
            this.ref_img_menustrip.ResumeLayout(false);
            this.ref_img_menustrip.PerformLayout();
            this.brush_menustrip.ResumeLayout(false);
            this.brush_menustrip.PerformLayout();
            this.top_canvas_strip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawing_picture_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).EndInit();
            this.right_settings_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.main_preview_picturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox canvas_frame;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Panel title_bar;
        private System.Windows.Forms.Button fullscreen_button;
        private System.Windows.Forms.Panel tool_bar;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Panel left_settings_panel;
        private System.Windows.Forms.Button img_form_button;
        private System.Windows.Forms.Panel quick_launch_bar;
        private System.Windows.Forms.Panel canvas_panel;
        private System.Windows.Forms.Panel right_settings_panel;

        // Database additions
        private System.Windows.Forms.PictureBox reference_img;
        private System.Windows.Forms.Label search_picture_label;
        private System.Windows.Forms.Label blue_label;
        private System.Windows.Forms.Label green_label;
        private System.Windows.Forms.Label red_label;
        private System.Windows.Forms.NumericUpDown green_input_box;
        private System.Windows.Forms.NumericUpDown blue_input_box;
        private System.Windows.Forms.Label stroke_label;
        private System.Windows.Forms.NumericUpDown stroke_size_input_box;
        private System.Windows.Forms.Panel color_display_box;
        private System.Windows.Forms.Button clear_canvas_button;
        private System.Windows.Forms.Button eraser_button;
        private System.Windows.Forms.Button pen_button;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exercisesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muscleMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatedCirclesPracticeToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox db_status_label;
        private System.Windows.Forms.Button clear_database_button;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegisterToolStripMenuItem;
        private System.Windows.Forms.Label on_label;
        private System.Windows.Forms.LinkLabel author_link_label;
        private System.Windows.Forms.Label pb_label;
        private System.Windows.Forms.LinkLabel unsplash_link;
        private System.Windows.Forms.Button layer_delete_button;
        private System.Windows.Forms.Button layer_add_button;
        private System.Windows.Forms.Panel layer_panel;
        private System.Windows.Forms.PictureBox main_preview_picturebox;
        private System.Windows.Forms.Button ref_img_options;
        private System.Windows.Forms.MenuStrip ref_img_menustrip;
        private System.Windows.Forms.ToolStripMenuItem viewImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addImageToLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.Button brush_button;
        private System.Windows.Forms.Button hand_button;
        private System.Windows.Forms.ToolStripMenuItem randomPromptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsEXCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Button select_rect_button;
        private System.Windows.Forms.PictureBox drawing_picture_box;
        private System.Windows.Forms.Panel top_canvas_strip;
        private System.Windows.Forms.Button set_color_button;
        private System.Windows.Forms.NumericUpDown red_input_box;
        private System.Windows.Forms.TrackBar stroke_track_bar;
        private System.Windows.Forms.Button select_lasso_button;
        private System.Windows.Forms.Button brush_selector_button;
        private System.Windows.Forms.MenuStrip brush_menustrip;
        private System.Windows.Forms.ToolStripMenuItem penToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paintBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem painBrushStripMenuItem;
        private System.Windows.Forms.TrackBar paintbrush_trackbar;
        private System.Windows.Forms.ToolStripMenuItem TipToolStripMenuItem;
    }
}

