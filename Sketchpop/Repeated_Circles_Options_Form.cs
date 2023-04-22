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
    public partial class Repeated_Circles_Options_Form : Form
    {
        public Repeated_Circles_Options_Form()
        {
            InitializeComponent();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            Program.canvas_manager.Repeated_Circles_Exercise((int)spacing_num_up_down.Value, (int)angle_num_up_down.Value);
            this.Close();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spacing_num_up_down_Enter(object sender, EventArgs e)
        {
            spacing_num_up_down.Select(0, spacing_num_up_down.Value.ToString().Length);
        }

        private void angle_num_up_down_Enter(object sender, EventArgs e)
        {
            angle_num_up_down.Select(0, angle_num_up_down.Value.ToString().Length);
        }
    }
}
