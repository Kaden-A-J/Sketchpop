using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System;
using OpenTK;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using System.Linq;
using System.Net.NetworkInformation;

namespace Sketchpop
{
    /// <summary>
    /// This class creates a floating tip. These tips are used to help the user understand the UI elements
    /// and other features of the program more clearly. The tips are color coded to signify what the tip
    /// is trying to explain.
    /// 
    /// The Color Code for the tips is as follows:
    /// 
    /// 0:(blue) : ui elements
    /// 1:(pink) : exercise
    /// 2:(green): information
    /// 
    /// A triangle is drawn to excentuate the folder_name that the tip is describing.
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
        private bool _shrink = false;
        private Dictionary<int, Slide> slides = new Dictionary<int, Slide>();
        private int _curr_idx = 1;

        private Timer _timer;// = new Timer();

        private readonly int _triangle_width = 25; // modify for length of the triangle on the form
        private readonly int _radius = 20; // modify for roundness of rectangle
        private int _original_width;
        private int _original_height;

        public event EventHandler closed;
        public event EventHandler<Tip> new_tip;

        private Point _prev_location;
        private Label pages;

        /// <summary>
        /// Constructor. Takes the main_window form, the control that the tip is representing,
        /// the container that holds the control, a string description, an int to represent
        /// the type of tip (see above for types), a boolean that determines if the tip can
        /// be expanded (display more information i.e. descriptions and images, and a folder
        /// name that contains the images and descriptions for this tip being created.
        /// </summary>
        /// <param name="mw">main_window form</param>
        /// <param name="c">container that holds the folder_name that the tip is representing</param>
        /// <param name="e">the folder_name that the tip is representing</param>
        /// <param name="d">description for the tip</param>
        /// <param name="t">type of tip</param>
        /// <param name="expand">determines if this tip will have more information to display</param>
        /// <param name="folder">the name of the folder that contains the images and descriptions for this tip</param>
        public Tip(Form mw, Control e, string d, int t, bool expand, string folder)
        {
            InitializeComponent();

            Setup(mw, e, d, t, expand, folder);// set variables for the tip

            Generate_Tip(); // draws the tip 

            new_tip?.Invoke(this, this); // signal to main that a new tip is now active
        }

        /// <summary>
        /// Constructor. Takes the main_window form, the control that the tip is representing,
        /// the container that holds the control, a string description, an int to represent
        /// the type of tip (see above for types), and a boolean that determines if the tip can
        /// be expanded (display more information i.e. descriptions and images.       
        /// </summary>
        /// <param name="mw">main_window form</param>
        /// <param name="c">container that holds the folder_name that the tip is representing</param>
        /// <param name="e">the folder_name that the tip is representing</param>
        /// <param name="d">description for the tip</param>
        /// <param name="t">type of tip</param>
        /// <param name="expand">determines if this tip will have more information to display</param>        
        public Tip(Form mw, Control e, string d, int t, bool expand)
        {
            InitializeComponent();

            Setup(mw, e, d, t, expand, ""); // set variables for the tip

            Generate_Tip(); // draws the tip 

            new_tip?.Invoke(this, this); // signal to main that a new tip is now active
        }

