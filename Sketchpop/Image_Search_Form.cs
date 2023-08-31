using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchpop
{
    public partial class Image_Search_Form : Form
    {
        private Database_Manager _dbm = new Database_Manager();
        private List<UnsplashImage> _favorite_images;
        private List<UnsplashImage> _cached_images = new List<UnsplashImage>();

        private PictureBox _main_form_pb;
        private PictureBox _selected_picturebox;
        private PictureBox _prev_selected_picturebox;

        private string _last_searched_query;
        private int _num_pictures;

        private Color selected_image_color = Color.SkyBlue;

        private bool _multi_select = false;
        private bool _favs_showing = false;
        private bool _search_results_showing = false;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private Dictionary<PictureBox, UnsplashImage> _currently_selected;

        public Image_Search_Form(PictureBox main_form_pb)
        {
            _main_form_pb = main_form_pb;
            _favorite_images = _dbm.Get_Db_Images();

            InitializeComponent();
        }

        private void Get_Images(object sender, EventArgs e)
        {
            string query = search_textbox.Text; // get the user's query
            _num_pictures = int.Parse(num_images_textbox.Text);

            _cts.Cancel();
            _cts = new CancellationTokenSource();

            images_panel.Controls.Clear();

            _cached_images = _dbm.Execute_Image_Request_Query(query, _num_pictures);

            // get images from API call
            if (_cached_images.Count > 0)
            {
                _search_results_showing = true;
                _favs_showing = false;
                Add_Images_To_Panel(_cached_images, _cts);
            }            
        }

        private async Task<Image> Convert_To_ImageAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var image = Image.FromStream(stream);

                    return image;
                }
            }
        }

        private async void Add_Images_To_Panel(List<UnsplashImage> images, CancellationTokenSource cts)
        {
            _currently_selected = new Dictionary<PictureBox, UnsplashImage>();

            try
            {
                foreach (UnsplashImage image in images)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    // create pictureboxes for each image
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = await Convert_To_ImageAsync(image.Get_Image_URL());
                    pictureBox.Width = 100;
                    pictureBox.Height = 100;
                    pictureBox.Margin = new Padding(5);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    // each picturebox gets an event to detect user selection
                    pictureBox.Click += (s, args) =>
                    {
                        if (_multi_select) // multiple images can be selected
                        {
                            // if the picturebox is in the dict, it is selected and needs to be deselected on the click
                            if (_currently_selected.ContainsKey(pictureBox))
                            {
                                _currently_selected.Remove(pictureBox);

                                // remove selection and color
                                pictureBox.BorderStyle = BorderStyle.None;
                                pictureBox.BackColor = Color.Transparent;
                            }
                            else
                            {
                                _currently_selected.Add(pictureBox, image);

                                // add selection and color indication
                                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                pictureBox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);
                            }
                        }
                        else // a single image is being selected
                        {
                            // no image has been selected yet
                            if (_selected_picturebox == null)
                            {
                                _selected_picturebox = pictureBox; // save the clicked pb as the selected one
                                _prev_selected_picturebox = _selected_picturebox;

                                // indicate selection to the user
                                _selected_picturebox.BorderStyle = BorderStyle.FixedSingle;
                                _selected_picturebox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);

                                _currently_selected.Add(_selected_picturebox, image);
                            }
                            else
                            {
                                // if user clicks on the same image, 'unselect' it
                                if (pictureBox == _selected_picturebox)
                                {
                                    _selected_picturebox.BorderStyle = BorderStyle.None;
                                    _selected_picturebox.BackColor = Color.Transparent;

                                    _currently_selected.Remove(_selected_picturebox);

                                    _selected_picturebox = null;
                                    _prev_selected_picturebox = null;
                                }
                                else // otherwise, change selection to the newly selected image
                                {
                                    // remove old selection
                                    _prev_selected_picturebox.BorderStyle = BorderStyle.None;
                                    _prev_selected_picturebox.BackColor = Color.Transparent;

                                    _currently_selected.Remove(_prev_selected_picturebox);

                                    _selected_picturebox = pictureBox;
                                    _selected_picturebox.BorderStyle = BorderStyle.FixedSingle;
                                    _selected_picturebox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);

                                    _currently_selected.Add(_selected_picturebox, image);

                                    _prev_selected_picturebox = _selected_picturebox;
                                }
                            }

                        }
                    };

                    images_panel.Controls.Add(pictureBox); // add images to the panel
                }

                // display relevant ui to the user
                if (_search_results_showing)
                {
                    select_button.Enabled = true;
                    add_fav_button.Enabled = true;
                    remove_fav_button.Visible = false;
                }
                else if (_favs_showing)
                {
                    remove_fav_button.Visible= true;
                    add_fav_button.Enabled = false;
                    select_button.Enabled = true;
                    view_fav_button.Enabled = true;
                }

                // reset the selected image when new images are pulled in
                _selected_picturebox = null;
                _prev_selected_picturebox = null;
            }
            catch (OperationCanceledException)
            {
                images_panel.Controls.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        /*
         * START -- Button Click Logic
         */

        private void search_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(search_textbox.Text))
            {
                e.SuppressKeyPress = true;
                search_button_Click(sender, e); // user presses the search button
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (num_images_textbox.Text.Equals("0")) { MessageBox.Show("Value must be greater than 0."); }
            else
            {
                Get_Images(sender, e);
            }
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            if (_currently_selected.Count == 1)
            {
                _main_form_pb.Image = _currently_selected.Single().Key.Image;
                Close();
            }
            else if (_currently_selected.Count > 1 && _currently_selected.Count != 0)
            {
                MessageBox.Show("Only one image can be selected at a time.");
            }
            else
            {
                MessageBox.Show("No image was selected.");
            }           
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void search_textbox_TextChanged(object sender, EventArgs e)
        {
            search_button.Enabled = !string.IsNullOrEmpty(search_textbox.Text);
        }

        private void upload_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.SafeFileName; // Gets the file name
                string filePath = openFileDialog.FileName; // Gets the full file path
                _dbm.Execute_Local_Picture_Upload_Query(fileName, filePath);
            }
        }

        private void view_fav_button_Click(object sender, EventArgs e)
        {            
            // cancel the previous flow layout if images are being added when this button is clicked
            _cts.Cancel();
            _cts = new CancellationTokenSource(); // create a new cancel token

            images_panel.Controls.Clear();

            view_fav_button.Enabled = false; // disable button so that user cant repeatedly press it (weird behavior if users allowed to press multiple times)         
            _favs_showing = true;
            _search_results_showing = false;

            _favorite_images = _dbm.Get_Db_Images();

            Add_Images_To_Panel(_favorite_images, _cts); // pass the cancel token so that the search criteria can cancel if needed
        }

        private void num_images_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits (0-9), backspace, and delete keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the non-numeric input
            }
            else if (e.KeyChar == '\r' && !string.IsNullOrEmpty(search_textbox.Text)) // Enter key
            {
                // Perform your action here
                e.Handled = true;
                search_button_Click(sender, e);
            }
            else if (e.KeyChar == '\r' && string.IsNullOrEmpty(search_textbox.Text))
            {
                MessageBox.Show("Please enter a prompt in the search bar.");
                e.Handled = true;
            }
        }

        private void add_fav_button_Click(object sender, EventArgs e)
        {
            if (_currently_selected.Count > 0)
            {
                var unique_images = new List<UnsplashImage>();

                foreach (UnsplashImage img in _currently_selected.Values)
                {
                    if (!_favorite_images.Contains(img))
                    {
                        unique_images.Add(img);
                        _favorite_images.Add(img);
                    }
                }

                if (unique_images.Count > 0)
                    _dbm.Insert_New_Images(unique_images);
            }
        }

        private void remove_fav_button_Click(object sender, EventArgs e)
        {
            if (_currently_selected.Count > 0)
            {
                var ids_to_remove = new List<string>();

                foreach (var pair in _currently_selected)
                {
                    ids_to_remove.Add(pair.Value.Get_ID());
                    _favorite_images.Remove(pair.Value);
                    images_panel.Controls.Remove(pair.Key);
                }

                _dbm.Remove_Images_By_Id(ids_to_remove);

                _cts = new CancellationTokenSource();                
            }
            else
            {
                MessageBox.Show("No image was selected.");
            }
        }

        private void multi_image_selection_Click(object sender, EventArgs e)
        {
            if (multi_image_selection.Checked)
            {
                _multi_select = true;
            }
            else
            {
                _multi_select = false;

                if (_currently_selected != null)
                {
                    foreach (PictureBox p in _currently_selected.Keys)
                    {
                        p.BorderStyle = BorderStyle.None;
                        p.BackColor = Color.Transparent;
                    }

                    _currently_selected.Clear();
                    
                    _prev_selected_picturebox = null;
                    _selected_picturebox = null;
                }
            }
        }

        /*
         *  END  -- Button Click Logic
         */        
    }
}
