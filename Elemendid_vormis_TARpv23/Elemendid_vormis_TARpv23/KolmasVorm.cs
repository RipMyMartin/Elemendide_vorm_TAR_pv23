using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmasVorm : Form
    {
        private List<int> numbers = new () { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        private string? firstChoice;
        private string? secondChoice;
        private int tries;
        private List<PictureBox> pictures = new();
        private PictureBox picA;
        private PictureBox picB;
        private Label lblTimeLeft;
        private System.Windows.Forms.Timer gameTimer;
        private int totalTime = 60;
        private int countDownTime;
        private bool gameOver = false;
        private Button btnRestart;
        private Button btnCheckAnswers;
        private Button btnPause;
        private bool isPaused = false;

        public KolmasVorm()
        {
            InitializeComponent();
            this.ClientSize = new Size(480, 650);
            this.BackColor = Color.LightCyan;

            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            gameTimer.Tick += TimerEvent;

            lblTimeLeft = new Label
            {
                Location = new Point(185, 50),
                Size = new Size(200, 40),
                ForeColor = Color.DarkBlue,
                Font = new Font("Arial", 12, FontStyle.Bold),
            };
            Controls.Add(lblTimeLeft);

            AddRestartButton();
            AddCheckAnswersButton();
            LoadPictures();
            AddPauseButton();
        }
        private void AddPauseButton()
        {
            btnPause = new Button
            {
                Text = "Pause",
                Size = new Size(100, 30),
                Location = new Point(260, 550),
                BackColor = Color.LightGray,
                ForeColor = Color.DarkSlateGray,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                gameTimer.Start();
                btnPause.Text = "Pause";
            }
            else
            {
                gameTimer.Stop();
                btnPause.Text = "Resume";
            }
            isPaused = !isPaused;
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

            topPos += 70;

            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new()
                {
                    Height = 100,
                    Width = 100,
                    BackColor = Color.LightSkyBlue,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);

                if (rows < 4)
                {
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos += 110;
                    rows++;
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
            if (gameOver) return;

            if (firstChoice == null)
            {
                picA = (PictureBox)sender;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile(@"..\..\..\" + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                picB = (PictureBox)sender;
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
            gameTimer.Start();
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

            bool allCollected = true;
            foreach (var pic in pictures)
            {
                if (pic.Tag != null)
                {
                    allCollected = false;
                    break;
                }
            }

            if (allCollected)
            {
                GameOver("Great Work, You Win!!!!");
            }
        }

        private void GameOver(string msg)
        {
            gameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg + " Click Restart to Play Again.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddCheckAnswersButton()
        {
            btnCheckAnswers = new Button
            {
                Text = "Check Answer",
                Size = new Size(120, 30),
                Location = new Point(20, 550),
                BackColor = Color.LightGreen,
                ForeColor = Color.DarkGreen,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnCheckAnswers.Click += BtnCheckAnswers_Click;
            this.Controls.Add(btnCheckAnswers);
        }

        private void AddRestartButton()
        {
            btnRestart = new Button
            {
                Text = "Restart",
                Size = new Size(100, 30),
                Location = new Point(150, 550),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnRestart.Click += BtnRestart_Click;
            this.Controls.Add(btnRestart);
        }


        private void BtnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RevealAllCards()
        {
            foreach (PictureBox pic in pictures)
            {
                if (pic.Tag != null)
                {
                    pic.Image = Image.FromFile(@"..\..\..\" + (string)pic.Tag + ".png");
                }
            }
        }

        private void BtnCheckAnswers_Click(object sender, EventArgs e)
        {
            RevealAllCards();

            gameTimer.Stop();
            gameOver = true;
            MessageBox.Show("Great Work, You Win!!!!" + " Click Restart to Play Again.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
