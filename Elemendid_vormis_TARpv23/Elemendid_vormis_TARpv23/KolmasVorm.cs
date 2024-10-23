using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class KolmasVorm : Form
    {
        // List of numbers representing pairs
        private List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };

        // Game variables
        private string firstChoice;
        private string secondChoice;
        private int tries;
        private List<PictureBox> pictures = new List<PictureBox>();
        private PictureBox picA;
        private PictureBox picB;
        private Label lblStatus;
        private Label lblTimeLeft;
        System.Windows.Forms.Timer gameTimer;
        private int totalTime = 60;
        private int countDownTime;
        private bool gameOver = false;

        private Button btnRestart;
        private Button btnCheckAnswers;

        public KolmasVorm(int h, int w)
        {
            InitializeComponent();
            this.ClientSize = new Size(480, 650);
            this.BackColor = Color.LightCyan; // Softer background color

            // Initialize Timer
            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second
            };
            gameTimer.Tick += TimerEvent;

            // Initialize Labels
            lblStatus = new Label
            {
                Location = new Point(20, 520),
                Size = new Size(200, 40),
                ForeColor = Color.DarkBlue, // Dark text color for better contrast
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            lblTimeLeft = new Label
            {
                Location = new Point(20, 560),
                Size = new Size(200, 40),
                ForeColor = Color.DarkBlue,
                Font = new Font("Arial", 12, FontStyle.Bold),
            };
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTimeLeft);

            // Add buttons and load pictures
            AddRestartButton();
            AddCheckAnswersButton();
            LoadPictures();
        }

        // Timer event to manage countdown
        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            lblTimeLeft.Text = "Time Left: " + countDownTime;
            if (countDownTime < 1)
            {
                GameOver("Time's Up, You Lose");
                // Show all images if time's up
                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile(@"..\..\..\" + (string)x.Tag + ".png");
                    }
                }
            }
        }

        // Load PictureBoxes for the game
        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;

            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new PictureBox
                {
                    Height = 100,
                    Width = 100,
                    BackColor = Color.LightSkyBlue, // Light background for PictureBoxes
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                
                // Positioning the PictureBox
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

        // Picture click event handler
        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

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

        // Restart the game
        private void RestartGame()
        {
            // Shuffle the numbers
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

        // Check if the selected pictures match
        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null; // Remove tags for matched pairs
            }
            else
            {
                tries++;
            }
            firstChoice = null;
            secondChoice = null;

            // Hide unmatched pictures
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }

            // Check if all pairs are matched
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
        }

        // Handle game over scenario
        private void GameOver(string msg)
        {
            gameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg + " Click Restart to Play Again.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Add Check Answers button
        private void AddCheckAnswersButton()
        {
            btnCheckAnswers = new Button
            {
                Text = "Check Result",
                Size = new Size(120, 30),
                Location = new Point(50, 480),
                BackColor = Color.LightGreen, // Button background color
                ForeColor = Color.DarkGreen, // Button text color
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnCheckAnswers.Click += CheckAnswersButton_Click;
            this.Controls.Add(btnCheckAnswers);
        }

        // Add Restart button
        private void AddRestartButton()
        {
            btnRestart = new Button
            {
                Text = "Restart",
                Size = new Size(100, 30),
                Location = new Point(180, 480),
                BackColor = Color.Orange, // Button background color
                ForeColor = Color.White, // Button text color
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnRestart.Click += btnRestart_Click;
            this.Controls.Add(btnRestart);
        }

        // Check answers button click event
        private void CheckAnswersButton_Click(object sender, EventArgs e)
        {
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
            else
            {
                MessageBox.Show("There are still unmatched pairs!", "Check Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Restart button click event
        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
