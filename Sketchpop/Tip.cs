using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System;
using OpenTK;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using System.Linq;

namespace Sketchpop
{
    /// <summary>
    /// This class creates a floating tip. These tips are used to help the user understand the UI elements
    /// and other features of the program more clearly. The tips are color coded to signify what the tip
    /// is trying to explain.
    /// 
    /// The Color Code for the tips is as follows:
    /// 
    /// 0:(blue) : ui element
    /// 1:(pink) : exercise
    /// 2:(green): information
    /// 
    /// A triangle is drawn to excentuate the element that the tip is describing.
    /// </summary>
    public partial class Tip : Form
    {
        private Point _tip_position;
        private Form _main_window;
        private Control _element;
        private Control _container;
        private int _type;
        private string _desc;
        private string _folder;

        private bool _expand;
        private bool _expanded = false;
        private Dictionary<Image, string> slides = new Dictionary<Image, string>();
        private int _curr_idx = 0;
        private bool _active = false;

        private Timer _timer = new Timer();

        private readonly int _triangle_width = 25; // modify for length of the triangle on the form
        private readonly int _radius = 20; // modify for roundness of rectangle
        private int _original_width;
        private int _original_height;

        public event EventHandler closed;
        public event EventHandler<Tip> new_tip;

        /// <summary>
        /// Constructor. Takes the main_window form, the control that the tip is representing,
        /// the container that holds the control, a string description and an int to represent
        /// the type of tip (see above for types).
        /// </summary>
        /// <param name="mw">main_window form</param>
        /// <param name="c">container that holds the element that the tip is representing</param>
        /// <param name="e">the element that the tip is representing</param>
        /// <param name="d">description for the tip</param>
        /// <param name="t">type of tip</param>
        /// <param name="expand">determines if this tip will have more information to display</param>
        /// <param name="expanded">determines the current state of the tip</param>
        public Tip(Form mw, Control c, Control e, string d, int t, bool expand, string folder)//, bool expanded)
        {
            InitializeComponent();

            Setup(mw, c, e, d, t, expand, folder);//, expanded); // set variables for the tip

            Generate_Tip(); // draws the tip 

            this._active = true;

            new_tip?.Invoke(this, this);

            this.TopMost = true;
        }

        public Tip(Form mw, Control c, Control e, string d, int t, bool expand)//, bool expanded)
        {
            InitializeComponent();

            Setup(mw, c, e, d, t, expand, "");//, expanded); // set variables for the tip

            Generate_Tip(); // draws the tip 

            this._active = true;

            new_tip?.Invoke(this, this);

            this.TopMost = true;
        }

        /// <summary>
        /// Sets up the Tip that is representing an Element of Sketchpop. This includes: setting the 
        /// position, choosing the color, and setting the description for the Tip.
        /// </summary>
        /// <param name="main_window">main Sketchpop window</param>
        /// <param name="container">container that holds element</param>
        /// <param name="element">element to add Tip for</param>
        /// <param name="desc">Tip description</param>
        /// <param name="type">type of element</param>
        private void Setup(Form main_window, Control container, Control element, string desc, int type, bool expand, string folder)//, bool expanded)
        {
            this.StartPosition = FormStartPosition.Manual;

            // use the element and its container to calculate the position for the tip
            Point element_pos = element.Location;
            Point container_pos = container.Location;

            int tip_x = container_pos.X + element_pos.X + element.Width;
            int tip_y = container_pos.Y + element_pos.Y - element.Height / 2; // pos the Tip in the middle of the element

            this._tip_position = new Point(tip_x, tip_y); // set this Tip's position

            // set color based on type
            switch (type)
            {
                case 0:
                    this.BackColor = Color.LightBlue;
                    break;
                case 1:
                    this.BackColor = Color.LightPink;
                    break;
                case 2:
                    this.BackColor = Color.LightGreen;
                    break;
            }

            // save this Object's fields for redrawing if necessary
            this.Opacity = 0.75;
            this.description_label.Text = desc;
            this._main_window = main_window;
            this._element = element;
            this._container = container;
            this._type = type;
            this._desc = desc;
            this._expand = expand;
            this._expanded = false;
            this._folder = folder;

            this._timer.Interval = 10000;
            this._timer.Tick += (s, e) => { Close(); };
            this.Load += (sender, e) => { _timer.Start(); };

            this._original_width = this.Width;
            this._original_height = this.Height;

            if (!this._expand) { this.more_link_label.Visible = false; }

            // add an event listener for when the window is maximized/minimized
            this._main_window.LocationChanged += MainWindow_LocationChanged;
            this._main_window.Resize += MainWindow_LocationChanged;
        }

