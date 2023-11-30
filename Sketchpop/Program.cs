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
        public static main_window main_window { get; private set; }
        public static Canvas_Manager canvas_manager = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main_window = new main_window();
            if (main_window == null)
            {
                Console.WriteLine("main_window is null");
            }
            Application.Run(main_window);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = ((Exception)args.ExceptionObject);
            MessageBox.Show("Unhandled exception: " + e.GetType() + "\n\nmessage: " + e.Message);
        }

    }
}
