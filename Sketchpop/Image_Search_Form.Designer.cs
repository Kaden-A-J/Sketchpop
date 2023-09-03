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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.user_images_button = new System.Windows.Forms.Button();
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
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 426);
            this.panel1.TabIndex = 0;
            // 
            // remove_fav_button
            // 
            this.remove_fav_button.Location = new System.Drawing.Point(337, 392);
            this.remove_fav_button.Name = "remove_fav_button";
            this.remove_fav_button.Size = new System.Drawing.Size(88, 23);
            this.remove_fav_button.TabIndex = 9;
            this.remove_fav_button.Text = "Remove";
            this.remove_fav_button.UseVisualStyleBackColor = true;
            this.remove_fav_button.Visible = false;
            this.remove_fav_button.Click += new System.EventHandler(this.remove_fav_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(19, 392);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(52, 23);
            this.cancel_button.TabIndex = 8;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // view_fav_button
            // 
            this.view_fav_button.Location = new System.Drawing.Point(355, 7);
            this.view_fav_button.Name = "view_fav_button";
            this.view_fav_button.Size = new System.Drawing.Size(84, 23);
            this.view_fav_button.TabIndex = 7;
            this.view_fav_button.Text = "View Favorites";
            this.view_fav_button.UseVisualStyleBackColor = true;
            this.view_fav_button.Click += new System.EventHandler(this.view_fav_button_Click);
            // 
            // add_fav_button
            // 
            this.add_fav_button.Enabled = false;
            this.add_fav_button.Location = new System.Drawing.Point(238, 391);
            this.add_fav_button.Name = "add_fav_button";
            this.add_fav_button.Size = new System.Drawing.Size(93, 23);
            this.add_fav_button.TabIndex = 6;
            this.add_fav_button.Text = "Add to Favorites";
            this.add_fav_button.UseVisualStyleBackColor = true;
            this.add_fav_button.Click += new System.EventHandler(this.add_fav_button_Click);
            // 
            // multi_image_selection
            // 
            this.multi_image_selection.AutoSize = true;
            this.multi_image_selection.Location = new System.Drawing.Point(442, 391);
            this.multi_image_selection.Name = "multi_image_selection";
            this.multi_image_selection.Size = new System.Drawing.Size(132, 17);
            this.multi_image_selection.TabIndex = 3;
            this.multi_image_selection.Text = "Select Multiple Images";
            this.multi_image_selection.UseVisualStyleBackColor = true;
            this.multi_image_selection.Click += new System.EventHandler(this.multi_image_selection_Click);
            // 
            // select_button
            // 
            this.select_button.BackColor = System.Drawing.Color.White;
            this.select_button.Enabled = false;
            this.select_button.Location = new System.Drawing.Point(157, 390);
            this.select_button.Name = "select_button";
            this.select_button.Size = new System.Drawing.Size(75, 24);
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
            this.help_button.Location = new System.Drawing.Point(549, 6);
            this.help_button.Name = "help_button";
            this.help_button.Size = new System.Drawing.Size(25, 25);
            this.help_button.TabIndex = 4;
            this.help_button.Text = "?";
            this.help_button.UseVisualStyleBackColor = false;
            this.help_button.Click += new System.EventHandler(this.help_button_Click);
            // 
            // search_textbox
            // 
            this.search_textbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.search_textbox.Location = new System.Drawing.Point(103, 9);
            this.search_textbox.Name = "search_textbox";
            this.search_textbox.Size = new System.Drawing.Size(147, 20);
            this.search_textbox.TabIndex = 3;
            this.search_textbox.TextChanged += new System.EventHandler(this.search_textbox_TextChanged);
            this.search_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_textbox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Browse Images:";
            // 
            // search_button
            // 
            this.search_button.Enabled = false;
            this.search_button.Location = new System.Drawing.Point(256, 7);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(52, 23);
            this.search_button.TabIndex = 1;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // images_panel
            // 
            this.images_panel.AutoScroll = true;
            this.images_panel.BackColor = System.Drawing.Color.DimGray;
            this.images_panel.Location = new System.Drawing.Point(0, 36);
            this.images_panel.Name = "images_panel";
            this.images_panel.Size = new System.Drawing.Size(581, 349);
            this.images_panel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.upload_button);
            this.panel2.Controls.Add(this.num_images_textbox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(609, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 426);
            this.panel2.TabIndex = 1;
            // 
            // upload_button
            // 
            this.upload_button.Location = new System.Drawing.Point(47, 391);
            this.upload_button.Name = "upload_button";
            this.upload_button.Size = new System.Drawing.Size(86, 23);
            this.upload_button.TabIndex = 3;
            this.upload_button.Text = "Upload Images";
            this.upload_button.UseVisualStyleBackColor = true;
            this.upload_button.Click += new System.EventHandler(this.upload_button_Click);
            // 
            // num_images_textbox
            // 
            this.num_images_textbox.Location = new System.Drawing.Point(106, 43);
            this.num_images_textbox.Name = "num_images_textbox";
            this.num_images_textbox.Size = new System.Drawing.Size(27, 20);
            this.num_images_textbox.TabIndex = 2;
            this.num_images_textbox.Text = "10";
            this.num_images_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_images_textbox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of Images:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "User Options:";
            // 
            // user_images_button
            // 
            this.user_images_button.Location = new System.Drawing.Point(442, 7);
            this.user_images_button.Name = "user_images_button";
            this.user_images_button.Size = new System.Drawing.Size(84, 23);
            this.user_images_button.TabIndex = 4;
            this.user_images_button.Text = "My Images";
            this.user_images_button.UseVisualStyleBackColor = true;
            // 
            // Image_Search_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "Image_Search_Form";
            this.Text = "Image_Search_Form1";
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