        /// <summary>
        /// Draws the Tip form. A triangle is drawn and added to the Form's path so that
        /// the graphic better shows which element is being described. 
        /// </summary>
        private void Generate_Tip()
        {
            Point[] triangle = new Point[3];
            triangle[0] = new Point(0, this.Height / 2); // New left-most point
            triangle[1] = new Point(this._triangle_width, 0); // Top-right
            triangle[2] = new Point(this._triangle_width, this.Height); // Bottom-right

            using (GraphicsPath path = new GraphicsPath())
            {
                // create the left triangle
                path.AddPolygon(triangle);

                // create the right rounded rectangle
                Rectangle rect = new Rectangle(this._triangle_width, 0, this.Width - this._triangle_width, this.Height);

                path.AddArc(rect.Right - _radius, rect.Y, _radius, _radius, 270, 90);
                path.AddArc(rect.Right - _radius, rect.Bottom - _radius, _radius, _radius, 0, 90);
                path.AddLine(this._triangle_width, this.Height, this._triangle_width, 0);

                this.Region = new Region(path);

                if (this.Location == new Point(0, 0))
                {
                    this.Location = Calculate_Form_Position();

                    // update the position when the form is resized (minimize/maximize)
                    this._main_window.LocationChanged += (s, e) => this.Location = Calculate_Form_Position();
                    this._main_window.Resize += (s, e) => this.Location = Calculate_Form_Position();
                }
            }
        }

        /// <summary>
        /// Checks to see if the Form has not been closed. If so, returns the calculated screen
        /// coordinates to transform the form's position so that it matches the screen's position.
        /// </summary>
        /// <returns>the transformed point</returns>
        private Point Calculate_Form_Position()
        {
            try
            {
                if (!this.IsDisposed)
                {
                    // calculate the form's position relative to the main window
                    Point screenTipPosition = this._main_window.PointToScreen(this._tip_position);
                    return this.PointToClient(screenTipPosition);
                }
                else
                {
                    return new Point(0, 0);
                }
            }
            catch (ObjectDisposedException)
            {
                return new Point(0, 0);
            }
        }

        /// <summary>
        /// Event Handler for when the main_window form is maximized or minimized.
        /// 
        /// Closes the old from, opens a new one at the updated position.
        /// </summary>
        /// <param name="sender">window resized</param>
        /// <param name="e">n/a</param>
        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (!this.IsDisposed)
            {
                // recalculate the form's position based on the new window size
                this._tip_position = Calculate_Form_Position();

                var new_tip = new Tip(this._main_window, this._container, this._element, this._desc, this._type, this._expand, this._folder);//, this._expanded);
                new_tip._expanded = this._expanded;
                new_tip._active = this._active;
                new_tip.new_tip += this.new_tip;
                new_tip.closed += this.closed;
                new_tip._timer = this._timer;
                new_tip.more_link_label.Text = this.more_link_label.Text;

                // close current form
                Close();

                // open a new tip form at the updated position                
                if (new_tip._expanded)
                {
                    new_tip.Expand();                    
                }

                new_tip.Show();
            }
            else
            {
                // remove the event handler to avoid triggering this event
                this._main_window.LocationChanged -= MainWindow_LocationChanged;
                this._main_window.Resize -= MainWindow_LocationChanged;
            }
        }

        /// <summary>
        /// Closes the form when user clicks the 'close' link label.
        /// </summary>
        /// <param name="sender">user clicks label</param>
        /// <param name="e">n/a</param>
        private void close_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this._active = false;
            this.closed?.Invoke(this, EventArgs.Empty);

