using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Ergasia
{
    public partial class Exercise : Form
    {
        DatabaseHelper databaseHelper = new DatabaseHelper();
        Numbers numbers;
        public int leftNumber, rightNumber, resultNumber, counter, score;
        public int numOfQuestions, level, selected;
        User student;
        ExerciseClass exercises;
        Game game;
        List<Question> questions = new List<Question>();
        SoundPlayer soundPlayer = new SoundPlayer();
        public Exercise(User student, ExerciseClass exercises, int selected)
        {
            InitializeComponent();
            score = 0;
            this.selected = selected;
            this.exercises = exercises;
            this.student = student;
            questions = databaseHelper.getQuestions(selected);
            Random random = new Random();
            questions = questions.OrderBy(item => random.Next()).ToList();
            if (selected == 4)
            {
                questions = questions.GetRange(0, 20);
                numOfQuestions = questions.Count();
            }
            numOfQuestions = questions.Count();
        }

        public Exercise(User student, Game game, int level)
        {
            InitializeComponent();
            score = 0;
            for(int i=1; i<=10; i++)
            {
                for(int j=1; j<=10; j++)
                {
                    questions.Add(new Question(i, j));
                }
            }
            Random random = new Random();
            questions = questions.OrderBy(item => random.Next()).ToList();
            this.student = student;
            this.game = game;
            this.level = level;
            if (level == 1)
            {
                questions = questions.GetRange(0, 15);
                numOfQuestions = questions.Count();
            }
            else if (level == 2)
            {
                questions = questions.GetRange(0, 30);
                numOfQuestions = questions.Count();
            }
            else if (level == 3)
            {
                questions = questions.GetRange(0, 50);
                numOfQuestions = questions.Count();
            }
        }

        private void Exercise_Load(object sender, EventArgs e)
        {
            counter = 1;
            label1.Text = "Ερώτηση " + counter + " από " + numOfQuestions;
            if (level == 0)
            {
                label2.Text = "Κεφάλαιο " + selected.ToString();
                if (selected == 4)
                {
                    label2.Text = "Eπαναλητική εξέταση";
                }
            }
            else if (level == 1)
            {
                label2.Text = "Εύκολο επίπεδο";
            }
            else if (level == 2)
            {
                label2.Text = "Μεσαίο επίπεδο";
            }
            else if (level == 3)
            {
                label2.Text = "Δύσκολο επίπεδο";
            }
            createQuestion(questions[0]);
            numbers = new Numbers(Right1, Right2, Equal1, Equal2, Equal3);
        }
        private void createQuestion(Question question)
        {
            Left2.Image = null;
            Left1.Image = null;
            Right2.Image = null;
            Right1.Image = null;
            Equal3.Image = null;
            Equal2.Image = null;
            Equal1.Image = null;

            leftNumber = question.getLeftNumber();

            if (question.getLeftDigits().Count == 2)
            {
                Left1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getLeftDigits()[1]);
                Left2.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getLeftDigits()[0]);
            }
            else
            {
                Left1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getLeftDigits()[0]);
            }
            Random random = new Random();
            int black = random.Next(0, 2);
            if (black == 0)
            {
                Right1.Image = Properties.Resources.question_mark_draw;
                rightNumber = 0;
                resultNumber = question.getResult();
                if (question.getResultDigits().Count == 3)
                {
                    Equal1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[2]);
                    Equal2.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[1]);
                    Equal3.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[0]);
                }
                else if (question.getResultDigits().Count == 2)
                {
                    Equal1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[1]);
                    Equal2.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[0]);
                }
                else
                {
                    Equal1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getResultDigits()[0]);
                }

                Right1.Tag = "empty";
                Right2.Tag = "empty";

                Equal1.Tag = "given";
                Equal2.Tag = "given";
                Equal3.Tag = "given";

            }
            else
            {
                Equal1.Image = Properties.Resources.question_mark_draw;
                rightNumber = question.getRightNumber();
                resultNumber = 0;
                if (question.getRightDigits().Count == 2)
                {
                    Right1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getRightDigits()[1]);
                    Right2.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getRightDigits()[0]);
                }
                else
                {
                    Right1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + question.getRightDigits()[0]);
                }

                Equal1.Tag = "empty";
                Equal2.Tag = "empty";
                Equal3.Tag = "empty";

                Right1.Tag = "given";
                Right2.Tag = "given";
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (rightNumber == questions[0].getRightNumber() && resultNumber == questions[0].getResult())
            {
                panel1.BackColor = Color.SpringGreen;
                score++;
                soundPlayer = new SoundPlayer(Properties.Resources.correct_answer);
                soundPlayer.Play();
            } 
            else
            {
                panel1.BackColor = Color.Firebrick;
                string correct = questions[0].getLeftNumber() + " x " + questions[0].getRightNumber() + " = " + questions[0].getResult();
                string wrong = leftNumber + " x " + rightNumber + " = " + resultNumber;
                MessageBox.Show("Η σωστή απάντηση είναι: " + correct);
                if (level == 0)
                {
                    MistakeClass mistake = new MistakeClass(student, questions[0].getId(), selected, correct, wrong);
                    databaseHelper.setMistake(mistake);
                }
            }
            Wait();            
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (level == 0)
            {
                if (selected == 1)
                {
                    if (score > exercises.getExercise1())
                    {
                        exercises.setExercise1(score);
                        databaseHelper.setExercise(exercises);
                    }
                    else
                    {
                        exercises.setTries1();
                        databaseHelper.setExercise(exercises);
                    }
                }
                else if (selected == 2)
                {
                    if (score > exercises.getExercise2())
                    {
                        exercises.setExercise2(score);
                        databaseHelper.setExercise(exercises);
                    }
                    else
                    {
                        exercises.setTries2();
                        databaseHelper.setExercise(exercises);
                    }
                }
                else if (selected == 3)
                {
                    if (score > exercises.getExercise3())
                    {
                        exercises.setExercise3(score);
                        databaseHelper.setExercise(exercises);
                    }
                    else
                    {
                        exercises.setTries3();
                        databaseHelper.setExercise(exercises);
                    }
                }
                else if (selected == 4)
                {
                    if (score > exercises.getExercise4())
                    {
                        exercises.setExercise4(score);
                        databaseHelper.setExercise(exercises);
                    }
                    else
                    {
                        exercises.setTries4();
                        databaseHelper.setExercise(exercises);
                    }
                }

            }
            else
            {
                if (score > game.getEasy() && level == 1)
                {
                    game.setEasy(score);
                    databaseHelper.setGameScores(game);
                }
                if (score > game.getMedium() && level == 2)
                {
                    game.setMedium(score);
                    databaseHelper.setGameScores(game);
                }
                if (score > game.getHard() && level == 3)
                {
                    game.setHard(score);
                    databaseHelper.setGameScores(game);
                }
            }
            
            StudentDashboard studentDashboard = new StudentDashboard(student);
            this.Hide();
            studentDashboard.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            questions.Add(questions[0]);
            questions.RemoveAt(0);
            createQuestion(questions[0]);
        }

        private void clickNumber(object sender, EventArgs e)
        {
            numbers.Check(sender, this);
        }
        private void clickRemove(object sender, EventArgs e)
        {
            numbers.DeleteNumber(this);
        }
        public async void Wait()
        {
            keyboardpanel.Enabled = false;
            await Task.Delay(1000);
            panel1.BackColor = Color.MidnightBlue;
            if (questions.Count > 1)
            {
                keyboardpanel.Enabled = true;
                questions.RemoveAt(0);
                createQuestion(questions[0]);
                counter++;
                label1.Text = "Ερώτηση " + counter + " από " + numOfQuestions;
            }
            else
            {
                questions.RemoveAt(0);
                soundPlayer = new SoundPlayer(Properties.Resources.exercise_sound);
                soundPlayer.Play();
                MessageBox.Show("Ολοκλήρωσες την διαδικασία\nΣκορ: " + score.ToString() + "/" + numOfQuestions.ToString());
                keyboardpanel.Enabled = false;
                if (level == 0)
                {
                    if (selected == 1)
                    {
                        if (score > exercises.getExercise1())
                        {
                            exercises.setExercise1(score);
                            databaseHelper.setExercise(exercises);
                        }
                        else
                        {
                            exercises.setTries1();
                            databaseHelper.setExercise(exercises);
                        }
                    } 
                    else if (selected == 2)
                    {
                        if (score > exercises.getExercise2())
                        {
                            exercises.setExercise2(score);
                            databaseHelper.setExercise(exercises);
                        }
                        else
                        {
                            exercises.setTries2();
                            databaseHelper.setExercise(exercises);
                        }
                    }
                    else if (selected == 3)
                    {
                        if (score > exercises.getExercise3())
                        {
                            exercises.setExercise3(score);
                            databaseHelper.setExercise(exercises);
                        }
                        else
                        {
                            exercises.setTries3();
                            databaseHelper.setExercise(exercises);
                        }
                    }
                    else if (selected == 4)
                    {
                        if (score > exercises.getExercise4())
                        {
                            exercises.setExercise4(score);
                            databaseHelper.setExercise(exercises);
                        }
                        else
                        {
                            exercises.setTries4();
                            databaseHelper.setExercise(exercises);
                        }
                    }
                    
                }
                else
                {
                    if (score > game.getEasy() && level == 1)
                    {
                        game.setEasy(score);
                        databaseHelper.setGameScores(game);
                    }
                    if (score > game.getMedium() && level == 2)
                    {
                        game.setMedium(score);
                        databaseHelper.setGameScores(game);
                    }
                    if (score > game.getHard() && level == 3)
                    {
                        game.setHard(score);
                        databaseHelper.setGameScores(game);
                    }
                }
                StudentDashboard studentDashboard = new StudentDashboard(student);
                this.Hide();
                studentDashboard.ShowDialog();
                this.Close();
            }
        }
    }
}
