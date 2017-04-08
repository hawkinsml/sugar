using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sugar
{
    public partial class ClipboardViewForm : Form
    {
        private int m_Opacity;

        public bool AutoHide { get; set; }

        public ClipboardViewForm()
        {
            AutoHide = false;
            m_Opacity = 100;
            InitializeComponent();
            Opacity = 0;
        }

        public bool ToggleForm()
        {
            bool retVal = true;
            if (Visible && Opacity > 0)
            {
                HideForm();
            }
            else
            {
                retVal = false;
                ShowForm();
            }
            return retVal;
        }

        public void ShowForm()
        {
          //  setDlgPosition();


            UpdateDisplay();
            WindowState = FormWindowState.Normal;
            Visible = true;
            TopMost = true;
            TopLevel = true;
            FadeUpTo(m_Opacity);

            if (AutoHide)
            {
                timer.Start();
            }
        }

        public void UpdateDisplay()
        {
            this.BackColor = Color.White;
            textLabel.Visible = false;
            pictureBox.Visible = false;

            if (Clipboard.ContainsImage())
            {
                DisplayImage();
            }
            else if (Clipboard.ContainsText())
            {
                DisplayText();
            }
            else
            {
                this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            }
        }

        private void DisplayText()
        {
           // this.Size = new Size(200, 200);
            textLabel.Text = Clipboard.GetText();
            textLabel.Visible = true;
        }

        private void DisplayImage()
        {
            Image clipboardImage = Clipboard.GetImage();
            if (clipboardImage != null)
            {
                Size imageSize = clipboardImage.Size;

                clipboardImage = ResizeImage(clipboardImage, this.Size.Width, this.Size.Height);


                //this.Size = new Size(200, 200);
                //setDlgPosition();
                string imagePath = Path.GetTempFileName();
                imagePath = Path.ChangeExtension(imagePath, ".bmp");
                clipboardImage.Save(imagePath);
                pictureBox.ImageLocation = imagePath;
                pictureBox.Visible = true;
            }
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
           return (Image)(new Bitmap(imgToResize, size));
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public void HideForm()
        {
            FadeDownTo(0);
        }

        private void FadeUpTo(int max)
        {
            for (int i = Int32Opacity; i <= max; i += 5)
            {
                SetOpacityAndWait(i);
            }
        }

        private void FadeDownTo(int min)
        {
            for (int i = Int32Opacity; i >= min; i -= 5)
            {
                SetOpacityAndWait(i);
            }
        }

        private void SetOpacityAndWait(int opacity)
        {
            Opacity = opacity / 100d;
            Refresh();
            Thread.Sleep(20);
        }

        private int Int32Opacity
        {
            get { return (int)(Opacity * 100); }
        }

        private void setDlgPosition()
        {
            Screen desktop = Screen.PrimaryScreen;

            Screen[] screens = Screen.AllScreens;
            int locationEnum = 3;

            switch (locationEnum)
            {
                case 0: // top left
                    {
                        Location = new Point(desktop.WorkingArea.Left + 10, desktop.WorkingArea.Top + 10);
                        break;
                    }
                case 1: // top right
                    {
                        Location = new Point(desktop.WorkingArea.Right - Width - 10, desktop.WorkingArea.Top + 10);
                        break;
                    }
                case 2: // bottom left
                    {
                        Location = new Point(desktop.WorkingArea.Left + 10, desktop.WorkingArea.Bottom - Height - 10);
                        break;
                    }
                case 3: // bottom right
                    {
                        Location = new Point(desktop.WorkingArea.Right - Width - 10, desktop.WorkingArea.Bottom - Height - 10);
                        break;
                    }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            HideForm();
        }
    }
}
