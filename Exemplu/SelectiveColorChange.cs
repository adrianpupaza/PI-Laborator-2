using System;
using System.Drawing;
using System.Windows.Forms;

namespace Exemplu
{
    public partial class SelectiveColorChange : Form
    {
        private Bitmap img;
        private Image loadedImage;
        private float sizeRatioX;
        private float sizeRatioY;
        private const int Error = 50;
        public SelectiveColorChange()
        {
            InitializeComponent();
            img = new Bitmap(pictureBox1.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
                ReplaceColor();
                MessageBox.Show(@"Done");
            }
        }

        private void ReplaceColor()
        {
            img = new Bitmap(img);
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                {
                    Color c = img.GetPixel(j, i);
                    if (IsInErrorRange(c.R, selectedColorBtn.BackColor.R) && IsInErrorRange(c.G, selectedColorBtn.BackColor.G) &&
                        IsInErrorRange(c.B, selectedColorBtn.BackColor.B))
                    {
                        img.SetPixel(j, i, Color.FromArgb(button1.BackColor.R, button1.BackColor.G, button1.BackColor.B));
                    }
                }
            pictureBox1.Image = img;
            pictureBox1.Refresh();
        }

        private static bool IsInErrorRange(byte x, byte y)
        {
            return Math.Abs(x - y) <= Error;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            try
            {
                selectedColorBtn.BackColor = img.GetPixel((int)(coordinates.X / sizeRatioX), (int)(coordinates.Y / sizeRatioY));

            }
            catch (Exception)
            {
                MessageBox.Show(@"Imagine prea mica");
            }
            txtX.Text = coordinates.X.ToString();
            txtY.Text = coordinates.Y.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                loadedImage = Image.FromFile(openFileDialog1.FileName);
                img = new Bitmap(loadedImage);
                pictureBox1.Image = img;
                pictureBox1.Refresh();
                sizeRatioX = (float)pictureBox1.Width / img.Width;
                sizeRatioY = (float)pictureBox1.Height / img.Height;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
