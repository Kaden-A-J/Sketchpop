namespace Sketchpop
{
    partial class Image_Layer_Options_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Image_Layer_Options_Form));
            this.options_panel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.add_layer_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.pb_panel = new System.Windows.Forms.Panel();
            this.image_pb = new System.Windows.Forms.PictureBox();
            this.layer_panel = new System.Windows.Forms.Panel();
            this.op_panel = new System.Windows.Forms.Panel();
            this.opacity_label = new System.Windows.Forms.Label();
            this.opacity_slider = new System.Windows.Forms.TrackBar();
            this.options_panel.SuspendLayout();
            this.pb_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_pb)).BeginInit();
            this.op_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_slider)).BeginInit();
            this.SuspendLayout();
            // 
            // options_panel
            // 
            this.options_panel.BackColor = System.Drawing.Color.Silver;
            this.options_panel.Controls.Add(this.flowLayoutPanel1);
            this.options_panel.Controls.Add(this.add_layer_button);
            this.options_panel.Controls.Add(this.save_button);
            this.options_panel.Controls.Add(this.pb_panel);
            this.options_panel.Controls.Add(this.layer_panel);
            this.options_panel.Controls.Add(this.op_panel);
            this.options_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.options_panel.Location = new System.Drawing.Point(0, 0);
            this.options_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.options_panel.Name = "options_panel";
            this.options_panel.Size = new System.Drawing.Size(1120, 829);
            this.options_panel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.DimGray;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(702, 18);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(386, 585);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // add_layer_button
            // 
            this.add_layer_button.Location = new System.Drawing.Point(831, 606);
            this.add_layer_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.add_layer_button.Name = "add_layer_button";
            this.add_layer_button.Size = new System.Drawing.Size(128, 35);
            this.add_layer_button.TabIndex = 4;
            this.add_layer_button.Text = "Add Layer";
            this.add_layer_button.UseVisualStyleBackColor = true;
            this.add_layer_button.Click += new System.EventHandler(this.add_layer_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(831, 775);
            this.save_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(128, 35);
            this.save_button.TabIndex = 2;
            this.save_button.Text = "Save Changes";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // pb_panel
            // 
            this.pb_panel.BackColor = System.Drawing.Color.Gray;
            this.pb_panel.Controls.Add(this.image_pb);
            this.pb_panel.Location = new System.Drawing.Point(15, 18);
            this.pb_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_panel.Name = "pb_panel";
            this.pb_panel.Size = new System.Drawing.Size(644, 585);
            this.pb_panel.TabIndex = 3;
            // 
            // image_pb
            // 
            this.image_pb.BackColor = System.Drawing.Color.Black;
            this.image_pb.Location = new System.Drawing.Point(0, 0);
            this.image_pb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.image_pb.Name = "image_pb";
            this.image_pb.Size = new System.Drawing.Size(644, 585);
            this.image_pb.TabIndex = 0;
            this.image_pb.TabStop = false;
            // 
            // layer_panel
            // 
            this.layer_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layer_panel.AutoScroll = true;
            this.layer_panel.BackColor = System.Drawing.Color.DimGray;
            this.layer_panel.Location = new System.Drawing.Point(702, 18);
            this.layer_panel.Margin = new System.Windows.Forms.Padding(0);
            this.layer_panel.Name = "layer_panel";
            this.layer_panel.Size = new System.Drawing.Size(386, 585);
            this.layer_panel.TabIndex = 2;
            this.layer_panel.Visible = false;
            // 
            // op_panel
            // 
            this.op_panel.BackColor = System.Drawing.Color.DarkGray;
            this.op_panel.Controls.Add(this.opacity_label);
            this.op_panel.Controls.Add(this.opacity_slider);
            this.op_panel.Location = new System.Drawing.Point(15, 612);
            this.op_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.op_panel.Name = "op_panel";
            this.op_panel.Size = new System.Drawing.Size(644, 198);
            this.op_panel.TabIndex = 1;
            // 
            // opacity_label
            // 
            this.opacity_label.AutoSize = true;
            this.opacity_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opacity_label.Location = new System.Drawing.Point(6, 5);
            this.opacity_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.opacity_label.Name = "opacity_label";
            this.opacity_label.Size = new System.Drawing.Size(120, 25);
            this.opacity_label.TabIndex = 1;
            this.opacity_label.Text = "Set Opacity:";
            // 
            // opacity_slider
            // 
            this.opacity_slider.LargeChange = 1;
            this.opacity_slider.Location = new System.Drawing.Point(123, 3);
            this.opacity_slider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opacity_slider.Maximum = 255;
            this.opacity_slider.Name = "opacity_slider";
            this.opacity_slider.RightToLeftLayout = true;
            this.opacity_slider.Size = new System.Drawing.Size(516, 69);
            this.opacity_slider.TabIndex = 0;
            this.opacity_slider.Value = 255;
            this.opacity_slider.Scroll += new System.EventHandler(this.opacity_slider_Scroll);
            // 
            // Image_Layer_Options_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 829);
            this.Controls.Add(this.options_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Image_Layer_Options_Form";
            this.Text = "Add Image to Canvas";
            this.options_panel.ResumeLayout(false);
            this.pb_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image_pb)).EndInit();
            this.op_panel.ResumeLayout(false);
            this.op_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_slider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel options_panel;
        private System.Windows.Forms.PictureBox image_pb;
        private System.Windows.Forms.Panel op_panel;
        private System.Windows.Forms.Panel layer_panel;
        private System.Windows.Forms.Label opacity_label;
        private System.Windows.Forms.TrackBar opacity_slider;
        private System.Windows.Forms.Panel pb_panel;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button add_layer_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}