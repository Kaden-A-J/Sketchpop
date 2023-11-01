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
    public partial class Save_Form : Form
    {
        public Save_Form()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string name = pictureName.Text;
            if(name.Equals(""))
            {
                MessageBox.Show("The picture name is blank.", "Tips", MessageBoxButtons.OK);
            }
            else
            {
                if(new Database_Manager().Execute_Drawing_Upload_Query(name)){
                    MessageBox.Show("The drawing has been saved.", "Tips", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("The drawing has not been saved.", "Tips", MessageBoxButtons.OK);
                }
            }
        }
    }
}
