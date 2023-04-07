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
            this.exit_button = new System.Windows.Forms.Button();
            this.title_bar = new System.Windows.Forms.Panel();
            this.title_label = new System.Windows.Forms.Label();
            this.minimize_button = new System.Windows.Forms.Button();
            this.fullscreen_button = new System.Windows.Forms.Button();
            this.tool_bar = new System.Windows.Forms.Panel();
            this.left_settings_panel = new System.Windows.Forms.Panel();
            this.quick_launch_bar = new System.Windows.Forms.Panel();
            this.canvas_panel = new System.Windows.Forms.Panel();
            this.reference_img = new System.Windows.Forms.PictureBox();
            this.put_ref_button = new System.Windows.Forms.Button();
            this.get_ref_button = new System.Windows.Forms.Button();
            this.canvas_frame = new System.Windows.Forms.PictureBox();
            this.right_settings_panel = new System.Windows.Forms.Panel();
            this.title_bar.SuspendLayout();
            this.canvas_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).BeginInit();
            this.SuspendLayout();
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
            this.title_label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_label.AutoSize = true;
            this.title_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.title_label.Location = new System.Drawing.Point(610, 4);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(59, 13);
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
            this.tool_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.tool_bar.Enabled = false;
            this.tool_bar.Location = new System.Drawing.Point(0, 26);
            this.tool_bar.Name = "tool_bar";
            this.tool_bar.Size = new System.Drawing.Size(25, 687);
            this.tool_bar.TabIndex = 3;
            // 
            // left_settings_panel
            // 
            this.left_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.left_settings_panel.Enabled = false;
            this.left_settings_panel.Location = new System.Drawing.Point(30, 26);
            this.left_settings_panel.Name = "left_settings_panel";
            this.left_settings_panel.Size = new System.Drawing.Size(172, 694);
            this.left_settings_panel.TabIndex = 4;
            // 
            // quick_launch_bar
            // 
            this.quick_launch_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.quick_launch_bar.Location = new System.Drawing.Point(210, 26);
            this.quick_launch_bar.Name = "quick_launch_bar";
            this.quick_launch_bar.Size = new System.Drawing.Size(906, 36);
            this.quick_launch_bar.TabIndex = 5;
            // 
            // canvas_panel
            // 
            this.canvas_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.canvas_panel.Controls.Add(this.reference_img);
            this.canvas_panel.Controls.Add(this.put_ref_button);
            this.canvas_panel.Controls.Add(this.get_ref_button);
            this.canvas_panel.Controls.Add(this.canvas_frame);
            this.canvas_panel.Location = new System.Drawing.Point(210, 63);
            this.canvas_panel.Name = "canvas_panel";
            this.canvas_panel.Size = new System.Drawing.Size(906, 653);
            this.canvas_panel.TabIndex = 6;
            // 
            // reference_img
            // 
            this.reference_img.Location = new System.Drawing.Point(566, 176);
            this.reference_img.Name = "reference_img";
            this.reference_img.Size = new System.Drawing.Size(321, 300);
            this.reference_img.TabIndex = 3;
            this.reference_img.TabStop = false;
            // 
            // put_ref_button
            // 
            this.put_ref_button.Location = new System.Drawing.Point(736, 147);
            this.put_ref_button.Name = "put_ref_button";
            this.put_ref_button.Size = new System.Drawing.Size(75, 23);
            this.put_ref_button.TabIndex = 2;
            this.put_ref_button.Text = "add ref img";
            this.put_ref_button.UseVisualStyleBackColor = true;
            this.put_ref_button.Click += new System.EventHandler(this.put_ref_button_Click);
            // 
            // get_ref_button
            // 
            this.get_ref_button.Location = new System.Drawing.Point(812, 147);
            this.get_ref_button.Name = "get_ref_button";
            this.get_ref_button.Size = new System.Drawing.Size(75, 23);
            this.get_ref_button.TabIndex = 1;
            this.get_ref_button.Text = "get ref img";
            this.get_ref_button.UseVisualStyleBackColor = true;
            this.get_ref_button.Click += new System.EventHandler(this.get_ref_button_Click);
            // 
            // canvas_frame
            // 
            this.canvas_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.canvas_frame.Location = new System.Drawing.Point(0, 28);
            this.canvas_frame.Name = "canvas_frame";
            this.canvas_frame.Size = new System.Drawing.Size(906, 625);
            this.canvas_frame.TabIndex = 0;
            this.canvas_frame.TabStop = false;
            this.canvas_frame.Click += new System.EventHandler(this.canvas_frame_Click);
            // 
            // right_settings_panel
            // 
            this.right_settings_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.right_settings_panel.Location = new System.Drawing.Point(1123, 26);
            this.right_settings_panel.Name = "right_settings_panel";
            this.right_settings_panel.Size = new System.Drawing.Size(154, 694);
            this.right_settings_panel.TabIndex = 7;
            // 
            // main_window
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
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
            this.MinimumSize = new System.Drawing.Size(128, 128);
            this.Name = "main_window";
            this.Text = "Sketchpop";
            this.title_bar.ResumeLayout(false);
            this.title_bar.PerformLayout();
            this.canvas_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reference_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_frame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Panel title_bar;
        private System.Windows.Forms.Button fullscreen_button;
        private System.Windows.Forms.Button minimize_button;
        private System.Windows.Forms.Panel tool_bar;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Panel left_settings_panel;
        private System.Windows.Forms.Panel quick_launch_bar;
        private System.Windows.Forms.Panel canvas_panel;
        private System.Windows.Forms.Panel right_settings_panel;
        private System.Windows.Forms.PictureBox canvas_frame;

        // Database additions
        private System.Windows.Forms.PictureBox reference_img;
        private System.Windows.Forms.Button put_ref_button;
        private System.Windows.Forms.Button get_ref_button;
    }
}

