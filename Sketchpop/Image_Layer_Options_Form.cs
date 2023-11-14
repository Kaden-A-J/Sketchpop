using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Sketchpop
{
    public partial class Image_Layer_Options_Form : Form
    {
        private byte[] _unmodified_image;
        private Bitmap _modified_image;
        private Image _pb_image;
        private List<RadioButton> _rb = new List<RadioButton>();
        private int buffer = 4;

        public event EventHandler<Save_Data> saved;
        public event EventHandler<EventArgs> add_layer;
        public event EventHandler<EventArgs> delete_layer;

        public Image_Layer_Options_Form(byte[] ref_img, int layer_count)
        {
            InitializeComponent();

            using (MemoryStream ms = new MemoryStream(ref_img))
            {
                _pb_image = Image.FromStream(ms);
                image_pb.Image = _pb_image;
            }

            image_pb.SizeMode = PictureBoxSizeMode.Zoom;

            _unmodified_image = ref_img;

            Build_Layer_UI(layer_count);
        }

        private void opacity_slider_Scroll(object sender, EventArgs e)
        {
            float opacity = opacity_slider.Value / 255.0f; // get value from slider and convert to range [0,1]
            _modified_image = Set_Image_Opacity(_pb_image, opacity);

            // Update the PictureBox with the modified image
            image_pb.Image = _modified_image;
        }

        private Bitmap Set_Image_Opacity(Image image, float opacity)
        {
            try
            {
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = opacity;

                    ImageAttributes attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            int idx = 0;
            foreach(RadioButton rb in _rb)
            {
                if (rb.Checked)
                {
                    idx = int.Parse(rb.Parent.Name);
                }
            }

            if (_modified_image != null)
                saved?.Invoke(this, new Save_Data(_modified_image,idx));
            else
                saved?.Invoke(this, new Save_Data(_unmodified_image, idx));

            Close();
        }        

        private void add_layer_button_Click(object sender, EventArgs e)
        {
            add_layer?.Invoke(this, e);
            Add_New_Layer(_rb.Count);
        }

        private void Build_Layer_UI(int layer_count)
        {
            for (int i = 0; i < layer_count; i ++)
            {
                Add_New_Layer(i);
            }
        }

        private void Add_New_Layer(int index)
        {
            Panel t_panel2 = new Panel();
            // panel name is its layer's index to help with layer selection
            t_panel2.Name = index.ToString();
            t_panel2.BackColor = Color.FromArgb(255, 157, 157, 157);
            t_panel2.Size = new Size(this.layer_panel.Width - 2*buffer, 60);
            t_panel2.Location = new Point(4, (t_panel2.Height + buffer) * index);

            //Button deleteButton = new Button();
            //deleteButton.Text = "-";
            //deleteButton.Size = new Size(20, 20);
            //deleteButton.Location = new Point(t_panel2.Width - deleteButton.Width - buffer, t_panel2.Height / 2 - deleteButton.Height / 2);
            //deleteButton.Click += new EventHandler(DeleteLayerButtonClick); // Attach a click event handler
            //t_panel2.Controls.Add(deleteButton);

            RadioButton t_visible_button2 = new RadioButton();
            //t_visible_button2.c
            t_visible_button2.Size = new Size(20, 20);
            t_visible_button2.Location = new Point(buffer, t_panel2.Height / 2 - t_visible_button2.Height / 2);
            t_panel2.Controls.Add(t_visible_button2);

            Panel t_preview_panel2 = new Panel();
            t_preview_panel2.BackColor = Color.FromArgb(255, 167, 167, 167);
            t_preview_panel2.Size = new Size((t_panel2.Height - buffer * 2) / 9 * 16, t_panel2.Height - buffer * 2);
            t_preview_panel2.Location = new Point(t_visible_button2.Width + buffer, buffer);
            t_panel2.Controls.Add(t_visible_button2);

            Label t_name_label2 = new Label();
            t_name_label2.Location = new Point(t_preview_panel2.Location.X + t_preview_panel2.Width + buffer, t_panel2.Height / 4);
            t_name_label2.Text = "layer (" + index.ToString() + ")";
            t_panel2.Controls.Add(t_name_label2);

            flowLayoutPanel1.Controls.Add(t_panel2);

            t_visible_button2.Click += T_visible_button2_Click;
            _rb.Add(t_visible_button2);
        }

        private void DeleteLayerButtonClick(object sender, EventArgs e)
        {
            Button b = (Button)(sender);

            layer_panel.Controls.Remove(b.Parent);
        }

        private void T_visible_button2_Click(object sender, EventArgs e)
        {
            RadioButton c_button = (RadioButton)(sender);

            // uncheck old selection
            foreach(RadioButton rb in _rb)
            {
                if (rb != c_button)
                {
                    rb.Checked = false;
                }
            }
        }

        public class Save_Data : EventArgs
        {
            private byte[] _img_data;
            private int _layer_index;
            public Save_Data(Bitmap img, int layer_idx)
            {
                ImageConverter converter = new ImageConverter();
                _img_data = (byte[])converter.ConvertTo(img, typeof(byte[]));
                _layer_index = layer_idx;
            }

            public Save_Data(byte[] img_data, int layer_idx)
            {
                _img_data = img_data;
                _layer_index = layer_idx;
            }

            public byte[] Image_Data => _img_data;
            public int Layer => _layer_index;
        }
    }
}
