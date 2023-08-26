using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchpop
{
    public partial class login_window : Form
    {
        private Database_Manager dbm = new Database_Manager();
        public login_window()
        {
            InitializeComponent();
        }

        private void skip_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_textbox.Text;
            string password = password_textbox.Text;
            if(username.Equals("") || password.Equals("")) {
                MessageBox.Show("The username or password is blank.", "Tips", MessageBoxButtons.OK);
            }
            else
            {
                if(dbm.Execute_Login_Signup_Query(username,password)) {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The corresponding password is wrong.", "Tips", MessageBoxButtons.OK);
                }
            }            
        }

        private void display_password_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(display_password_checkbox.Checked) { password_textbox.PasswordChar = '\0'; }
            else {password_textbox.PasswordChar = '*'; }
        }
    }
}
