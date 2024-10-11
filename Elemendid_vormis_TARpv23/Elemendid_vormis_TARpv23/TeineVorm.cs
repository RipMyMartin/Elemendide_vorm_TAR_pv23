using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Elemendid_vormis_TARpv23
{
    public partial class TeineVorm : Form
    {
        Label timeLabel;
        Label plusLeftLabel, plusRightLabel;
        Label minusLeftLabel, minusRightLabel;
        Label timesLeftLabel, timesRightLabel;
        Label dividedLeftLabel, dividedRightLabel;
        NumericUpDown sum, difference, product, quotient;
        Button startButton;
        System.Windows.Forms.Timer quizTime;
        int timeLeft;

        public TeineVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";

            timeLabel = new Label();
            timeLabel.Text = "Time Left";
            timeLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            timeLabel.Size = new Size(200, 50);
            timeLabel.Location = new Point(50, 20);
            Controls.Add(timeLabel);

            CreateMathQuestion(out plusLeftLabel, out plusRightLabel, out sum, "+", 50, 100);
            CreateMathQuestion(out minusLeftLabel, out minusRightLabel, out difference, "-", 50, 150);
            CreateMathQuestion(out timesLeftLabel, out timesRightLabel, out product, "×", 50, 200);
            CreateMathQuestion(out dividedLeftLabel, out dividedRightLabel, out quotient, "÷", 50, 250);

            startButton = new Button();
            startButton.Text = "Start Quiz";
            startButton.Size = new Size(100, 50);
            startButton.Location = new Point(200, 400);
            startButton.Click += new EventHandler(StartButton_Click);
            Controls.Add(startButton);

            quizTime = new System.Windows.Forms.Timer();
            quizTime.Interval = 1000;
            quizTime.Tick += new EventHandler(Timer_Tick);
        }

        private void CreateMathQuestion(out Label leftLabel, out Label rightLabel, out NumericUpDown answerBox, string operation, int x, int y)
        {
            leftLabel = new Label();
            leftLabel.Text = "?";
            leftLabel.Font = new Font("Arial", 18);
            leftLabel.Location = new Point(x, y);
            leftLabel.Size = new Size(60, 50);
            Controls.Add(leftLabel);

            rightLabel = new Label();
            rightLabel.Text = "?";
            rightLabel.Font = new Font("Arial", 18);
            rightLabel.Location = new Point(x + 100, y);
            rightLabel.Size = new Size(60, 50);
            Controls.Add(rightLabel);

            Label operatorLabel = new Label();
            operatorLabel.Text = operation;
            operatorLabel.Font = new Font("Arial", 18);
            operatorLabel.Location = new Point(x + 60, y);
            operatorLabel.Size = new Size(40, 50);
            Controls.Add(operatorLabel);

            answerBox = new NumericUpDown();
            answerBox.Size = new Size(100, 50);
            answerBox.Location = new Point(x + 200, y);
            Controls.Add(answerBox);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            plusLeftLabel.Text = random.Next(1, 10).ToString();
            plusRightLabel.Text = random.Next(1, 10).ToString();

            minusLeftLabel.Text = random.Next(1, 10).ToString();
            minusRightLabel.Text = random.Next(1, 10).ToString();

            timesLeftLabel.Text = random.Next(1, 10).ToString();
            timesRightLabel.Text = random.Next(1, 10).ToString();

            dividedLeftLabel.Text = random.Next(1, 10).ToString();
            dividedRightLabel.Text = random.Next(1, 10).ToString();
            timeLeft = 30;
            quizTime.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = "Time Left: " + timeLeft + " seconds";
            }
            else
            {
                quizTime.Stop();
                MessageBox.Show("Time's up!");
            }
        }
    }
}
