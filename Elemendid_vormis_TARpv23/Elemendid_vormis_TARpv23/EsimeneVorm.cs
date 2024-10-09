using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Elemendid_vormis_TARpv23
{
    public partial class EsimeneVorm : Form
    {
        PictureBox pb1 = new PictureBox();
        System.Windows.Forms.CheckBox chk;
        ColorDialog ColorDialog1 = new ColorDialog();
        Button bt = new Button();
        

        public EsimeneVorm(int h,int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Esimene vorm";

            int x = 300;
            int y = 440;
            int buttonWidth = 75;
            int buttonSpacing = 10;

            Button btnEsimene = new Button { Text = "Close", Size = new Size(buttonWidth, 20), Location = new Point(x, y) };
            btnEsimene.Click += Click_CloseButton;
            Controls.Add(btnEsimene);

            x += buttonWidth + buttonSpacing;

            Button btnTeine = new Button { Text = "Set a background picture", Size = new Size(buttonWidth, 20), Location = new Point(x, y) };
            btnTeine.Click += backgroundButton_Click;
            Controls.Add(btnTeine);

            x += buttonWidth + buttonSpacing;

            Button btnKolmas = new Button { Text = "Clear the picture", Size = new Size(buttonWidth, 20), Location = new Point(x, y) };
            btnKolmas.Click += Click_ClearPictureButton;
            Controls.Add(btnKolmas);

            x += buttonWidth + buttonSpacing;

            Button btnNeljas = new Button { Text = "Show a picture", Size = new Size(buttonWidth, 20), Location = new Point(x, y) };
            btnNeljas.Click += Click_ShowPictureButton;
            Controls.Add(btnNeljas);

            chk = new System.Windows.Forms.CheckBox();
            chk.Checked = false;
            chk.Text = "Stretch";
            chk.Size = new Size(buttonWidth, 20);
            chk.Location = new Point(20,440);
            chk.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);

            Controls.Add(chk);

        }

        private void Click_ClearPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = null;
        }

        private void Click_ShowPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = Image.FromFile(@"..\..\..\Picture1.jpg");
            pb1.Location = new Point(0,0);
            pb1.Size = new Size(700,700);
            this.Controls.Add(pb1);
        }

        private void Click_CloseButton(object? sender, EventArgs e)
        {
            this.Close();
        }
        private void backgroundButton_Click(object sender, System.EventArgs e)
        {
            ColorDialog1.ShowDialog();
            this.BackColor = ColorDialog1.Color;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box, 
            // change the PictureBox's
            // SizeMode property to "Stretch". If the user clears 
            // the check box, change it to "Normal".
            if (chk.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