        /// <summary>
        /// Method for closing the tip when the form loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Form.ActiveForm != this)
            {
                close_link_label_LinkClicked(null, null);
            }
        }

        /// <summary>
        /// Sets up the Tip that is representing an Element of Sketchpop. This includes: setting the 
        /// position, choosing the color, and setting the description for the Tip.
        /// </summary>
        /// <param name="main_window">main Sketchpop window</param>
        /// <param name="container">container that holds folder_name</param>
        /// <param name="element">folder_name to add Tip for</param>
        /// <param name="desc">Tip description</param>
        /// <param name="type">type of folder_name</param>
        private void Setup(Form main_window, Control element, string desc, int type, bool expand, string folder)
        {
            this.StartPosition = FormStartPosition.Manual;

            // calculate the position for the tip

            Point elementPos = element.PointToScreen(Point.Empty);

            // set the tip's position to the center of the element's right side
            int tip_x = elementPos.X - main_window.Location.X + element.Width;
            int tip_y = elementPos.Y - main_window.Location.Y + element.Height / 2 - this.Height / 2;

            this._tip_position = new Point(tip_x, tip_y);





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
            this._type = type;
            this._desc = desc;
            this._expand = expand;
            this._expanded = false;
            this._folder = folder;

            // save dimensions for redrawing
            this._original_width = this.Width;
            this._original_height = this.Height;

            if (!this._expand) { this.more_link_label.Visible = false; } // hide label if cannot be expanded

            // add an event listener for when the window is maximized/minimized
            this._main_window.LocationChanged += MainWindow_LocationChanged;
            this._main_window.Resize += MainWindow_LocationChanged;
            this._main_window.ResizeBegin += (s, ev) =>
            {
                this.TopMost = true;
                this._timer.Stop();
            };
            this._main_window.ResizeEnd += (s, ev) =>
            {
                this._timer.Start();
                this.TopMost = false;
            };

            // check to see if the form is in focus, if not (the user clicks off of the tip) close the form
            this._timer = new Timer();
            this._timer.Tick += Timer_Tick;
            this._timer.Start();
        }      

        /// <summary>
        /// Draws the Tip form. A triangle is drawn and added to the Form's path so that
        /// the graphic better shows which folder_name is being described. 
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
                this._timer.Stop();
                // recalculate the form's position based on the new window size
                this._tip_position = Calculate_Form_Position();

                // save the old information and create a new tip with that information
                var new_tip = new Tip(this._main_window, this._element, this._desc, this._type, this._expand, this._folder);
                new_tip._expanded = this._expanded;
                new_tip.new_tip += this.new_tip;
                new_tip.closed += this.closed;
                new_tip.more_link_label.Text = this.more_link_label.Text;

                // open a new tip form at the updated position                
                if (new_tip._expanded)
                    new_tip.Expand();

                // close old form
                this.Close();

                // show the new form
                new_tip.Show();
            }
            else
            {
                // remove the event handler to avoid triggering this event
                this._main_window.LocationChanged -= MainWindow_LocationChanged;
                this._main_window.Resize -= MainWindow_LocationChanged;
                this._timer.Tick -= Timer_Tick;
            }
        }

        /// <summary>
        /// Closes the form when user clicks the 'close' link label.
        /// </summary>
        /// <param name="sender">user clicks label</param>
        /// <param name="e">n/a</param>
        private void close_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.closed?.Invoke(this, EventArgs.Empty);
            Close();
            this.Dispose();
            this._main_window.LocationChanged -= MainWindow_LocationChanged;
            this._main_window.Resize -= MainWindow_LocationChanged;
            this._timer.Tick -= Timer_Tick;
        }

        /// <summary>
        /// Displays the expanded view of this Tip when user clicks this label and the form
        /// is not expanded, and resets the form back to its original state if the form is
        /// currently expanded.
        /// </summary>
        /// <param name="sender">user clicks label</param>
        /// <param name="e">n/a</param>
        private void more_link_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            if (!_expanded)
            {
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
            this.Show();
        }

        /// <summary>
        /// Displays the extra information provided for the Tip by hiding certain Control
        /// elements, and showing others.
        /// </summary>
        private void Expand()
        {
            this._timer.Stop();
            this._timer.Tick -= Timer_Tick;
            this.TopMost = true;

            // make the form larger to show more information
            this.Height = 500;
            this.Width = 300;

            this.main_Flow_Layout_Panel.Visible = false;

            this.Show();

            this.Set_Content(this._folder); // add images, buttons, descriptions

            // show all of the hidden elements
            this.flowLayoutPanel1.Visible = true;
            this.flowLayoutPanel2.Padding = new Padding(0, 0, 20, 0);
            this.flowLayoutPanel2.Visible = true;
            this.flowLayoutPanel3.Controls.Add(more_link_label);
            this.flowLayoutPanel3.Controls.Add(close_link_label);
            this.flowLayoutPanel3.Height = 20;
            this.flowLayoutPanel3.Visible = true;

            // draw the new expanded form
            using (GraphicsPath path = new GraphicsPath())
            {
                // create the right rounded rectangle
                Rectangle rect = new Rectangle(0, 0, this.Width - this._triangle_width, this.Height);

                CheckBounds(); // move Tip within the bounds of the form

                path.AddArc(rect.Left, rect.Top, _radius, _radius, 180, 90);
                path.AddArc(rect.Right - _radius, rect.Top, _radius, _radius, 270, 90);
                path.AddArc(rect.Right - _radius, rect.Bottom - _radius, _radius, _radius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - _radius, _radius, _radius, 90, 90);

                this.Region = new Region(path);
            }
        }

        /// <summary>
        /// Resets the expanded form back to its original state by hiding the expanded
        /// Controls and making the original Controls visible.
        /// </summary>
        private void Reset()
        {
            if (this._prev_location != null)
                this.Location = this._prev_location; // reset the location if it was changed

            // reset the timer
            this._timer = new Timer();
            this._timer.Tick += Timer_Tick;
            this._timer.Start();
            this.TopMost = false;

            // move the labels to the correct layout
            this.link_Label_Layout_Panel.Controls.Add(more_link_label);
            this.link_Label_Layout_Panel.Controls.Add(close_link_label);

            // hide the expanded form elements
            this.flowLayoutPanel1.Visible = false;
            this.flowLayoutPanel2.Visible = false;
            this.flowLayoutPanel3.Visible = false;

            // display the original Tip form
            this.main_Flow_Layout_Panel.Visible = true;

            this.Height = this._original_height;
            this.Width = this._original_width;

            Generate_Tip(); // redraw the tip
        }

        /// <summary>
        /// The tip may be generated outside of the Main_Window's bounds, this ensures the tip is created inside of the Main_Window bounds
        /// </summary>
        private void CheckBounds()
        {
            if (this.Right > this._main_window.Right)
            {
                this._prev_location = this.Location;
                this.Location = new Point(this.Location.X - (this.Right - this._main_window.Right), this.Location.Y);
            }
            if (this.Bottom > this._main_window.Bottom)
            {
                this._prev_location = this.Location;
                this.Location = new Point(this.Location.X, this.Location.Y - (this.Bottom - this._main_window.Bottom));
            }
        }

        /// <summary>
        /// Finds the images and descriptions from the designated folders, and adds them to the controls
        /// of this Tip. The Tip form is resized, controls are added, and the images and descriptions
        /// are added to the new controls. *** IMPORTANT: the number of images must match the number of
        /// descriptions for a specified Tip.
        /// </summary>
        /// <param name="folder_name">name of the folder to retrieve images and descriptions from</param>
        public void Set_Content(string folder_name)
        {
            // if slides are already created, dont reload all of the elements
            if (slides.Count == 0)
            {
                // get image and description folders
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var imageFolder = Path.Combine(baseDirectory, $"..\\..\\Tip_Images\\{folder_name}");
                var descrFolder = Path.Combine(baseDirectory, $"..\\..\\Tip_Descriptions\\{folder_name}");

                // iterate through images and descriptions, adding them to our data structure for Slides

                foreach (string img_file in Directory.GetFiles(imageFolder, "*.*"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(img_file);
                    int startIndex = fileName.LastIndexOf('_') + 1; // find the position of the last '_'

                    // create a new slide with the image (if there are any)
                    int slide = int.Parse(fileName.Substring(startIndex));
                    Slide s = new Slide();
                    s._image = Image.FromFile(img_file);

                    slides.Add(slide, s);
                }

                // iterate through descriptions, add a slide if one does not already exist
                foreach (string des_file in Directory.GetFiles(descrFolder, "*.*"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(des_file);
                    int startIndex = fileName.LastIndexOf('_') + 1; // find the position of the last '_'

                    int slide = int.Parse(fileName.Substring(startIndex));

                    if (slides.ContainsKey(slide))
                    {
                        slides[slide]._description = File.ReadAllText(des_file);
                    }
                    else
                    {
                        Slide s = new Slide();
                        s._description = File.ReadAllText(des_file);

                        slides.Add(slide, s);
                    }
                }

                // display the first slide

                Slide first_slide = slides[_curr_idx];

                // if the image is not null, display image
                if (first_slide._image != null)
                {
                    pb.Image = first_slide._image;
                }
                else
                {
                    this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height * 0.6);
                    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
                    this.pb.Visible = false;
                    this.flowLayoutPanel2.Dock = DockStyle.Fill;
                    _shrink = true;
                }

                // if the description is not null, display description
                if (first_slide._description != null)
                    label1.Text = first_slide._description;

                // add navigation buttons if more that 1 slide
                if (slides.Count > 1)
                {
                    Panel sp1 = new Panel();
                    sp1.Height = 20;
                    sp1.Width = 70;
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

                    this.flowLayoutPanel1.Controls.Add(sp1);
                    this.flowLayoutPanel1.Controls.Add(left_arrow);
                    this.flowLayoutPanel1.Controls.Add(sp2);
                    this.flowLayoutPanel1.Controls.Add(right_arrow);
                }

                // create pages label
                pages = new Label();
                pages.Text = $"{_curr_idx} / {slides.Count}";
                this.flowLayoutPanel3.Controls.Add(pages);
            }
        }

        /// <summary>
        /// The click handler for the left button on the expanded Tip form. When clicked,
        /// shows the previous image if there is one.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void Left_Click(object sender, EventArgs e)
        {
            if (_curr_idx != 1)
            {
                _curr_idx--;
                pages.Text = $"{_curr_idx} / {slides.Count}";

                Slide s = slides[_curr_idx]; // get the next slide

                // image and descriptions are both shown
                if (s._image != null && s._description != null)
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Top;
                    this.flowLayoutPanel2.Dock = DockStyle.Bottom;

                    // if the previous slide was just a description
                    if (_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height / 0.6);
                        _shrink = false;
                    }

                    this.pb.Visible = true; // show the image
                }
                // only a description is shown
                else if (s._image == null && s._description != null)
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
                    this.flowLayoutPanel2.Dock = DockStyle.Fill;

                    if (!_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height * 0.6);
                        _shrink = true;
                    }

                    this.pb.Visible = false; // hide the blank image
                }
                // only an image is shown
                else
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Top;
                    this.flowLayoutPanel2.Dock = DockStyle.Bottom;

                    if (_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height / 0.6);
                        _shrink = false;
                    }

                    pb.Visible = true;
                }

                pb.Image = s._image;
                label1.Text = s._description;
            }
        }

        /// <summary>
        /// The click handler for the right button on the expanded Tip form. When clicked,
        /// shows the next image if there is one.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void Right_Click(object sender, EventArgs e)
        {
            if (_curr_idx != slides.Count)
            {
                _curr_idx++;
                pages.Text = $"{_curr_idx} / {slides.Count}";

                Slide s = slides[_curr_idx];

                // image and descriptions are both shown
                if (s._image != null && s._description != null)
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Top;
                    this.flowLayoutPanel2.Dock = DockStyle.Bottom;

                    // if the previous slide was just a description
                    if (_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height / 0.6);
                        _shrink = false;
                    }

                    this.pb.Visible = true; // show the image
                }
                // only a description is shown
                else if (s._image == null && s._description != null)
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
                    this.flowLayoutPanel2.Dock = DockStyle.Fill;

                    if (!_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height * 0.6);
                        _shrink = true;
                    }

                    this.pb.Visible = false; // hide the blank image
                }
                // only an image is shown
                else
                {
                    this.flowLayoutPanel1.Dock = DockStyle.Top;
                    this.flowLayoutPanel2.Dock = DockStyle.Bottom;

                    if (_shrink)
                    {
                        this.flowLayoutPanel1.Height = (int)(this.flowLayoutPanel1.Height / 0.6);
                        _shrink = false;
                    }

                    pb.Visible = true;
                }
                pb.Image = s._image;
                label1.Text = s._description;
            }
        }
    }

    // data structure for the elements of a single 'Slide' of the Tip form's extra content
    internal class Slide
    {
        public Image _image { get; set; }
        public string _description { get; set; }
    }
}
