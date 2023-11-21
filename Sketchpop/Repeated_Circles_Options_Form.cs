using System;
using System.Windows.Forms;

namespace Sketchpop
{
    public partial class Repeated_Circles_Options_Form : Form
    {
        private main_window main_window_form;

        public Repeated_Circles_Options_Form(main_window main_window)
        {
            this.main_window_form = main_window;
            InitializeComponent();
            Random r = new Random();
            spacing_num_up_down.Value = r.Next((int)spacing_num_up_down.Minimum, (int)spacing_num_up_down.Maximum);
            angle_num_up_down.Value = r.Next((int)angle_num_up_down.Minimum, (int)angle_num_up_down.Maximum);
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            main_window_form.clear_canvas_button_Click(null, null);
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
