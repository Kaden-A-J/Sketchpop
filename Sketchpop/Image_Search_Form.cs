using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Pipes;
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
        // variables 
        private Database_Manager _dbm = new Database_Manager();

        private List<UnsplashImage> _favorite_images;
        private List<UnsplashImage> _cached_images;
        private List<UnsplashImage> _uploaded_images;

        //private PictureBox _selected_picturebox;
        //private PictureBox _prev_selected_picturebox;
        private Dictionary<PictureBox, UnsplashImage> _currently_selected;

        public event EventHandler<SelectedImageData> _image_selected; // event for notifying the Main form

        private bool _multi_select = false;
        private bool _favs_showing = false;
        private bool _search_results_showing = false;
        private bool _uploaded_showing = false;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public Image_Search_Form()
        {
            _favorite_images = _dbm.Get_Db_Images();
            InitializeComponent();
        }

        /// <summary>
        /// Makes an API call to Unsplash to retrieve the images for the query that was
        /// searched by the user. Once the images have been collected, the images are 
        /// displayed to the user via the flow-layout.
        /// </summary>
        /// <param name="sender">user presses either view favorites or searches for a specific image</param>
        /// <param name="e">n/a</param>
        private void Get_Images(object sender, EventArgs e)
        {
            // get the user's input 
            string query = search_textbox.Text;

            // pass a cancellation token to detect user choosing other images while the current images are being displayed to flow-layout
            _cts.Cancel();
            _cts = new CancellationTokenSource();

            images_panel.Controls.Clear(); // clear the existing images if there are any

            _cached_images = _dbm.Execute_Image_Request_Query(query, int.Parse(num_images_textbox.Text)); // API call

            // only display the images if the search returned any
            if (_cached_images.Count > 0)
            {
                _search_results_showing = true;
                _favs_showing = false;
                _uploaded_showing = false;
                Add_Images_To_Panel(_cached_images, _cts);
            }
        }

        /// <summary>
        /// Converts url representations of images into actual Image objects.
        /// Method is async as to not block other operations.
        /// </summary>
        /// <param name="url">the url of the image to convert</param>
        /// <returns>returns an Image object of the url</returns>
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

        /// <summary>
        /// This method iterates through the Unsplash_Images given to this method and 
        /// creates an Image object that is placed within a Picturebox object to be 
        /// displayed to the user. The PictureBoxes are placed in a flow-layout and each
        /// PictureBox is paired with an event that detects if the user clicked on the 
        /// PictureBox. If a user clicks on the picture box, it is added to the list of 
        /// selected images for later operations. Detects if user suddenly chooses to view
        /// another set of images and cancels the operation so that the images aren't mixed
        /// together in the flow-layout.
        /// </summary>
        /// <param name="images">the images to be displayed to the user</param>
        /// <param name="cts">cancellation token for stopping the async method if user chooses to view other images while the current images are being added to the flow-layout</param>
        private async void Add_Images_To_Panel(List<UnsplashImage> images, CancellationTokenSource cts)
        {
            _currently_selected = new Dictionary<PictureBox, UnsplashImage>(); // keep track of the images the user has selected

            try
            {
                foreach (UnsplashImage image in images)
                {
                    cts.Token.ThrowIfCancellationRequested(); // throw if user selected another set of images

                    // create pictureboxes for each image
                    PictureBox pictureBox = new PictureBox();

                    if (!image.Get_Author_Profile().Equals(""))
                        pictureBox.Image = await Convert_To_ImageAsync(image.Get_Image_URL());
                    else
                    {
                        using (MemoryStream ms = new MemoryStream(image.Get_Image_Data()))
                        {
                            pictureBox.Image = Image.FromStream(ms);
                        }
                    }

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
                            else // image is not selected, add it to the selected images
                            {
                                _currently_selected.Add(pictureBox, image);

                                // add selection and color indication
                                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                pictureBox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);
                            }
                        }
                        else // a single image is being selected
                        {
                            // no images have been selected
                            if (_currently_selected.Count == 0)
                            {
                                _currently_selected.Add(pictureBox, image);

                                // add selection and color indication
                                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                pictureBox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);
                            }
                            else // there is 1 image already selected
                            {
                                if (_currently_selected.ContainsKey(pictureBox))
                                {
                                    _currently_selected.Remove(_currently_selected.Single().Key);

                                    // add selection and color indication
                                    pictureBox.BorderStyle = BorderStyle.None;
                                    pictureBox.BackColor = Color.Transparent;
                                }
                                else
                                {
                                    var tmp = _currently_selected.Single().Key; // get the existing element

                                    // set it back to its original state
                                    tmp.BorderStyle = BorderStyle.None;
                                    tmp.BackColor = Color.Transparent;

                                    _currently_selected.Remove(tmp);

                                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                    pictureBox.BackColor = Color.FromArgb(255, Color.LightGoldenrodYellow);

                                    _currently_selected.Add(pictureBox, image);
                                }
                            }
                        }
                    };

                    images_panel.Controls.Add(pictureBox); // add images to the panel
                }

                // display relevant ui to the user
                if (_search_results_showing)
                {

                }
                else if (_favs_showing)
                {

                }
                else if (_uploaded_showing)
                {

                }
            }
            catch (OperationCanceledException)
            {
                images_panel.Controls.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
         * START -- Button Click Logic
         */

        /// <summary>
        /// User presses the enter key from the search bar, calls the same method as clicking the search button.
        /// </summary>
        /// <param name="sender">user presses the enter key</param>
        /// <param name="e">key that was pressed</param>
        private void search_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(search_textbox.Text))
            {
                e.SuppressKeyPress = true;
                search_button_Click(sender, e); // user presses the search button
            }
        }

        /// <summary>
        /// User clicks this button to search for the query that they typed into the searchbar.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void search_button_Click(object sender, EventArgs e)
        {
            // check to make sure a valid number of images to search for has been chosen.
            if (num_images_textbox.Text.Equals("0")) { MessageBox.Show("Value must be greater than 0."); }
            else if (int.Parse(num_images_textbox.Text) > 30) { MessageBox.Show("Max number of returned images cannot exceed 30."); }
            else
            {
                // enable/disable unnecessary buttons
                select_button.Enabled = true;
                add_fav_button.Visible = true;
                remove_fav_button.Visible = false;

                _search_results_showing = true;
                _favs_showing = false;
                _uploaded_showing = false;

                Get_Images(sender, e);
            }
        }

        /// <summary>
        /// User clicks this button when they have selected the image
        /// they want to be displayed on the Main form for reference 
        /// use. This method notifies the Main form by invoking an event
        /// that the Main form is subscribed to.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void select_button_Click(object sender, EventArgs e)
        {
            // there should only be one image selected for reference use
            if (_currently_selected.Count == 1)
            {
                var selected = _currently_selected.Single(); // get the only entry in the dict.

                // data we want to pass to the main form
                Image thumbnail = selected.Key.Image;
                string author_name = selected.Value.Get_Author();
                string author_link = selected.Value.Get_Author_Profile();

                // send an event to the main form that the image has been selected so that the UI can update accordingly
                _image_selected?.Invoke(this, new SelectedImageData(thumbnail, author_name, author_link));

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

        /// <summary>
        /// User clicks the cancel button which closes the form.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Enables and disables the search button based on text being present in the search bar.
        /// </summary>
        /// <param name="sender">user enters or deletes text in the search bar</param>
        /// <param name="e">n/a</param>
        private void search_textbox_TextChanged(object sender, EventArgs e)
        {
            search_button.Enabled = !string.IsNullOrEmpty(search_textbox.Text);
        }

        /// <summary>
        /// User clicks this button to upload pictures locally to the database.
        /// 
        /// TODO: link locally uploaded images to the UI
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
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

        /// <summary>
        /// User clicks this button when they want to view the images
        /// stored in the 'Favorites' database. The images that exist
        /// in the db are displayed to the user via the images_panel 
        /// flow-layout.
        /// </summary>
        /// <param name="sender">the user clicks this button</param>
        /// <param name="e">n/a</param>
        private void view_fav_button_Click(object sender, EventArgs e)
        {
            // cancel the previous flow layout if images are being added when this button is clicked
            _cts.Cancel();
            _cts = new CancellationTokenSource(); // create a new cancel token

            images_panel.Controls.Clear();

            // update the local favorites collection to the 
            _favorite_images = _dbm.Get_Db_Images();

            // enable/disable unnecessary buttons
            select_button.Enabled = true;
            remove_fav_button.Visible = true;
            add_fav_button.Visible = false;

            _favs_showing = true;
            _search_results_showing = false;
            _uploaded_showing = false;

            Add_Images_To_Panel(_favorite_images, _cts); // pass the cancel token so that the search criteria can cancel if needed
        }

        /// <summary>
        /// User clicks this button when they want to view the images that were uploaded locally.
        /// These images belong to a different db than the favorites.
        /// </summary>
        /// <param name="sender">the user clicks this button</param>
        /// <param name="e">n/a</param>
        private void user_images_button_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();

            images_panel.Controls.Clear();

            _uploaded_images = _dbm.Get_User_Images();

            // enable/disable unnecessary buttons
            select_button.Enabled = true;
            remove_fav_button.Visible = true;
            add_fav_button.Visible = true;

            _uploaded_showing = true;
            _favs_showing = false;
            _search_results_showing = false;

            Add_Images_To_Panel(_uploaded_images, _cts);
        }

        /// <summary>
        /// The text box that determines the number of images searched for when making 
        /// the call to the Unsplash API for getting reference images. Textbox does not
        /// allow for non-numerical chars and checks to make sure the search bar is not
        /// empty before searching for images
        /// </summary>
        /// <param name="sender">user interacts with this textbox</param>
        /// <param name="e">user enters numerical chars or presses enter key</param>
        private void num_images_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits (0-9), backspace, and delete keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // suppresses the microsoft *ding
            }
            // Enter key
            else if (e.KeyChar == '\r' && !string.IsNullOrEmpty(search_textbox.Text))
            {
                // Perform your action here
                e.Handled = true;
                search_button_Click(sender, e);
            }
            // check to see that the search bar is not empty
            else if (e.KeyChar == '\r' && string.IsNullOrEmpty(search_textbox.Text))
            {
                MessageBox.Show("Please enter a prompt in the search bar.");
                e.Handled = true;
            }
        }

        /// <summary>
        /// User clicks this button when they want to add a selected image
        /// to the favorites database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_fav_button_Click(object sender, EventArgs e)
        {
            // only add images to the favorites collection if there are images selected
            if (_currently_selected.Count > 0)
            {
                var unique_images = new List<UnsplashImage>();

                foreach (UnsplashImage img in _currently_selected.Values)
                {
                    // ensure that repeated images cannot be added to the DB
                    if (!_favorite_images.Contains(img))
                    {
                        unique_images.Add(img);
                        _favorite_images.Add(img); // have to add them locally to update the UI
                    }
                }

                // make a MySQL query call to add the images to the DB
                if (unique_images.Count > 0)
                    _dbm.Insert_New_Images(unique_images);
            }
            else
            {
                MessageBox.Show("No image was selected.");
            }
        }

        /// <summary>
        /// Removes an image from the favorites database when the user
        /// clicks this button.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void remove_fav_button_Click(object sender, EventArgs e)
        {
            // only remove images that are selected by the user
            if (_currently_selected.Count > 0)
            {
                var pb_to_remove = new List<PictureBox>();
                var ids_to_remove = new List<string>();

                // a pair is a Picturebox containing the image and an Unsplash_Image object
                foreach (var pair in _currently_selected)
                {
                    pb_to_remove.Add(pair.Key);
                    ids_to_remove.Add(pair.Value.Get_ID());

                    // remove the image from the local favorites collection and from the flow layout
                    _favorite_images.Remove(pair.Value);
                    images_panel.Controls.Remove(pair.Key);
                }
                
                foreach (PictureBox pb in pb_to_remove)
                {
                    _currently_selected.Remove(pb);
                }

                if (ids_to_remove.Count > 0)
                {
                    if (_favs_showing)
                        _dbm.Remove_Images_By_Unsplash_Id(ids_to_remove); // make a MySQL query to delete the image from the DB
                    if (_uploaded_showing)
                        _dbm.Remove_Images_By_Id(ids_to_remove);
                }
            }
            else
            {
                MessageBox.Show("No image was selected.");
            }
        }

        /// <summary>
        /// Help Button. Displays helpful information for how to use the Image Search Form.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void help_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("User Help: Image Search\r\n\r\n" +
                "Image Search\r\n\r\n" +
                "\u2022 Use the search bar to find reference images.\r\n" +
                "\u2022 Searches are limited to 50 per hour due to Unsplash's API requirements.\r\n" +
                "\u2022 Note: Not all queries return images, and a search term is required.\r\n\r\n" +
                "Favorites\r\n\r\n" +
                "\u2022 Click 'View Favorites' to see saved images.\r\n" +
                "\u2022 Use 'Select' to add images to the canvas.\r\n" +
                "\u2022 Images can be added to favorites by clicking 'Add to Favorites'.\r\n" +
                "\u2022 Images can be removed from favorites by clicking 'Remove'.\r\n\r\n" +
                "Upload Images\r\n\r\n" +
                "\u2022 Add local images to 'My Images' by clicking 'Upload Images'.\r\n" +
                "\u2022 Supported formats: .jpg, .jpeg, .png, .gif.\r\n\r\n" +
                "My Images\r\n\r\n" +
                "\u2022 Click 'My Images to view locally uploaded images.\r\n" +
                "\u2022 Use 'Select' to add images to the canvas.\r\n" +
                "\u2022 Add them to favorites by clicking 'Add to Favorites'.\r\n\r\n" +
                "Select Multiple Images\r\n\r\n" +
                "\u2022 Enable to select multiple images for actions.\r\n" +
                "\u2022 Note: only one image can be used on the canvas at a time.\r\n\r\n" +
                "User Options\r\n" +
                "\u2022 Customize the number of images (1-30) returned by the 'Search' button.\r\n" +
                "\u2022 0 images or over 30 images are not allowed.");
        }

        /// <summary>
        /// Checkbox that sets a global boolean variable. The variable determines
        /// how many images can be selected by the user.
        /// </summary>
        /// <param name="sender">user clicks this button</param>
        /// <param name="e">n/a</param>
        private void multi_image_selection_Click(object sender, EventArgs e)
        {
            if (multi_image_selection.Checked)
            {
                _multi_select = true;
            }
            else
            {
                _multi_select = false;

                // remove all of the selected images, reset the selection logic
                if (_currently_selected != null)
                {
                    foreach (PictureBox p in _currently_selected.Keys)
                    {
                        p.BorderStyle = BorderStyle.None;
                        p.BackColor = Color.Transparent;
                    }

                    _currently_selected.Clear();
                }
            }
        }

        /*
         *  END  -- Button Click Logic
         */

        /// <summary>
        /// This class is to create a single object containing all of the necessary information
        /// needed to display to the required information as described by Unsplash's API to the 
        /// user. This data is the image, the photographer who took the image, and a link to the 
        /// author's profile page on the Unsplash website.
        /// </summary>
        public class SelectedImageData : EventArgs
        {
            private Image _selected_image;
            private string _author_name;
            private string _author_link;

            public SelectedImageData(Image selected_image, string author_name, string author_link)
            {
                _selected_image = selected_image;
                _author_name = author_name;
                _author_link = author_link;
            }

            // getters for retrieving the data
            public Image Image => _selected_image;
            public string Name => _author_name;
            public string Link => _author_link;

        }
    }
}
