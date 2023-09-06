namespace Sketchpop
{
    partial class Fullscreen_Image_Form
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
            this.image_picturebox = new System.Windows.Forms.PictureBox();
            this.info_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.image_picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // image_picturebox
            // 
            this.image_picturebox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.image_picturebox.Location = new System.Drawing.Point(0, 0);
            this.image_picturebox.Name = "image_picturebox";
            this.image_picturebox.Size = new System.Drawing.Size(800, 450);
            this.image_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image_picturebox.TabIndex = 0;
            this.image_picturebox.TabStop = false;
            this.image_picturebox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.image_picturebox_MouseDown);
            this.image_picturebox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.image_picturebox_MouseMove);
            this.image_picturebox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.image_picturebox_MouseUp);
            // 
            // info_label
            // 
            this.info_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.info_label.AutoSize = true;
            this.info_label.BackColor = System.Drawing.Color.Transparent;
            this.info_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_label.Location = new System.Drawing.Point(170, 404);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(427, 37);
            this.info_label.TabIndex = 1;
            this.info_label.Text = "Press \'Esc\' to Exit Fullscreen";
            // 
            // Fullscreen_Image_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.image_picturebox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Fullscreen_Image_Form";
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Fullscreen_Image_Form_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.image_picturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox image_picturebox;
        private System.Windows.Forms.Label info_label;
    }
}