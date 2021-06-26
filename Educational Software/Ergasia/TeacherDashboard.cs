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
    public partial class TeacherDashboard : Form
    {
        DatabaseHelper databaseHelper = new DatabaseHelper();
        User teacher;
        List<User> students = new List<User>();
        LessonClass lessons;
        ExerciseClass exercises;

        public TeacherDashboard(User teacher)
        {
            InitializeComponent();
            this.teacher = teacher;
        }

        private void TeacherDashboard_Load(object sender, EventArgs e)
        {
            students = databaseHelper.getAllStudents();
            setProfile();
            setStudents();
        }

        private void setStudents()
        {
            for (int i = 0; i < students.Count; i++)
            {
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = students[i].getId();
                dataGridView1.Rows[row].Cells[1].Value = students[i].getFirst_name() + " " + students[i].getLast_name();
                dataGridView1.Rows[row].Cells[2].Value = students[i].getEmail();
            }
        }
        private void setProfile()
        {
            fullname.Text = teacher.getFirst_name() + " " + teacher.getLast_name();
            email.Text = teacher.getEmail();
        }
        private void showResults()
        {
            if (lessons == null)
            {
                MessageBox.Show("Δεν βρέθηκε μαθητής με αυτό το id");
            }
            else
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
                resultPanel.Visible = true;
            }
            
        }

        private void selectMenu(Panel sidePanel, Panel mainPanel)
        {
            studentsSide.BackColor = Color.MidnightBlue;
            studentsPanel.Visible = false;
            progressSide.BackColor = Color.MidnightBlue;
            progressPanel.Visible = false;
            profileSide.BackColor = Color.MidnightBlue;
            profilePanel.Visible = false;
            helpSide.BackColor = Color.MidnightBlue;
            exitSide.BackColor = Color.MidnightBlue;
            sidePanel.BackColor = Color.FromArgb(64, 64, 114);
            mainPanel.Visible = true;
        }
        private void studentsSide_Click(object sender, EventArgs e)
        {
            selectMenu(studentsSide, studentsPanel);
        }
        private void progressSide_Click(object sender, EventArgs e)
        {
            selectMenu(progressSide, progressPanel);
            resultPanel.Visible = false;
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
                    databaseHelper.updatePassword(teacher, textBox2.Text);
                    textBox2.UseSystemPasswordChar = false;
                    textBox2.Text = "Εισάγετε νέο κωδικό εδώ";
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Εισάγετε το id του μαθητή")
            {
                textBox1.Text = "";
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Εισάγετε το id του μαθητή";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "Εισάγετε το id του μαθητή" && textBox1.Text != "")
            {
                textBox2.Text = "Εισάγετε το id του μαθητή";
                if (databaseHelper.checkIfStudentExists(Convert.ToInt32(textBox1.Text)))
                {
                    User student = databaseHelper.getStudent(Convert.ToInt32(textBox1.Text));
                    exercises = databaseHelper.getExercises(student);
                    lessons = databaseHelper.getLessons(student);
                    showResults();
                }
                else
                {
                    MessageBox.Show("Δεν υπάρχει μαθητής με αυτό το id");
                }
            }
            else
            {
                MessageBox.Show("Εισάγετε πρώτα το id του μαθητή");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User student = databaseHelper.getStudent(Convert.ToInt32(textBox1.Text));
            Mistakes mistakes = new Mistakes(student, true);
            this.Hide();
            mistakes.ShowDialog();
            this.Close();
        }
    }
}