            Close();
            this.Dispose();
            this._main_window.LocationChanged -= MainWindow_LocationChanged;
            this._main_window.Resize -= MainWindow_LocationChanged;
        }

        public void Show_Tip()
        {
            this.Show();
        }

        private void more_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!_expanded)
            {
                //_timer.Stop();
                Expand();
                _expanded = true;
                more_link_label.Text = "less info";
            }
            else
            {
                Reset();
                _expanded = false;
                more_link_label.Text = "more info";
            }
        }

        private void Expand()
        {
            _timer.Stop();

            // make the form larger to show more information
            this.Height = 500;
            this.Width = 500;

            this.main_Flow_Layout_Panel.Visible = false;

            this.Show();

            this.Set_Content("Repeated_Circles");
            this.flowLayoutPanel2.Padding = new Padding(0, 0, 20, 0);
            this.flowLayoutPanel2.Visible = true;
            this.flowLayoutPanel3.Controls.Add(more_link_label);
            this.flowLayoutPanel3.Controls.Add(close_link_label);
            this.flowLayoutPanel3.Height = 20;            
            this.flowLayoutPanel3.Visible = true;


            using (GraphicsPath path = new GraphicsPath())
            {
                // create the right rounded rectangle
                Rectangle rect = new Rectangle(0, 0, this.Width - this._triangle_width, this.Height);

                path.AddArc(rect.Left, rect.Top, _radius, _radius, 180, 90);
                path.AddArc(rect.Right - _radius, rect.Top, _radius, _radius, 270, 90);
                path.AddArc(rect.Right - _radius, rect.Bottom - _radius, _radius, _radius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - _radius, _radius, _radius, 90, 90);

                this.Region = new Region(path);
            }
        }

        private void Reset()
        {
            this.link_Label_Layout_Panel.Controls.Add(more_link_label);
            this.link_Label_Layout_Panel.Controls.Add(close_link_label);

            this.flowLayoutPanel1.Visible = false;
            this.flowLayoutPanel2.Visible = false;
            this.flowLayoutPanel3.Visible = false;

            this.main_Flow_Layout_Panel.Visible = true;

            this.Height = this._original_height;
            this.Width = this._original_width;

            this._curr_idx = 0;
            pb.Image = slides.ElementAt(_curr_idx).Key;
            label1.Text = slides.ElementAt(_curr_idx).Value;

            Generate_Tip();
        }

        public void Set_Content(string element)
        {
            if (slides.Count == 0)
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var imageFolder = Path.Combine(baseDirectory, $"..\\..\\Tip_Images\\{element}");
                var descrFolder = Path.Combine(baseDirectory, $"..\\..\\Tip_Descriptions\\{element}");

                var tmp_imgs = new List<Image>();
                foreach (string png in Directory.GetFiles(imageFolder, "*.*"))
                {
                    var img = Image.FromFile(png);
                    tmp_imgs.Add(img);
                }

                var tmp_descs = new List<string>();
                foreach (string des in Directory.GetFiles(descrFolder, "*.txt").ToList())
                {
                    var descr = File.ReadAllText(des);
                    tmp_descs.Add(descr);
                }

                for (int i = 0; i < tmp_imgs.Count; i++)
                {
                    slides.Add(tmp_imgs[i], tmp_descs[i]);
                }

                pb.Width = 500;
                pb.Height = 200;
                pb.Image = slides.First().Key;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Visible = true;

                flowLayoutPanel1.Controls.Add(pb);
                label1.Text = slides.First().Value;

                Panel sp1 = new Panel();
                sp1.Height = 20;
                sp1.Width = 185;
                sp1.Anchor = AnchorStyles.Bottom;
                sp1.BackColor = this.BackColor;

                Button left_arrow = new Button();
                left_arrow.Height = 20;
                left_arrow.Width = 45;
                left_arrow.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                left_arrow.BackColor = Control.DefaultBackColor;
                left_arrow.Text = "←";
                left_arrow.Click += Left_Click;

                Panel sp2 = new Panel();
                sp2.Height = 20;
                sp2.Width = 20;
                sp2.Anchor = AnchorStyles.Bottom;
                sp2.BackColor = this.BackColor;

                Button right_arrow = new Button();
                right_arrow.Height = 20;
                right_arrow.Width = 45;
                right_arrow.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                right_arrow.BackColor = Control.DefaultBackColor;
                right_arrow.Text = "→";
                right_arrow.Click += Right_Click;

                flowLayoutPanel1.Controls.Add(sp1);
                flowLayoutPanel1.Controls.Add(left_arrow);
                flowLayoutPanel1.Controls.Add(sp2);
                flowLayoutPanel1.Controls.Add(right_arrow);

            }

            flowLayoutPanel1.Visible = true;
        }

        private void Left_Click(object sender, EventArgs e)
        {
            if (_curr_idx != 0)
            {
                _curr_idx--;

                pb.Image = slides.ElementAt(_curr_idx).Key;
                label1.Text = slides.ElementAt(_curr_idx).Value;
            }
        }

        private void Right_Click(object sender, EventArgs e)
        {
            if (_curr_idx != slides.Count - 1)
            {
                _curr_idx++;

                pb.Image = slides.ElementAt(_curr_idx).Key;
                label1.Text = slides.ElementAt(_curr_idx).Value;
            }
        }

        //public class TipArgs
    }

}
