using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class TeineVorm : Form
    {
        private Label timeLabel;
        private Label plusLeftLabel, plusRightLabel;
        private Label minusLeftLabel, minusRightLabel;
        private Label timesLeftLabel, timesRightLabel;
        Label dividedLeftLabel, dividedRightLabel;
        private NumericUpDown sum, difference, product, quotient;
        private Button startButton, checkButton, saveButton, stopButton, randomQuestionButton;
        private System.Windows.Forms.Timer quizTime;
        private int timeLeft;
        private bool isQuizRunning = false;

        public TeineVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";
            this.BackColor = Color.LightSkyBlue;

            timeLabel = new Label
            {
                Text = "Time Left",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(200, 50),
                Location = new Point(50, 20)
            };
            Controls.Add(timeLabel);

            CreateMathQuestion(out plusLeftLabel, out plusRightLabel, out sum, "+", 50, 100);
            CreateMathQuestion(out minusLeftLabel, out minusRightLabel, out difference, "-", 50, 150);
            CreateMathQuestion(out timesLeftLabel, out timesRightLabel, out product, "×", 50, 200);
            CreateMathQuestion(out dividedLeftLabel, out dividedRightLabel, out quotient, "÷", 50, 250);

            startButton = CreateButton("Start Quiz", 60, 400, StartButton_Click);
            checkButton = CreateButton("Check Results", 170, 400, CheckButton_Click);
            saveButton = CreateButton("Save Answers", 280, 400, SaveButton_Click);
            stopButton = CreateButton("Stop", 390, 400, StopButton_Click);
            randomQuestionButton = CreateButton("Show Random Question", 500, 400, ShowRandomQuestion_Click);

            quizTime = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            quizTime.Tick += new EventHandler(Timer_Tick);
        }
        private void StopButton_Click(object? sender, EventArgs e)
        {
            if (isQuizRunning)
            {
                quizTime.Stop();
                isQuizRunning = false;
                stopButton.Text = "Resume";
            }
            else
            {
                quizTime.Start();
                isQuizRunning = true;
                stopButton.Text = "Stop";
            }
        }

        private void ShowRandomQuestion_Click(object sender, EventArgs e)
        {
            Random random = new ();
            int randomAnswer = random.Next(1, 5);

            int answer = 0;

            switch (randomAnswer)
            {
                case 1:
                    answer = int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text);
                    break;
                case 2:
                    answer = int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text);
                    break;
                case 3:
                    answer = int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text);
                    break;
                case 4:
                    answer = int.Parse(dividedLeftLabel.Text) / int.Parse(dividedRightLabel.Text);
                    break;
                default:
                    answer = 0;
                    break;
            }
            MessageBox.Show($"Random Answer: {answer}", "Random Answer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Button CreateButton(string text, int x, int y, EventHandler clickEvent)
        {
            Button button = new()
            {
                Text = text,
                Size = new Size(100, 50),
                Location = new Point(x, y),
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            button.Click += clickEvent;
            Controls.Add(button);
            return button;
        }

        private void CreateMathQuestion(out Label leftLabel, out Label rightLabel, out NumericUpDown answerBox, string operation, int x, int y)
        {
            leftLabel = CreateLabel("?", x, y);
            rightLabel = CreateLabel("?", x + 100, y);
            Label operatorLabel = CreateLabel(operation, x + 60, y);

            answerBox = new NumericUpDown
            {
                Size = new Size(100, 50),
                Location = new Point(x + 200, y),
                BackColor = Color.LightYellow
            };
            Controls.Add(answerBox);
        }
        
        private Label CreateLabel(string text, int x, int y)
        {
            Label label = new()
            {
                Text = text,
                Font = new Font("Arial", 18),
                Location = new Point(x, y),
                Size = new Size(60, 50),
                ForeColor = Color.DarkSlateGray
            };
            Controls.Add(label);
            return label;
        }

        private void CheckButton_Click(object? sender, EventArgs e)
        {
            quizTime.Stop();
            timeLeft = 30;
            timeLabel.Text = "Time Left: " + timeLeft + " seconds";

            int correctAnswers = 0;

            if (sum.Value == (int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text)))
                correctAnswers++;

            if (difference.Value == (int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text)))
                correctAnswers++;

            if (product.Value == (int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text)))
                correctAnswers++;

            if (quotient.Value == (int.Parse(dividedLeftLabel.Text) / int.Parse(dividedRightLabel.Text)))
                correctAnswers++;

            MessageBox.Show($"You got {correctAnswers} correct out of 4!", "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Random random = new();
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            StringBuilder answers = new();
            answers.AppendLine("Answers to the math test:");

            int correctSum = int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text);
            answers.AppendLine("Sum: " + sum.Value + ": Correct answer: " + correctSum + "");

            int correctDifference = int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text);
            answers.AppendLine("Difference: " + difference.Value + ": Correct answer: " + correctDifference + "");

            int correctProduct = int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text);
            answers.AppendLine("Product: " + product.Value + ": Correct answer: " + correctProduct + "");

            int correctQuotient = int.Parse(dividedRightLabel.Text) != 0 ? int.Parse(dividedLeftLabel.Text) / int.Parse(dividedRightLabel.Text) : 0;
            answers.AppendLine("Quotient: " + quotient.Value + ": Correct answer: " + (int.Parse(dividedRightLabel.Text) != 0 ? correctQuotient.ToString() : "N/A") + "");

            File.WriteAllText(@"..\..\..\QuizAnswers.txt", answers.ToString());

            MessageBox.Show("Answers saved", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Time's up!", "Quiz Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isQuizRunning = false;
                stopButton.Text = "Stop";
            }
        }
    }
}
