using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using Extensions = SkiaSharp.Views.Desktop.Extensions;

namespace Sketchpop
{
    internal static class Program
    {
        
        public static Canvas_Manager canvas_manager = null;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login_window login_window = new login_window();
            DialogResult result = login_window.ShowDialog();
            if (result == DialogResult.OK)
            {
                Application.Run(new main_window());
            }
        }

    }
}
