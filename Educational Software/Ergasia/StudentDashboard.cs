using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ergasia
{
    public partial class StudentDashboard : Form
    {
        DatabaseHelper databaseHelper = new DatabaseHelper();
        User student;
        LessonClass lessons;
        ExerciseClass exercises;
        Game game;
        public StudentDashboard(User student)
        {
            InitializeComponent();
            this.student = student;
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            lessons = databaseHelper.getLessons(student);
            exercises = databaseHelper.getExercises(student);
            game = databaseHelper.getGameScores(student);
            checkLessons();
            checkExercises();
            setProfile();
            setGame();
        }

        private void checkLessons()
        {
            lessonUnlocked(lesson1Title, lesson1Image, lesson1Play, lesson1Message, lessons.getRead1());
            if (exercises.getExercise1() == 10)
            {
                lessonUnlocked(lesson2Title, lesson2Image, lesson2Play, lesson2Message, lessons.getRead2());
            }
            else
            {
                lessonLocked(lesson2Title, lesson2Image, lesson2Play, lesson2Message);
            }
            if (exercises.getExercise2() == 10)
            {
                lessonUnlocked(lesson3Title, lesson3Image, lesson3Play, lesson3Message, lessons.getRead3());
            }
            else
            {
                lessonLocked(lesson3Title, lesson3Image, lesson3Play, lesson3Message);
            }
        }
        private void checkExercises()
        {
            if (lessons.getRead1() >= 1)
            {
                exerciseUnlocked(exercise1Title, exercise1Image, exercise1Score, exercise1Message, exercise1Play, exercises.getExercise1(), exercises.getTries1());
            }
            else
            {
                exerciseLocked(exercise1Title, exercise1Image, exercise1Score, exercise1Message, exercise1Play);
            }
            if (lessons.getRead2() >= 1)
            {
                exerciseUnlocked(exercise2Title, exercise2Image, exercise2Score, exercise2Message, exercise2Play, exercises.getExercise2(), exercises.getTries2());
            }
            else
            {
                exerciseLocked(exercise2Title, exercise2Image, exercise2Score, exercise2Message, exercise2Play);
            }
            if (lessons.getRead3() >= 1)
            {
                exerciseUnlocked(exercise3Title, exercise3Image, exercise3Score, exercise3Message, exercise3Play, exercises.getExercise3(), exercises.getTries3());
            }
            else
            {
                exerciseLocked(exercise3Title, exercise3Image, exercise3Score, exercise3Message, exercise3Play);
            }
            if (exercises.getExercise1() == 10 && exercises.getExercise2() == 10 && exercises.getExercise3() == 10)
            {
                exerciseUnlocked(exercise4Title, exercise4Image, exercise4Score, exercise4Message, exercise4Play, exercises.getExercise4(), exercises.getTries4());
            }
            else
            {
                exerciseLocked(exercise4Title, exercise4Image, exercise4Score, exercise4Message, exercise4Play);
            }
        }
        private void setGame()
        {
            easy.Text = "Εύκολο επίπεδο: " + game.getEasy() + "/15";
            medium.Text = "Μεσαίο επίπεδο: " + game.getMedium() + "/30";
            hard.Text = "Δύσκολο επίπεδο: " + game.getHard() + "/50";
        }
        private void setProfile()
        {
            if (lessons.getRead1() == 1)
            {
                read1.Text = "Διαβάστηκε 1 φορά";
            }
            else
            {
                read1.Text = "Διαβάστηκε " + lessons.getRead1() + " φορές";
            }
            if (lessons.getRead2() == 1)
            {
                read2.Text = "Διαβάστηκε 1 φορά";
            }
            else
            {
                read2.Text = "Διαβάστηκε " + lessons.getRead2() + " φορές";
            }
            if (lessons.getRead3() == 1)
            {
                read3.Text = "Διαβάστηκε 1 φορά";
            }
            else
            {
                read3.Text = "Διαβάστηκε " + lessons.getRead3() + " φορές";
            }
            score1.Text = "Καλύτερο σκορ: " + exercises.getExercise1() + "/10";
            score2.Text = "Καλύτερο σκορ: " + exercises.getExercise2() + "/10";
            score3.Text = "Καλύτερο σκορ: " + exercises.getExercise3() + "/10";
            score4.Text = "Καλύτερο σκορ: " + exercises.getExercise4() + "/20";
            tries1.Text = "Προσπάθειες: " + exercises.getTries1();
            tries2.Text = "Προσπάθειες: " + exercises.getTries2();
            tries3.Text = "Προσπάθειες: " + exercises.getTries3();
            tries4.Text = "Προσπάθειες: " + exercises.getTries4();
            fullname.Text = student.getFirst_name() + " " + student.getLast_name();
            email.Text = student.getEmail();
        }

        private void lessonLocked(Label lessonTitle, PictureBox lessonImage, PictureBox lessonPlay, Label lessonMessage)
        {
            lessonTitle.Cursor = Cursors.No;    
            lessonImage.Visible = true;
            lessonPlay.Visible = false;
            lessonImage.Image = Properties.Resources.padlock;
            lessonMessage.Text = "Λύσε σωστά όλες τις ασκήσεις\nαπό το προηγούμενο κεφάλαιο.";
        }
        private void lessonUnlocked(Label lessonTitle, PictureBox lessonImage, PictureBox lessonPlay, Label lessonMessage, int lessonRead)
        {
            lessonTitle.Click += new EventHandler(openLesson);
            lessonTitle.Cursor = Cursors.Hand;
            lessonTitle.MouseEnter += new EventHandler(enterLesson);
            lessonPlay.MouseEnter += new EventHandler(enterLesson);
            lessonTitle.MouseLeave += new EventHandler(leaveLesson);
            lessonPlay.MouseLeave += new EventHandler(leaveLesson);
            lessonTitle.Cursor = Cursors.Hand;
            lessonPlay.Cursor = Cursors.Hand;
            if (lessonRead == 1)
            {
                lessonImage.Image = Properties.Resources.check;
                lessonMessage.Text = "Διαβάστηκε 1 φορά";
            }
            else 
            {
                lessonMessage.Text = "Διαβάστηκε " + lessonRead + " φορές";
                if (lessonRead == 0)
                {
                    lessonImage.Image = null;
                }
                else
                {
                    lessonImage.Image = Properties.Resources.check;
                }
            }
        }
        private void openLesson(object sender, EventArgs e)
        {
            int selected;
            if (sender == lesson1Play || sender == lesson1Title)
            {
                lessons.setRead1(lessons.getRead1() + 1);
                databaseHelper.setLessonRead(lessons);
                selected = 1;
            }
            else if (sender == lesson2Play || sender == lesson2Title)
            {
                lessons.setRead2(lessons.getRead2() + 1);
                databaseHelper.setLessonRead(lessons);
                selected = 2;
            }
            else
            {
                lessons.setRead3(lessons.getRead3() + 1);
                databaseHelper.setLessonRead(lessons);
                selected = 3;
            }
            Lesson lesson = new Lesson(student, selected);
            this.Hide();
            lesson.ShowDialog();
            this.Close();
        }

        private void exerciseLocked(Label exerciseTitle, PictureBox exerciseImage, Label exerciseScore, Label exerciseMessage, PictureBox exercisePlay)
        {
            exerciseTitle.Cursor = Cursors.No;
            exerciseImage.Visible = true;
            exerciseImage.Image = Properties.Resources.padlock;
            exercisePlay.Visible = false;
            exerciseScore.Visible = false;
            if (exerciseMessage == exercise4Message)
            {
                exerciseMessage.Text = "Ολοκλήρωσε με επιτυχία όλες τις\nπροηγούμενες εξετάσεις";
            }
            else
            {
                exerciseMessage.Text = "Διάβασε πρώτα το αντίστοιχο κεφάλαιο";
            }
        }
        private void exerciseUnlocked(Label exerciseTitle, PictureBox exerciseImage, Label exerciseScore, Label exerciseMessage, PictureBox exercisePlay, int score, int tries)
        {
            exerciseTitle.Click += new EventHandler(openExercise);
            exerciseTitle.MouseEnter += new EventHandler(enterExercise);
            exercisePlay.MouseEnter += new EventHandler(enterExercise);
            exerciseTitle.MouseLeave += new EventHandler(leaveExercise);
            exercisePlay.MouseLeave += new EventHandler(leaveExercise);
            exerciseTitle.Cursor = Cursors.Hand;
            exercisePlay.Cursor = Cursors.Hand;
            exercisePlay.Visible = true;
            if (tries == 0)
            {
                exerciseScore.Visible = false;
                exerciseImage.Image = null;
                exerciseMessage.Text = "Έγινε " + tries + " φορές";
            }
            else
            {
                if (tries == 1)
                {
                    exerciseMessage.Text = "Έγινε 1 φορά";

                }
                else
                {
                    exerciseMessage.Text = "Έγινε " + tries + " φορές";
                }
                exerciseScore.Visible = true;
                exerciseScore.Text = "Καλύτερη βαθμολογία: " + score + "/10";
                if (exerciseTitle == exercise4Title)
                {
                    exerciseScore.Text = "Καλύτερη βαθμολογία: " + score + "/20";
                }
                if (score >= 0 && score <= 5)
                {
                    exerciseImage.Image = Properties.Resources.confused;
                }
                else if (score > 5 && score <= 7)
                {
                    exerciseImage.Image = Properties.Resources.smile;
                }
                else if (score > 7 && score <= 9)
                {
                    exerciseImage.Image = Properties.Resources.happy;
                }
                else if (score == 10)
                {
                    exerciseImage.Image = Properties.Resources.very_happy;
                }
            }
        }
        private void openExercise(object sender, EventArgs e)
        {
            int selected;
            if (sender == exercise1Play || sender == exercise1Title)
            {
                selected = 1;
            }
            else if (sender == exercise2Play || sender == exercise2Title)
            {
                selected = 2;
            }
            else if (sender == exercise3Play || sender == exercise3Title)
            {
                selected = 3;
            }
            else
            {
                selected = 4;
            }
            Exercise lesson = new Exercise(student, exercises, selected);
            this.Hide();
            lesson.ShowDialog();
            this.Close();
        }


        private void enterLesson(object sender, EventArgs e)
        {
           if (sender == lesson1Play || sender == lesson1Title)
           {
                lesson1Title.ForeColor = Color.RoyalBlue;
                lesson1Play.Image = Properties.Resources.play_selected;
           }
           else if (sender == lesson2Play || sender == lesson2Title)
           {
                lesson2Title.ForeColor = Color.RoyalBlue;
                lesson2Play.Image = Properties.Resources.play_selected;
           }
           else if (sender == lesson3Play || sender == lesson3Title)
           {
                lesson3Title.ForeColor = Color.RoyalBlue;
                lesson3Play.Image = Properties.Resources.play_selected;
           }
        }
        private void leaveLesson(object sender, EventArgs e)
        {
            if (sender == lesson1Play || sender == lesson1Title)
            {
                lesson1Title.ForeColor = Color.MidnightBlue;
                lesson1Play.Image = Properties.Resources.play;
            }
            else if (sender == lesson2Play || sender == lesson2Title)
            {
                lesson2Title.ForeColor = Color.MidnightBlue;
                lesson2Play.Image = Properties.Resources.play;
            }
            else if (sender == lesson3Play || sender == lesson3Title)
            {
                lesson3Title.ForeColor = Color.MidnightBlue;
                lesson3Play.Image = Properties.Resources.play;
            }
        }
        private void enterExercise(object sender, EventArgs e)
        {
            if (sender == exercise1Play || sender == exercise1Title)
            {
                exercise1Title.ForeColor = Color.RoyalBlue;
                exercise1Play.Image = Properties.Resources.play_selected;
            }
            else if (sender == exercise2Play || sender == exercise2Title)
            {
                exercise2Title.ForeColor = Color.RoyalBlue;
                exercise2Play.Image = Properties.Resources.play_selected;
            }
            else if (sender == exercise3Play || sender == exercise3Title)
            {
                exercise3Title.ForeColor = Color.RoyalBlue;
                exercise3Play.Image = Properties.Resources.play_selected;
            }
            else if (sender == exercise4Play || sender == exercise4Title)
            {
                exercise4Title.ForeColor = Color.RoyalBlue;
                exercise4Play.Image = Properties.Resources.play_selected;
            }

        }
        private void leaveExercise(object sender, EventArgs e)
        {
            if (sender == exercise1Play || sender == exercise1Title)
            {
                exercise1Title.ForeColor = Color.MidnightBlue;
                exercise1Play.Image = Properties.Resources.play;
            }
            else if (sender == exercise2Play || sender == exercise2Title)
            {
                exercise2Title.ForeColor = Color.MidnightBlue;
                exercise2Play.Image = Properties.Resources.play;
            }
            else if (sender == exercise3Play || sender == exercise3Title)
            {
                exercise3Title.ForeColor = Color.MidnightBlue;
                exercise3Play.Image = Properties.Resources.play;
            }
            else if (sender == exercise4Play || sender == exercise4Title)
            {
                exercise4Title.ForeColor = Color.MidnightBlue;
                exercise4Play.Image = Properties.Resources.play;
            }
        }



        // Menu Handling
        private void selectMenu(Panel sidePanel, Panel mainPanel)
        {
            lessonsSide.BackColor = Color.MidnightBlue;
            lessonsPanel.Visible = false;
            exercisesSide.BackColor = Color.MidnightBlue;
            exercisesPanel.Visible = false;
            gameSide.BackColor = Color.MidnightBlue;
            gamePanel.Visible = false;
            profileSide.BackColor = Color.MidnightBlue;
            profilePanel.Visible = false;
            helpSide.BackColor = Color.MidnightBlue;
            exitSide.BackColor = Color.MidnightBlue;
            sidePanel.BackColor = Color.FromArgb(64, 64, 114);
            mainPanel.Visible = true;
        }
        private void lessonsSide_Click(object sender, EventArgs e)
        {
            selectMenu(lessonsSide, lessonsPanel);
        }
        private void exercisesSide_Click(object sender, EventArgs e)
        {
            selectMenu(exercisesSide, exercisesPanel);
        }
        private void gameSide_Click(object sender, EventArgs e)
        {
            selectMenu(gameSide, gamePanel);
        }
        private void profileSide_Click(object sender, EventArgs e)
        {
            selectMenu(profileSide, profilePanel);
        }
        private void helpSide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + "/Online Help/login.html");
        }
        private void exitSide_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "Εισάγετε νέο κωδικό εδώ" && textBox2.Text != "")
            {
                if (textBox2.Text.Length < 6)
                {
                    MessageBox.Show("Ο κωδικός πρέπει να έχει τουλάχιστον 6 χαρακτήρες");
                }
                else
                {
                    databaseHelper.updatePassword(student, textBox2.Text);
                    textBox2.Text = "Εισάγετε νέο κωδικό εδώ";
                    textBox2.UseSystemPasswordChar = false;
                    MessageBox.Show("Επιτυχής αλλαγή");
                }
            }
            else
            {
                MessageBox.Show("Εισάγετε πρώτα τον νέο σας κωδικό");
            }
            
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Εισάγετε νέο κωδικό εδώ")
            {
                textBox2.UseSystemPasswordChar = true;
                textBox2.Text = "";
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Εισάγετε νέο κωδικό εδώ";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int level = 0;
            if (radioButton1.Checked)
            {
                level = 1;
            } 
            else if (radioButton2.Checked)
            {
                level = 2;
            } 
            else if (radioButton3.Checked)
            {
                level = 3;
            }
            Exercise exercise = new Exercise(student, game, level);
            this.Hide();
            exercise.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mistakes mistakes = new Mistakes(student, false);
            this.Hide();
            mistakes.ShowDialog();
            this.Close();
        }
    }
}
