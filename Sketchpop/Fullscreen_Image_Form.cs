using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchpop
{
    /// <summary>
    /// This form is used to display an Image in fullscreen. The form is maximized
    /// and the image is placed in the center of the form. The image is draggable and
    /// has zoom functionality and is exited when the user presses the 'Escape' key.
    /// 
    /// Code Credit:
    /// 
    /// Zoom Code: https://stackoverflow.com/questions/34319999/zoom-in-and-zoom-out-picturebox-c-sharp
    /// Drag Code: http://www.java2s.com/Code/CSharp/GUI-Windows-Form/DraganddropthePictureBox.htm
    /// </summary>
    public partial class Fullscreen_Image_Form : Form
    {
        // image resizing variables
        private int _originalPictureBoxWidth;
        private int _originalPictureBoxHeight;

        // picturebox dragging variables
        private bool _dragging = false;
        private int _currentX, _currentY;

        /// <summary>
        /// Constructor. Image data in the form of a byte[] is passed into
        /// the constructor, then this data is used to construct the fullscreen
        /// view. This image is placed in a picturebox, with events tied to it for
        /// zoom functionality.
        /// </summary>
        /// <param name="image_data">the byte[] representation of the image</param>
        public Fullscreen_Image_Form(byte[] image_data)
        {
            InitializeComponent();

            // attach a mouse scrollwheel event to the PictureBox
            image_picturebox.MouseWheel += new MouseEventHandler(Image_Mouse_Wheel);

            // center the image
            image_picturebox.Location = new Point((image_picturebox.Parent.ClientSize.Width - image_picturebox.Width) / 2, (image_picturebox.Parent.ClientSize.Height - image_picturebox.Height) / 2);
            using (MemoryStream ms = new MemoryStream(image_data))
            {
                image_picturebox.Image = Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Exit fullscreen functionality. User presses the 'Esc' key to exit form.
        /// </summary>
        /// <param name="sender">user presses 'esc' key</param>
        /// <param name="e">key pressed</param>
        private void Fullscreen_Image_Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// Zoom functionality. When the user scrolls up or down on the mousewheel, this
        /// method taks the direction of the mousewheel (+/-) and 'zooms' in or out by a
        /// small amount, within a specified boundary. The zoom is determined by cursor location.
        /// </summary>
        /// <param name="sender">user moves mousewheel</param>
        /// <param name="e">mousewheel direction, cursor position</param>
        private void Image_Mouse_Wheel(object sender, MouseEventArgs e)
        {
            // mousewheel direction will either be + or -
            if (e.Delta > 0)
            {
                // zoom in
                if ((image_picturebox.Width < (15 * this.Width)) && (image_picturebox.Height < (15 * this.Height)))
                {
                    image_picturebox.Width = (int)(image_picturebox.Width * 1.25);
                    image_picturebox.Height = (int)(image_picturebox.Height * 1.25);

                    // adjust PictureBox position to zoom in on the cursor
                    image_picturebox.Top = (int)(e.Y - 1.25 * (e.Y - image_picturebox.Top));
                    image_picturebox.Left = (int)(e.X - 1.25 * (e.X - image_picturebox.Left));
                }
            }
            else
            {
                // zoom out
                if ((image_picturebox.Width > (_originalPictureBoxWidth)) && (image_picturebox.Height > (_originalPictureBoxHeight)))
                {
                    image_picturebox.Width = (int)(image_picturebox.Width / 1.25);
                    image_picturebox.Height = (int)(image_picturebox.Height / 1.25);

                    // adjust PictureBox position to zoom out on the cursor
                    image_picturebox.Top = (int)(e.Y - 0.80 * (e.Y - image_picturebox.Top));
                    image_picturebox.Left = (int)(e.X - 0.80 * (e.X - image_picturebox.Left));
                }
            }
        }

        /// <summary>
        /// Draggable functionality. When the left mouse button is clicked,
        /// signify that the PictureBox is being dragged and save the current
        /// mouse position.
        /// </summary>
        /// <param name="sender">user clicks mouse</param>
        /// <param name="e">mousebutton clicked</param>
        private void image_picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _currentX = e.X;
                _currentY = e.Y;
            }
        }                

        /// <summary>
        /// Draggable functionality. When the mouse moves while the left mouse
        /// button is clicked (signified by the '_dragging' bool), the image position
        /// is redetermined depending on the position from the current position (where
        /// the user last pressed the left mouse click), and where the cursor currently
        /// is. Then the Picturebox position is set to the new position, no redrawing necessary.
        /// </summary>
        /// <param name="sender">user moves mouse</param>
        /// <param name="e">cursor position</param>
        private void image_picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                image_picturebox.Top = image_picturebox.Top + (e.Y - _currentY);
                image_picturebox.Left = image_picturebox.Left + (e.X - _currentX);
            }
        }

        /// <summary>
        /// Draggable functionality. When the user releases the left mouse button
        /// the image is no longer being dragged, and the boolean is set to false,
        /// waiting for the PictureBox to be clicked again.
        /// </summary>
        /// <param name="sender">user releases left mouse button</param>
        /// <param name="e">mouse button being released</param>
        private void image_picturebox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragging = false;
        }
    }
}
