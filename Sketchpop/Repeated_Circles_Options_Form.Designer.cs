namespace Sketchpop
{
    partial class Repeated_Circles_Options_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Repeated_Circles_Options_Form));
            this.spacing_num_up_down = new System.Windows.Forms.NumericUpDown();
            this.angle_num_up_down = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.start_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spacing_num_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angle_num_up_down)).BeginInit();
            this.SuspendLayout();
            // 
            // spacing_num_up_down
            // 
            this.spacing_num_up_down.Location = new System.Drawing.Point(259, 85);
            this.spacing_num_up_down.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spacing_num_up_down.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.spacing_num_up_down.Name = "spacing_num_up_down";
            this.spacing_num_up_down.Size = new System.Drawing.Size(120, 20);
            this.spacing_num_up_down.TabIndex = 1;
            this.spacing_num_up_down.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.spacing_num_up_down.Enter += new System.EventHandler(this.spacing_num_up_down_Enter);
            // 
            // angle_num_up_down
            // 
            this.angle_num_up_down.Location = new System.Drawing.Point(259, 125);
            this.angle_num_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.angle_num_up_down.Name = "angle_num_up_down";
            this.angle_num_up_down.Size = new System.Drawing.Size(120, 20);
            this.angle_num_up_down.TabIndex = 2;
            this.angle_num_up_down.Enter += new System.EventHandler(this.angle_num_up_down_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Spacing between lines (20 - 500):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Angle of lines (in degrees, 0 - 360):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(326, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Draw as many identical circles between the parallel lines as you can";
            // 
            // start_button
            // 
            this.start_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.start_button.Location = new System.Drawing.Point(282, 182);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 4;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(43, 182);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // Repeated_Circles_Options_Form
            // 
            this.AcceptButton = this.start_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(414, 217);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.angle_num_up_down);
            this.Controls.Add(this.spacing_num_up_down);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Repeated_Circles_Options_Form";
            this.Text = "Repeated_Circles_Options_Form";
            ((System.ComponentModel.ISupportInitialize)(this.spacing_num_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angle_num_up_down)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown spacing_num_up_down;
        private System.Windows.Forms.NumericUpDown angle_num_up_down;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button cancel_button;
    }
}