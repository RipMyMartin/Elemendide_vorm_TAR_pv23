using System;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class EsimeneVorm : Form
    {
        PictureBox pb1 = new PictureBox();
        CheckBox chk;
        ColorDialog ColorDialog1 = new ColorDialog();
        bool isCircular = false;
        Bitmap? bmp;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public EsimeneVorm(int h, int w)
        {
            this.Height = 600;
            this.Width = 1150;
            this.Text = "Esimene Vorm";
            this.BackColor = Color.LightBlue;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeButtons();
            InitializePictureBox();
            InitializeCheckBox();
        }

        private void InitializeButtons()
        {
            int x = 20;
            int y = 520;
            int buttonWidth = 150;
            int buttonSpacing = 10;

            Button btnYourFile = CreateButton("Your File", x, y, Click_btnYouFile);

            x += buttonWidth + buttonSpacing;

            Button btnNewButton = CreateButton("Blurring", x, y, Click_blurring);

            x = 20;
            y = 480;

            Button btnClose = CreateButton("Close", x, y, Click_CloseButton);
            x += buttonWidth + buttonSpacing;

            Button btnBackground = CreateButton("Set Background Picture", x, y, backgroundButton_Click);
            x += buttonWidth + buttonSpacing;

            Button btnClear = CreateButton("Clear Picture", x, y, Click_ClearPictureButton);
            x += buttonWidth + buttonSpacing;

            Button btnShow = CreateButton("Show Picture", x, y, Click_ShowPictureButton);
            x += buttonWidth + buttonSpacing;

            Button btnCircular = CreateButton("Toggle Circular", x, y, Click_btncircularButton);
            x += buttonWidth + buttonSpacing;

            Button btnRotate90 = CreateButton("Rotate 90°", x, y, Click_btnRotateButton90);
            x += buttonWidth + buttonSpacing;

            Button btnRotateM90 = CreateButton("Rotate -90°", x, y, Click_btnRotateButtonM90);
        }

        private void Click_blurring(object? sender, EventArgs e)
        {
            if (pb1.Image != null)
            {
                int newWidth = (int)(pb1.Image.Width * 0.8);
                int newHeight = (int)(pb1.Image.Height * 0.8);
                Bitmap newImage = new (pb1.Image, new Size(newWidth, newHeight));
                pb1.Image = newImage;
            }
        }

        private Button CreateButton(string text, int x, int y, EventHandler clickEvent)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(150, 40),
                Location = new Point(x, y),
                BackColor = Color.CadetBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            button.Click += clickEvent;
            Controls.Add(button);
            return button;
        }

        private void InitializePictureBox()
        {
            pb1.Location = new Point(10, 10);
            pb1.Size = new Size(1100, 400);
            pb1.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pb1);
        }

        private void InitializeCheckBox()
        {
            chk = new CheckBox
            {
                Text = "Stretch Image",
                Size = new Size(120, 20),
                Location = new Point(20, 440),
                BackColor = Color.LightBlue,
                ForeColor = Color.Black
            };
            chk.CheckedChanged += CheckBox1_CheckedChanged;
            Controls.Add(chk);
        }

        private void Click_CloseButton(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (ColorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = ColorDialog1.Color;
            }
        }

        private void Click_ClearPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = null;
            bmp = null;
            this.BackColor = Color.White;
        }

        private void Click_ShowPictureButton(object? sender, EventArgs e)
        {
                bmp = new Bitmap(@"..\..\..\Picture1.jpg");
                pb1.Image = bmp;
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void Click_btnYouFile(object? sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                bmp = new Bitmap(filePath);
                pb1.Image = bmp;
                pb1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void Click_btnRotateButton90(object? sender, EventArgs e)
        {
            if (bmp != null)
            {
                bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);
                pb1.Image = bmp;
                pb1.Invalidate();
            }
        }

        private void Click_btnRotateButtonM90(object? sender, EventArgs e)
        {
            if (bmp != null)
            {
                bmp.RotateFlip(RotateFlipType.Rotate270FlipXY);
                pb1.Image = bmp;
                pb1.Invalidate();
            }
        }

        private void Click_btncircularButton(object? sender, EventArgs e)
        {
            if (!isCircular)
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, pb1.Width, pb1.Height);
                Region region = new Region(path);
                pb1.Region = region;
            }
            else
            {
                pb1.Region = null;
            }
            isCircular = !isCircular;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
