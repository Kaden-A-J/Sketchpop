namespace Sketchpop
{
    partial class Image_Search_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Image_Search_Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.user_images_button = new System.Windows.Forms.Button();
            this.remove_fav_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.view_fav_button = new System.Windows.Forms.Button();
            this.add_fav_button = new System.Windows.Forms.Button();
            this.multi_image_selection = new System.Windows.Forms.CheckBox();
            this.select_button = new System.Windows.Forms.Button();
            this.help_button = new System.Windows.Forms.Button();
            this.search_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.search_button = new System.Windows.Forms.Button();
            this.images_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.upload_button = new System.Windows.Forms.Button();
            this.num_images_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.user_images_button);
            this.panel1.Controls.Add(this.remove_fav_button);
            this.panel1.Controls.Add(this.cancel_button);
            this.panel1.Controls.Add(this.view_fav_button);
            this.panel1.Controls.Add(this.add_fav_button);
            this.panel1.Controls.Add(this.multi_image_selection);
            this.panel1.Controls.Add(this.select_button);
            this.panel1.Controls.Add(this.help_button);
            this.panel1.Controls.Add(this.search_textbox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.search_button);
            this.panel1.Controls.Add(this.images_panel);
            this.panel1.Location = new System.Drawing.Point(18, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 655);
            this.panel1.TabIndex = 0;
            // 
            // user_images_button
            // 
            this.user_images_button.Location = new System.Drawing.Point(646, 12);
            this.user_images_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.user_images_button.Name = "user_images_button";
            this.user_images_button.Size = new System.Drawing.Size(126, 35);
            this.user_images_button.TabIndex = 4;
            this.user_images_button.Text = "My Images";
            this.user_images_button.UseVisualStyleBackColor = true;
            this.user_images_button.Click += new System.EventHandler(this.user_images_button_Click);
            // 
            // remove_fav_button
            // 
            this.remove_fav_button.Location = new System.Drawing.Point(506, 603);
            this.remove_fav_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.remove_fav_button.Name = "remove_fav_button";
            this.remove_fav_button.Size = new System.Drawing.Size(132, 35);
            this.remove_fav_button.TabIndex = 9;
            this.remove_fav_button.Text = "Remove";
            this.remove_fav_button.UseVisualStyleBackColor = true;
            this.remove_fav_button.Visible = false;
            this.remove_fav_button.Click += new System.EventHandler(this.remove_fav_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(28, 603);
            this.cancel_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(78, 35);
            this.cancel_button.TabIndex = 8;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // view_fav_button
            // 
            this.view_fav_button.Location = new System.Drawing.Point(512, 11);
            this.view_fav_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.view_fav_button.Name = "view_fav_button";
            this.view_fav_button.Size = new System.Drawing.Size(126, 35);
            this.view_fav_button.TabIndex = 7;
            this.view_fav_button.Text = "View Favorites";
            this.view_fav_button.UseVisualStyleBackColor = true;
            this.view_fav_button.Click += new System.EventHandler(this.view_fav_button_Click);
            // 
            // add_fav_button
            // 
            this.add_fav_button.Location = new System.Drawing.Point(357, 602);
            this.add_fav_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.add_fav_button.Name = "add_fav_button";
            this.add_fav_button.Size = new System.Drawing.Size(140, 35);
            this.add_fav_button.TabIndex = 6;
            this.add_fav_button.Text = "Add to Favorites";
            this.add_fav_button.UseVisualStyleBackColor = true;
            this.add_fav_button.Visible = false;
            this.add_fav_button.Click += new System.EventHandler(this.add_fav_button_Click);
            // 
            // multi_image_selection
            // 
            this.multi_image_selection.AutoSize = true;
            this.multi_image_selection.Location = new System.Drawing.Point(663, 602);
            this.multi_image_selection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.multi_image_selection.Name = "multi_image_selection";
            this.multi_image_selection.Size = new System.Drawing.Size(195, 24);
            this.multi_image_selection.TabIndex = 3;
            this.multi_image_selection.Text = "Select Multiple Images";
            this.multi_image_selection.UseVisualStyleBackColor = true;
            this.multi_image_selection.Click += new System.EventHandler(this.multi_image_selection_Click);
            // 
            // select_button
            // 
            this.select_button.BackColor = System.Drawing.Color.White;
            this.select_button.Enabled = false;
            this.select_button.Location = new System.Drawing.Point(236, 600);
            this.select_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.select_button.Name = "select_button";
            this.select_button.Size = new System.Drawing.Size(112, 37);
            this.select_button.TabIndex = 5;
            this.select_button.Text = "Select";
            this.select_button.UseVisualStyleBackColor = false;
            this.select_button.Click += new System.EventHandler(this.select_button_Click);
            // 
            // help_button
            // 
            this.help_button.BackColor = System.Drawing.Color.Transparent;
            this.help_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.help_button.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.help_button.Location = new System.Drawing.Point(824, 9);
            this.help_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.help_button.Name = "help_button";
            this.help_button.Size = new System.Drawing.Size(38, 38);
            this.help_button.TabIndex = 4;
            this.help_button.Text = "?";
            this.help_button.UseVisualStyleBackColor = false;
            this.help_button.Click += new System.EventHandler(this.help_button_Click);
            // 
            // search_textbox
            // 
            this.search_textbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.search_textbox.Location = new System.Drawing.Point(154, 14);
            this.search_textbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.search_textbox.Name = "search_textbox";
            this.search_textbox.Size = new System.Drawing.Size(218, 26);
            this.search_textbox.TabIndex = 3;
            this.search_textbox.TextChanged += new System.EventHandler(this.search_textbox_TextChanged);
            this.search_textbox.DoubleClick += new System.EventHandler(this.search_textbox_DoubleClick);
            this.search_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_textbox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Browse Images:";
            // 
            // search_button
            // 
            this.search_button.Enabled = false;
            this.search_button.Location = new System.Drawing.Point(384, 11);
            this.search_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(78, 35);
            this.search_button.TabIndex = 1;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // images_panel
            // 
            this.images_panel.AutoScroll = true;
            this.images_panel.BackColor = System.Drawing.Color.DimGray;
            this.images_panel.Location = new System.Drawing.Point(0, 55);
            this.images_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.images_panel.Name = "images_panel";
            this.images_panel.Size = new System.Drawing.Size(872, 537);
            this.images_panel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.upload_button);
            this.panel2.Controls.Add(this.num_images_textbox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(914, 18);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 655);
            this.panel2.TabIndex = 1;
            // 
            // upload_button
            // 
            this.upload_button.Location = new System.Drawing.Point(70, 602);
            this.upload_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.upload_button.Name = "upload_button";
            this.upload_button.Size = new System.Drawing.Size(129, 35);
            this.upload_button.TabIndex = 3;
            this.upload_button.Text = "Upload Images";
            this.upload_button.UseVisualStyleBackColor = true;
            this.upload_button.Click += new System.EventHandler(this.upload_button_Click);
            // 
            // num_images_textbox
            // 
            this.num_images_textbox.Location = new System.Drawing.Point(159, 66);
            this.num_images_textbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_images_textbox.Name = "num_images_textbox";
            this.num_images_textbox.Size = new System.Drawing.Size(38, 26);
            this.num_images_textbox.TabIndex = 2;
            this.num_images_textbox.Text = "10";
            this.num_images_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_images_textbox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of Images:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "User Options:";
            // 
            // Image_Search_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Image_Search_Form";
            this.Text = "Reference Image Search";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button help_button;
        private System.Windows.Forms.TextBox search_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.FlowLayoutPanel images_panel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button select_button;
        private System.Windows.Forms.Button add_fav_button;
        private System.Windows.Forms.CheckBox multi_image_selection;
        private System.Windows.Forms.Button upload_button;
        private System.Windows.Forms.TextBox num_images_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button view_fav_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button remove_fav_button;
        private System.Windows.Forms.Button user_images_button;
    }
}