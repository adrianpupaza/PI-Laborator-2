using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exemplu
{
    public partial class Exemplu : Form
    {
        Bitmap Im1, Im2;
        private Image loadedImage;

        public Exemplu()
        {
            InitializeComponent();
            Im1 = new Bitmap(pictureBox1.Image);
            Im2 = new Bitmap(pictureBox2.Image);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void openLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            loadedImage = Image.FromFile(openFileDialog1.FileName);
            Im1 = new Bitmap(loadedImage);
            pictureBox1.Image = Im1;
            pictureBox1.Refresh();
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            Im2.Save(saveFileDialog1.FileName);
        }

        private void exempluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Im2 = new Bitmap(Im1);
            for (int i=0; i<Im1.Height; i++)
                for (int j=0; j<Im1.Width; j++) {
                    Color c = Im1.GetPixel(j, i);
                    Im2.SetPixel(j,i,Color.FromArgb(c.B,c.G,c.R));  // Swap (R,B)
                }
            pictureBox2.Image = Im2; pictureBox2.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openLeftToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openLeftToolStripMenuItem_Click(sender, e);
        }

        private void saveRightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveRightToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exempluToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exempluToolStripMenuItem_Click(sender, e);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Im2 = new Bitmap(Im1);
            for (int i = 0; i < Im1.Height; i++)
                for (int j = 0; j < Im1.Width; j++)
                {
                    Color c = Im1.GetPixel(j, i);
                    int r = (int) (c.R * 1.5);
                    if (r > 255) r = 255;
                    Im2.SetPixel(j, i, Color.FromArgb(r, c.G/2, c.B/2));  // Swap (R,B)
                }
            pictureBox2.Image = Im2; pictureBox2.Refresh();
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            redToolStripMenuItem_Click(sender, e);
        }
    }
}
