using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        Button startButton, checkButton, saveButton, stopButton;
        System.Windows.Forms.Timer quizTime;
        int timeLeft;
        bool isQuizRunning = false;

        public TeineVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";
            this.BackColor = Color.LightSkyBlue;

            timeLabel = new Label();
            timeLabel.Text = "Time Left";
            timeLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            timeLabel.ForeColor = Color.White;
            timeLabel.Size = new Size(200, 50);
            timeLabel.Location = new Point(50, 20);
            Controls.Add(timeLabel);

            CreateMathQuestion(out plusLeftLabel, out plusRightLabel, out sum, "+", 50, 100);
            CreateMathQuestion(out minusLeftLabel, out minusRightLabel, out difference, "-", 50, 150);
            CreateMathQuestion(out timesLeftLabel, out timesRightLabel, out product, "×", 50, 200);
            CreateMathQuestion(out dividedLeftLabel, out dividedRightLabel, out quotient, "÷", 50, 250);

            startButton = CreateButton("Start Quiz", 180, 400, StartButton_Click);
            checkButton = CreateButton("Check Results", 290, 400, CheckButton_Click);
            saveButton = CreateButton("Save Answers", 400, 400, SaveButton_Click);
            stopButton = CreateButton("Stop", 510, 400, StopButton_Click);

            quizTime = new System.Windows.Forms.Timer();
            quizTime.Interval = 1000;
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


        private Button CreateButton(string text, int x, int y, EventHandler clickEvent)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(100, 50);
            button.Location = new Point(x, y);
            button.BackColor = Color.LightCoral;
            button.ForeColor = Color.White;
            button.Font = new Font("Arial", 10, FontStyle.Bold);
            button.Click += clickEvent;
            Controls.Add(button);
            return button;
        }

        private void CreateMathQuestion(out Label leftLabel, out Label rightLabel, out NumericUpDown answerBox, string operation, int x, int y)
        {
            leftLabel = CreateLabel("?", x, y);
            rightLabel = CreateLabel("?", x + 100, y);
            Label operatorLabel = CreateLabel(operation, x + 60, y);

            answerBox = new NumericUpDown();
            answerBox.Size = new Size(100, 50);
            answerBox.Location = new Point(x + 200, y);
            answerBox.BackColor = Color.LightYellow;
            Controls.Add(answerBox);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Font = new Font("Arial", 18);
            label.Location = new Point(x, y);
            label.Size = new Size(60, 50);
            label.ForeColor = Color.DarkSlateGray;
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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Math Quiz Answers:");
            sb.AppendLine($"Sum: {sum.Value} (Correct: {int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text)})");
            sb.AppendLine($"Difference: {difference.Value} (Correct: {int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text)})");
            sb.AppendLine($"Product: {product.Value} (Correct: {int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text)})");

            int rightOperand = int.Parse(dividedRightLabel.Text);
            sb.AppendLine($"Quotient: {quotient.Value} (Correct: {(rightOperand != 0 ? int.Parse(dividedLeftLabel.Text) / rightOperand : "N/A")})");

            string filePath = @"..\..\..\QuizAnswers.txt";
            File.WriteAllText(filePath, sb.ToString());

            MessageBox.Show("Answers saved", "Save Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
