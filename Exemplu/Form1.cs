﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Exemplu
{
    public partial class Lab2 : Form
    {
        private Bitmap _imgLeft, _imgRight;
        private Image _loadedImage;

        public Lab2()
        {
            InitializeComponent();
            _imgLeft = new Bitmap(pictureBox1.Image);
            _imgRight = new Bitmap(pictureBox2.Image);
        }

        private void openLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            _loadedImage = Image.FromFile(openFileDialog1.FileName);
            _imgLeft = new Bitmap(_loadedImage);
            pictureBox1.Image = _imgLeft;
            pictureBox1.Refresh();
        }

        private void saveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            _imgRight.Save(saveFileDialog1.FileName);
        }

        private void exempluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            for (int i = 0; i < _imgLeft.Height; i++)
                for (int j = 0; j < _imgLeft.Width; j++)
                {
                    Color c = _imgLeft.GetPixel(j, i);
                    _imgRight.SetPixel(j, i, Color.FromArgb(c.B, c.G, c.R));  // Swap (R,B)
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
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
            Close();
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar1.Minimum = -100;
            trackBar1.Maximum = 100;
            trackBar1.Value = 0;
            trackBar1.Show();
        }

        private void compresieDeContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);

            for (int i = 0; i < _imgLeft.Height; i++)
                for (int j = 0; j < _imgLeft.Width; j++)
                {
                    var oldColor = _imgLeft.GetPixel(j, i);
                    double red = 255 / Math.Log(256) * Math.Log(1 + oldColor.R);
                    double green = 255 / Math.Log(256) * Math.Log(1 + oldColor.G);
                    double blue = 255 / Math.Log(256) * Math.Log(1 + oldColor.B);
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    var newColor = Color.FromArgb(oldColor.A, (int)red, (int)green, (int)blue);
                    _imgRight.SetPixel(j, i, newColor);
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            for (int i = 0; i < _imgRight.Height; i++)
                for (int j = 0; j < _imgRight.Width; j++)
                {
                    Color c = _imgRight.GetPixel(j, i);
                    int r = (int)(c.R * 1.5);
                    if (r > 255) r = 255;
                    _imgRight.SetPixel(j, i, Color.FromArgb(r, c.G / 2, c.B / 2));  // Swap (R,B)
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            for (int i = 0; i < _imgRight.Height; i++)
                for (int j = 0; j < _imgRight.Width; j++)
                {
                    Color c = _imgRight.GetPixel(j, i);
                    int green = (int)(c.G * 1.5);
                    if (green > 255) green = 255;
                    _imgRight.SetPixel(j, i, Color.FromArgb(c.R / 2, green, c.B / 2));  // Swap (R,B)
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            for (int i = 0; i < _imgRight.Height; i++)
                for (int j = 0; j < _imgRight.Width; j++)
                {
                    Color c = _imgRight.GetPixel(j, i);
                    int blue = (int)(c.B * 1.5);
                    if (blue > 255) blue = 255;
                    _imgRight.SetPixel(j, i, Color.FromArgb(c.R / 2, c.G / 2, blue));  // Swap (R,B)
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
        }

        private void btnToLeft_Click(object sender, EventArgs e)
        {
            _imgLeft = _imgRight;
            pictureBox1.Image = _imgRight; pictureBox1.Refresh();
        }

        private void filtrareTCSiTBToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            for (int i = 1; i < _imgLeft.Height - 1; i++)
                for (int j = 1; j < _imgLeft.Width - 1; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -1; ii <= 1; ii++)
                    {
                        for (int jj = -1; jj <= 1; jj++)
                        {
                            int var;
                            if (ii == 0 && jj == 0)
                            {
                                var = 9;
                            }
                            else
                            {
                                var = -1;
                            }
                            red += _imgLeft.GetPixel(j + jj, i + ii).R * var;
                            green += _imgLeft.GetPixel(j + jj, i + ii).G * var;
                            blue += _imgLeft.GetPixel(j + jj, i + ii).B * var;
                        }
                    }
                    red = setInInterval(red);
                    green = setInInterval(green);
                    blue = setInInterval(blue);

                    _imgRight.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }
            pictureBox2.Image = _imgRight;
            pictureBox2.Refresh();
        }

        private int setInInterval(int color)
        {
            if (color < 0)
            {
                color = 0;
            }
            else if (color > 255)
            {
                color = 255;
            }

            return color;
        }

        private void dilatareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);
            const float f = 2f;
            for (int i = 1; i < _imgRight.Width - 1; i++)
            {
                for (int j = 1; j < _imgRight.Height - 1; j++)
                {
                    int k = (int)((i - 1) / f) + 1; //+ _imgLeft.Width/2;
                    int l = (int)((j - 1) / f) + 1; //+ _imgLeft.Height/2;
                    _imgRight.SetPixel(i, j, _imgLeft.GetPixel(k, l));
                }
            }
            pictureBox2.Image = _imgRight;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            _imgRight = new Bitmap(_imgLeft);

            double contrast = Math.Pow((100.0 + trackBar1.Value) / 100.0, 2);

            for (int i = 0; i < _imgLeft.Height; i++)
                for (int j = 0; j < _imgLeft.Width; j++)
                {

                    var oldColor = _imgLeft.GetPixel(j, i);
                    double red = ((((oldColor.R / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    double green = ((((oldColor.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    double blue = ((((oldColor.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    var newColor = Color.FromArgb(oldColor.A, (int)red, (int)green, (int)blue);
                    _imgRight.SetPixel(j, i, newColor);
                }
            pictureBox2.Image = _imgRight; pictureBox2.Refresh();
        }
    }
}
