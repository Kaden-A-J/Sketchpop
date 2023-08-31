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
            this.components = new System.ComponentModel.Container();
            this.RegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_button = new System.Windows.Forms.Button();
            this.title_bar = new System.Windows.Forms.Panel();
            this.title_label = new System.Windows.Forms.Label();
            this.minimize_button = new System.Windows.Forms.Button();
            this.fullscreen_button = new System.Windows.Forms.Button();
            this.tool_bar = new System.Windows.Forms.Panel();
            this.left_settings_panel = new System.Windows.Forms.Panel();
            this.img_form_button = new System.Windows.Forms.Button();
            this.reference_img = new System.Windows.Forms.PictureBox();
            this.db_status_label = new System.Windows.Forms.TextBox();
            this.color_display_box = new System.Windows.Forms.Panel();
            this.blue_label = new System.Windows.Forms.Label();
            this.search_picture_label = new System.Windows.Forms.Label();
            this.green_label = new System.Windows.Forms.Label();
            this.red_label = new System.Windows.Forms.Label();
            this.stroke_label = new System.Windows.Forms.Label();
            this.color_label = new System.Windows.Forms.Label();
            this.quick_launch_bar = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exercisesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muscleMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatedCirclesPracticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas_panel = new System.Windows.Forms.Panel();
            this.layers_box = new System.Windows.Forms.ListBox();
            this.clear_canvas_button = new System.Windows.Forms.Button();
            this.pen_button = new System.Windows.Forms.Button();
            this.eraser_button = new System.Windows.Forms.Button();
            this.canvas_frame = new System.Windows.Forms.PictureBox();
            this.right_settings_panel = new System.Windows.Forms.Panel();
            this.red_input_box = new System.Windows.Forms.NumericUpDown();
            this.green_input_box = new System.Windows.Forms.NumericUpDown();
            this.blue_input_box = new System.Windows.Forms.NumericUpDown();
            this.stroke_size_input_box = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.title_bar.SuspendLayout();
            this.left_settings_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).BeginInit();
            this.quick_launch_bar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.canvas_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_input_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_size_input_box)).BeginInit();
            this.SuspendLayout();
            // 
            // RegisterToolStripMenuItem
            // 
            this.RegisterToolStripMenuItem.Name = "RegisterToolStripMenuItem";
            this.RegisterToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
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
            this.title_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
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
            this.title_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_label.AutoSize = true;
            this.title_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.title_label.Location = new System.Drawing.Point(610, 4);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(86, 20);
            this.title_label.TabIndex = 4;
            this.title_label.Text = "Sketchpop";
            // 
            // minimize_button
            // 
            this.minimize_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimize_button.Location = new System.Drawing.Point(1220, 0);
            this.minimize_button.MaximumSize = new System.Drawing.Size(20, 20);
            this.minimize_button.MinimumSize = new System.Drawing.Size(20, 20);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(20, 20);
            this.minimize_button.TabIndex = 1;
            this.minimize_button.UseVisualStyleBackColor = true;
            this.minimize_button.Click += new System.EventHandler(this.minimize_button_Click);
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
            this.tool_bar.Enabled = false;
            this.tool_bar.Location = new System.Drawing.Point(0, 26);
            this.tool_bar.Name = "tool_bar";
            this.tool_bar.Size = new System.Drawing.Size(25, 687);
            this.tool_bar.TabIndex = 3;
            // 
            // left_settings_panel
            // 
            this.left_settings_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.left_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.left_settings_panel.Controls.Add(this.img_form_button);
            this.left_settings_panel.Controls.Add(this.reference_img);
            this.left_settings_panel.Controls.Add(this.db_status_label);
            this.left_settings_panel.Controls.Add(this.color_display_box);
            this.left_settings_panel.Controls.Add(this.blue_label);
            this.left_settings_panel.Controls.Add(this.search_picture_label);
            this.left_settings_panel.Controls.Add(this.green_label);
            this.left_settings_panel.Controls.Add(this.red_label);
            this.left_settings_panel.Controls.Add(this.stroke_label);
            this.left_settings_panel.Controls.Add(this.color_label);
            this.left_settings_panel.Location = new System.Drawing.Point(30, 26);
            this.left_settings_panel.Name = "left_settings_panel";
            this.left_settings_panel.Size = new System.Drawing.Size(172, 694);
            this.left_settings_panel.TabIndex = 4;
            // 
            // img_form_button
            // 
            this.img_form_button.Location = new System.Drawing.Point(97, 130);
            this.img_form_button.Name = "img_form_button";
            this.img_form_button.Size = new System.Drawing.Size(72, 23);
            this.img_form_button.TabIndex = 4;
            this.img_form_button.Text = "Browse";
            this.img_form_button.UseVisualStyleBackColor = true;
            this.img_form_button.Click += new System.EventHandler(this.img_form_buttonClick);
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
            this.reference_img.UseWaitCursor = true;
            // 
            // db_status_label
            // 
            this.db_status_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.db_status_label.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.db_status_label.Location = new System.Drawing.Point(7, 165);
            this.db_status_label.Name = "db_status_label";
            this.db_status_label.Size = new System.Drawing.Size(157, 19);
            this.db_status_label.TabIndex = 0;
            this.db_status_label.UseWaitCursor = true;
            this.db_status_label.Visible = false;
            // 
            // color_display_box
            // 
            this.color_display_box.BackColor = System.Drawing.Color.Black;
            this.color_display_box.Location = new System.Drawing.Point(131, 289);
            this.color_display_box.Name = "color_display_box";
            this.color_display_box.Size = new System.Drawing.Size(31, 19);
            this.color_display_box.TabIndex = 1;
            this.color_display_box.UseWaitCursor = true;
            // 
            // blue_label
            // 
            this.blue_label.AutoSize = true;
            this.blue_label.Location = new System.Drawing.Point(109, 247);
            this.blue_label.Name = "blue_label";
            this.blue_label.Size = new System.Drawing.Size(39, 20);
            this.blue_label.TabIndex = 0;
            this.blue_label.Text = "blue";
            this.blue_label.UseWaitCursor = true;
            // 
            // search_picture_label
            // 
            this.search_picture_label.AutoSize = true;
            this.search_picture_label.Location = new System.Drawing.Point(3, 135);
            this.search_picture_label.Name = "search_picture_label";
            this.search_picture_label.Size = new System.Drawing.Size(144, 20);
            this.search_picture_label.TabIndex = 0;
            this.search_picture_label.Text = "Search for Images:";
            // 
            // green_label
            // 
            this.green_label.AutoSize = true;
            this.green_label.Location = new System.Drawing.Point(54, 247);
            this.green_label.Name = "green_label";
            this.green_label.Size = new System.Drawing.Size(50, 20);
            this.green_label.TabIndex = 0;
            this.green_label.Text = "green";
            this.green_label.UseWaitCursor = true;
            // 
            // red_label
            // 
            this.red_label.AutoSize = true;
            this.red_label.Location = new System.Drawing.Point(6, 247);
            this.red_label.Name = "red_label";
            this.red_label.Size = new System.Drawing.Size(32, 20);
            this.red_label.TabIndex = 0;
            this.red_label.Text = "red";
            this.red_label.UseWaitCursor = true;
            // 
            // stroke_label
            // 
            this.stroke_label.AutoSize = true;
            this.stroke_label.Location = new System.Drawing.Point(3, 306);
            this.stroke_label.Name = "stroke_label";
            this.stroke_label.Size = new System.Drawing.Size(95, 20);
            this.stroke_label.TabIndex = 0;
            this.stroke_label.Text = "Stroke Size:";
            this.stroke_label.UseWaitCursor = true;
            // 
            // color_label
            // 
            this.color_label.AutoSize = true;
            this.color_label.Location = new System.Drawing.Point(3, 234);
            this.color_label.Name = "color_label";
            this.color_label.Size = new System.Drawing.Size(50, 20);
            this.color_label.TabIndex = 0;
            this.color_label.Text = "Color:";
            this.color_label.UseWaitCursor = true;
            // 
            // quick_launch_bar
            // 
            this.quick_launch_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_launch_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.quick_launch_bar.Controls.Add(this.menuStrip1);
            this.quick_launch_bar.Location = new System.Drawing.Point(210, 26);
            this.quick_launch_bar.Name = "quick_launch_bar";
            this.quick_launch_bar.Size = new System.Drawing.Size(906, 36);
            this.quick_launch_bar.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exercisesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(906, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exercisesToolStripMenuItem
            // 
            this.exercisesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muscleMemoryToolStripMenuItem});
            this.exercisesToolStripMenuItem.Name = "exercisesToolStripMenuItem";
            this.exercisesToolStripMenuItem.Size = new System.Drawing.Size(102, 28);
            this.exercisesToolStripMenuItem.Text = "Exercises";
            // 
            // muscleMemoryToolStripMenuItem
            // 
            this.muscleMemoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repeatedCirclesPracticeToolStripMenuItem});
            this.muscleMemoryToolStripMenuItem.Name = "muscleMemoryToolStripMenuItem";
            this.muscleMemoryToolStripMenuItem.Size = new System.Drawing.Size(249, 34);
            this.muscleMemoryToolStripMenuItem.Text = "Muscle Memory";
            // 
            // repeatedCirclesPracticeToolStripMenuItem
            // 
            this.repeatedCirclesPracticeToolStripMenuItem.Name = "repeatedCirclesPracticeToolStripMenuItem";
            this.repeatedCirclesPracticeToolStripMenuItem.Size = new System.Drawing.Size(327, 34);
            this.repeatedCirclesPracticeToolStripMenuItem.Text = "Repeated Circles Practice";
            this.repeatedCirclesPracticeToolStripMenuItem.Click += new System.EventHandler(this.repeatedCirclesPracticeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegisterToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(80, 28);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // canvas_panel
            // 
            this.canvas_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.canvas_panel.Controls.Add(this.layers_box);
            this.canvas_panel.Controls.Add(this.clear_canvas_button);
            this.canvas_panel.Controls.Add(this.pen_button);
            this.canvas_panel.Controls.Add(this.eraser_button);
            this.canvas_panel.Controls.Add(this.canvas_frame);
            this.canvas_panel.Location = new System.Drawing.Point(210, 63);
            this.canvas_panel.Name = "canvas_panel";
            this.canvas_panel.Size = new System.Drawing.Size(906, 653);
            this.canvas_panel.TabIndex = 6;
            // 
            // layers_box
            // 
            this.layers_box.FormattingEnabled = true;
            this.layers_box.Location = new System.Drawing.Point(497, 0);
            this.layers_box.Name = "layers_box";
            this.layers_box.Size = new System.Drawing.Size(114, 30);
            this.layers_box.Sorted = true;
            this.layers_box.TabIndex = 14;
            this.layers_box.SelectedIndexChanged += new System.EventHandler(this.layers_box_SelectedIndexChanged);
            // 
            // clear_canvas_button
            // 
            this.clear_canvas_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_canvas_button.Location = new System.Drawing.Point(828, 3);
            this.clear_canvas_button.Name = "clear_canvas_button";
            this.clear_canvas_button.Size = new System.Drawing.Size(75, 23);
            this.clear_canvas_button.TabIndex = 10;
            this.clear_canvas_button.Text = "Clear";
            this.clear_canvas_button.UseVisualStyleBackColor = true;
            this.clear_canvas_button.UseWaitCursor = true;
            this.clear_canvas_button.Click += new System.EventHandler(this.clear_canvas_button_Click);
            // 
            // pen_button
            // 
            this.pen_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pen_button.Location = new System.Drawing.Point(665, 3);
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
            this.eraser_button.Location = new System.Drawing.Point(747, 3);
            this.eraser_button.Name = "eraser_button";
            this.eraser_button.Size = new System.Drawing.Size(75, 23);
            this.eraser_button.TabIndex = 1;
            this.eraser_button.Text = "Eraser";
            this.eraser_button.UseVisualStyleBackColor = true;
            this.eraser_button.Click += new System.EventHandler(this.eraser_button_Click);
            // 
            // canvas_frame
            // 
            this.canvas_frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.canvas_frame.Location = new System.Drawing.Point(0, 28);
            this.canvas_frame.Name = "canvas_frame";
            this.canvas_frame.Size = new System.Drawing.Size(906, 625);
            this.canvas_frame.TabIndex = 0;
            this.canvas_frame.TabStop = false;
            this.canvas_frame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseDown);
            this.canvas_frame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseMove);
            this.canvas_frame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_frame_MouseUp);
            // 
            // right_settings_panel
            // 
            this.right_settings_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.right_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.right_settings_panel.Location = new System.Drawing.Point(1123, 26);
            this.right_settings_panel.Name = "right_settings_panel";
            this.right_settings_panel.Size = new System.Drawing.Size(154, 694);
            this.right_settings_panel.TabIndex = 7;
            // 
            // red_input_box
            // 
            this.red_input_box.Location = new System.Drawing.Point(36, 289);
            this.red_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.red_input_box.Name = "red_input_box";
            this.red_input_box.Size = new System.Drawing.Size(45, 26);
            this.red_input_box.TabIndex = 8;
            this.red_input_box.ValueChanged += new System.EventHandler(this.red_input_box_ValueChanged);
            // 
            // green_input_box
            // 
            this.green_input_box.Location = new System.Drawing.Point(87, 289);
            this.green_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.green_input_box.Name = "green_input_box";
            this.green_input_box.Size = new System.Drawing.Size(45, 26);
            this.green_input_box.TabIndex = 8;
            this.green_input_box.ValueChanged += new System.EventHandler(this.green_input_box_ValueChanged);
            // 
            // blue_input_box
            // 
            this.blue_input_box.Location = new System.Drawing.Point(142, 289);
            this.blue_input_box.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blue_input_box.Name = "blue_input_box";
            this.blue_input_box.Size = new System.Drawing.Size(45, 26);
            this.blue_input_box.TabIndex = 8;
            this.blue_input_box.ValueChanged += new System.EventHandler(this.blue_input_box_ValueChanged);
            // 
            // stroke_size_input_box
            // 
            this.stroke_size_input_box.Location = new System.Drawing.Point(36, 348);
            this.stroke_size_input_box.Name = "stroke_size_input_box";
            this.stroke_size_input_box.Size = new System.Drawing.Size(45, 26);
            this.stroke_size_input_box.TabIndex = 9;
            this.stroke_size_input_box.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stroke_size_input_box.ValueChanged += new System.EventHandler(this.stroke_size_input_box_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // main_window
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.ControlBox = false;
            this.Controls.Add(this.stroke_size_input_box);
            this.Controls.Add(this.blue_input_box);
            this.Controls.Add(this.green_input_box);
            this.Controls.Add(this.red_input_box);
            this.Controls.Add(this.right_settings_panel);
            this.Controls.Add(this.canvas_panel);
            this.Controls.Add(this.quick_launch_bar);
            this.Controls.Add(this.left_settings_panel);
            this.Controls.Add(this.tool_bar);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.fullscreen_button);
            this.Controls.Add(this.minimize_button);
            this.Controls.Add(this.title_bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(128, 128);
            this.Name = "main_window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sketchpop";
            this.title_bar.ResumeLayout(false);
            this.title_bar.PerformLayout();
            this.left_settings_panel.ResumeLayout(false);
            this.left_settings_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).EndInit();
            this.quick_launch_bar.ResumeLayout(false);
            this.quick_launch_bar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.canvas_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_input_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stroke_size_input_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox canvas_frame;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Panel title_bar;
        private System.Windows.Forms.Button fullscreen_button;
        private System.Windows.Forms.Button minimize_button;
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
        private System.Windows.Forms.Label color_label;
        private System.Windows.Forms.NumericUpDown red_input_box;
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox db_status_label;
        private System.Windows.Forms.Button clear_database_button;
        private System.Windows.Forms.Button b_layer_3;
        private System.Windows.Forms.Button b_layer_2;
        private System.Windows.Forms.Button b_layer_1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegisterToolStripMenuItem;
    }
}

