using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class TeineVorm : Form
    {
        Button btn;

        public TeineVorm(int h, int w)
        {
            this.Height= h;
            this.Width= w;
            this.Text = "Teine vorm";

            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 70;
            btn.Location = new Point(150, 50);
            btn.Click += Btn_Click;
            Controls.Add(btn);

        }

        int t = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            t++;
            if (t % 2 == 0)
            {
                btn.BackColor = Color.Red;
            }
            else
            {
                btn.BackColor = Color.Green;
            }
        }
    }
}
