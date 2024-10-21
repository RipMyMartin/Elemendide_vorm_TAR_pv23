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
    public partial class KolmasVorm : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        string firstChoice;
        string secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        Label lblStatus;
        Label lblTimeLeft;
        System.Windows.Forms.Timer GameTimer;
        int totalTime = 60;
        int countDownTime;
        bool gameOver = false;

        Button btnRestart;
        Button btnCheckAnswers;

        public KolmasVorm(int h,int w)
        {
            InitializeComponent();
            this.ClientSize = new Size(480,650);
            this.BackColor = Color.White;
            GameTimer = new System.Windows.Forms.Timer();
            GameTimer.Interval = 1000;
            GameTimer.Tick += TimerEvent;

            lblStatus = new Label
            {
                Location = new Point(20, 520),
                Size = new Size(200, 40),
                ForeColor = Color.Black
            };
            lblTimeLeft = new Label
            {
                Location = new Point(20, 560),
                Size = new Size(200, 40),
                ForeColor = Color.Black,
            };
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTimeLeft);

            AddRestartButton();
            AddCheckAnswersButton();

            LoadPictures();
        }


        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            lblTimeLeft.Text = "Time Left: " + countDownTime;
            if (countDownTime < 1)
            {
                GameOver("Time's Up, You Lose");
                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile(@"..\..\..\" + (string)x.Tag + ".png");
                    }
                }
            }
        }

        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 100;
                newPic.Width = 100;
                newPic.BackColor = Color.Blue;
                newPic.SizeMode = PictureBoxSizeMode.Zoom;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 4)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos += 110;
                }
                if (rows == 4)
                {
                    leftPos = 20;
                    topPos += 110;
                    rows = 0;
                }
            }
            RestartGame();
        }
        private void NewPic_Click(object sender, EventArgs e)
        {
            {
            if (gameOver)
                return;
            }
            if (firstChoice == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile(@"..\..\..\" + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Image == null)
                {
                    picB.Image = Image.FromFile(@"..\..\..\" + (string)picB.Tag + ".png");
                    secondChoice = (string)picB.Tag;
                }
            }
            else
            {
                CheckPictures(picA, picB);
            }
        }

        private void RestartGame()
        {
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            numbers = randomList;
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();
            }
            tries = 0;
            gameOver = false;
            countDownTime = totalTime;
            GameTimer.Start();
        }

        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null;
            }
            else
            {
                tries++;
            }
            firstChoice = null;
            secondChoice = null;
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
        }

        private void GameOver(string msg)
        {
            GameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg + " Click Restart to Play Again.");
        }

        private void AddCheckAnswersButton()
        {
            btnCheckAnswers = new Button
            {
                Text = "Check result",
                Size = new Size(120, 30),
                Location = new Point(50, 480)
            };
            btnCheckAnswers.Click += CheckAnswersButton_Click;
            this.Controls.Add(btnCheckAnswers);
        }
        private void AddRestartButton()
        {
            btnRestart = new Button();
            btnRestart.Text = "Restart";
            btnRestart.Size = new Size(100, 30);
            btnRestart.Location = new Point(180, 480);
            btnRestart.Click += btnRestart_Click;
            this.Controls.Add(btnRestart);
        }

        private void CheckAnswersButton_Click(object sender, EventArgs e)
        {
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
            else
            {
                MessageBox.Show("There are still unmatched pairs!", "Check result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }



    }
}
