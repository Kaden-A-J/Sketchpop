namespace Sketchpop
{
    partial class login_window
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
            this.login_button = new System.Windows.Forms.Button();
            this.skip_button = new System.Windows.Forms.Button();
            this.password_textbox = new System.Windows.Forms.TextBox();
            password_textbox.PasswordChar = '*';
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.display_password_checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // login_button
            // 
            this.login_button.Location = new System.Drawing.Point(274, 258);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(75, 60);
            this.login_button.TabIndex = 0;
            this.login_button.Text = "sign up/ login";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // skip_button
            // 
            this.skip_button.Location = new System.Drawing.Point(442, 258);
            this.skip_button.Name = "skip_button";
            this.skip_button.Size = new System.Drawing.Size(75, 60);
            this.skip_button.TabIndex = 1;
            this.skip_button.Text = "skip";
            this.skip_button.UseVisualStyleBackColor = true;
            this.skip_button.Click += new System.EventHandler(this.skip_button_Click);
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(325, 154);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(232, 26);
            this.password_textbox.TabIndex = 3;
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(325, 101);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(232, 26);
            this.username_textbox.TabIndex = 4;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(229, 160);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(85, 20);
            this.password_label.TabIndex = 5;
            this.password_label.Text = "password :";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(229, 107);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(88, 20);
            this.username_label.TabIndex = 6;
            this.username_label.Text = "username :";
            // 
            // display_password_checkbox
            // 
            this.display_password_checkbox.AutoSize = true;
            this.display_password_checkbox.Location = new System.Drawing.Point(585, 159);
            this.display_password_checkbox.Name = "display_password_checkbox";
            this.display_password_checkbox.Size = new System.Drawing.Size(148, 24);
            this.display_password_checkbox.TabIndex = 7;
            this.display_password_checkbox.Text = "Show Password";
            this.display_password_checkbox.UseVisualStyleBackColor = true;
            this.display_password_checkbox.CheckedChanged += new System.EventHandler(this.display_password_checkbox_CheckedChanged);
            // 
            // login_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.display_password_checkbox);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.skip_button);
            this.Controls.Add(this.login_button);
            this.Name = "login_window";
            this.Text = "Login_Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button skip_button;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.CheckBox display_password_checkbox;
    }
}