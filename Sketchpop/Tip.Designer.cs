using System;

namespace Sketchpop
{
    partial class Tip
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
            this.main_Flow_Layout_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.description_Layout_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.description_label = new System.Windows.Forms.Label();
            this.link_Label_Layout_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.more_link_label = new System.Windows.Forms.LinkLabel();
            this.close_link_label = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.pb = new System.Windows.Forms.PictureBox();
            this.main_Flow_Layout_Panel.SuspendLayout();
            this.description_Layout_Panel.SuspendLayout();
            this.link_Label_Layout_Panel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // main_Flow_Layout_Panel
            // 
            this.main_Flow_Layout_Panel.Controls.Add(this.description_Layout_Panel);
            this.main_Flow_Layout_Panel.Controls.Add(this.link_Label_Layout_Panel);
            this.main_Flow_Layout_Panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.main_Flow_Layout_Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.main_Flow_Layout_Panel.Location = new System.Drawing.Point(45, 0);
            this.main_Flow_Layout_Panel.Margin = new System.Windows.Forms.Padding(2);
            this.main_Flow_Layout_Panel.Name = "main_Flow_Layout_Panel";
            this.main_Flow_Layout_Panel.Size = new System.Drawing.Size(167, 47);
            this.main_Flow_Layout_Panel.TabIndex = 2;
            // 
            // description_Layout_Panel
            // 
            this.description_Layout_Panel.AutoSize = true;
            this.description_Layout_Panel.Controls.Add(this.description_label);
            this.description_Layout_Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.description_Layout_Panel.Location = new System.Drawing.Point(2, 2);
            this.description_Layout_Panel.Margin = new System.Windows.Forms.Padding(2);
            this.description_Layout_Panel.Name = "description_Layout_Panel";
            this.description_Layout_Panel.Size = new System.Drawing.Size(21, 13);
            this.description_Layout_Panel.TabIndex = 3;
            // 
            // description_label
            // 
            this.description_label.AutoSize = true;
            this.description_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description_label.Location = new System.Drawing.Point(2, 0);
            this.description_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.description_label.Name = "description_label";
            this.description_label.Size = new System.Drawing.Size(17, 13);
            this.description_label.TabIndex = 3;
            this.description_label.Text = "\"\"";
            // 
            // link_Label_Layout_Panel
            // 
            this.link_Label_Layout_Panel.AutoSize = true;
            this.link_Label_Layout_Panel.Controls.Add(this.more_link_label);
            this.link_Label_Layout_Panel.Controls.Add(this.close_link_label);
            this.link_Label_Layout_Panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.link_Label_Layout_Panel.Location = new System.Drawing.Point(2, 19);
            this.link_Label_Layout_Panel.Margin = new System.Windows.Forms.Padding(2);
            this.link_Label_Layout_Panel.Name = "link_Label_Layout_Panel";
            this.link_Label_Layout_Panel.Size = new System.Drawing.Size(70, 9);
            this.link_Label_Layout_Panel.TabIndex = 0;
            // 
            // more_link_label
            // 
            this.more_link_label.AutoSize = true;
            this.more_link_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.more_link_label.Location = new System.Drawing.Point(2, 0);
            this.more_link_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.more_link_label.Name = "more_link_label";
            this.more_link_label.Size = new System.Drawing.Size(37, 9);
            this.more_link_label.TabIndex = 4;
            this.more_link_label.TabStop = true;
            this.more_link_label.Text = "more info";
            this.more_link_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.more_link_label_LinkClicked);
            // 
            // close_link_label
            // 
            this.close_link_label.AutoSize = true;
            this.close_link_label.Dock = System.Windows.Forms.DockStyle.Right;
            this.close_link_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_link_label.LinkColor = System.Drawing.Color.Black;
            this.close_link_label.Location = new System.Drawing.Point(44, 0);
            this.close_link_label.Name = "close_link_label";
            this.close_link_label.Size = new System.Drawing.Size(23, 9);
            this.close_link_label.TabIndex = 4;
            this.close_link_label.TabStop = true;
            this.close_link_label.Text = "close";
            this.close_link_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.close_link_label_LinkClicked);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pb);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(45, 240);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.Visible = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, -193);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(45, 240);
            this.flowLayoutPanel2.TabIndex = 0;
            this.flowLayoutPanel2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, -293);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(45, 100);
            this.flowLayoutPanel3.TabIndex = 0;
            this.flowLayoutPanel3.Visible = false;
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(3, 3);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 50);
            this.pb.TabIndex = 1;
            this.pb.TabStop = false;
            this.pb.Visible = false;
            // 
            // Tip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(212, 47);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.main_Flow_Layout_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tip";
            this.Text = "Tip";
            this.main_Flow_Layout_Panel.ResumeLayout(false);
            this.main_Flow_Layout_Panel.PerformLayout();
            this.description_Layout_Panel.ResumeLayout(false);
            this.description_Layout_Panel.PerformLayout();
            this.link_Label_Layout_Panel.ResumeLayout(false);
            this.link_Label_Layout_Panel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel main_Flow_Layout_Panel;
        private System.Windows.Forms.FlowLayoutPanel description_Layout_Panel;
        private System.Windows.Forms.Label description_label;
        private System.Windows.Forms.FlowLayoutPanel link_Label_Layout_Panel;
        private System.Windows.Forms.LinkLabel more_link_label;
        private System.Windows.Forms.LinkLabel close_link_label;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pb;
    }
}