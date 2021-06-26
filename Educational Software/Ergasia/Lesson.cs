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
    public partial class Lesson : Form
    {
        List<Image> tables = new List<Image>();
        List<Image> numbers = new List<Image>();
        List<string> tips = new List<string>();
        int currentIndex = 0;
        User student;
        public Lesson(User student, int lesson)
        {
            this.student = student;
            InitializeComponent();
            if (lesson == 1)
            {
                tables.Add(Properties.Resources.propaidia1);
                tables.Add(Properties.Resources.propaidia2);
                tables.Add(Properties.Resources.propaidia3);
                numbers.Add(Properties.Resources._1);
                numbers.Add(Properties.Resources._2);
                numbers.Add(Properties.Resources._3);
                tips.Add("Όταν πολλαπλασιάζομαι\nείναι σαν να μην\nπολλαπλασιάστηκα ποτέ!");
                tips.Add("Μπορείς να πεις ότι απλά\nπροσθέτω στον άλλο\nαριθμό τον εαυτό του!");
                tips.Add("Αν προσθέσεις τα ψηφία\nκάθε αποτελέσματος μου\nβγάζουν πάντα 3, 6, ή 9!");
            } 
            else if (lesson == 2)
            {
                tables.Add(Properties.Resources.propaidia4);
                tables.Add(Properties.Resources.propaidia5);
                tables.Add(Properties.Resources.propaidia6);
                numbers.Add(Properties.Resources._4);
                numbers.Add(Properties.Resources._5);
                numbers.Add(Properties.Resources._6);
                tips.Add("Διπλασιάζω τα\nαποτελέσματα του 2!");
                tips.Add("Όλα τα αποτελέσματά\nμου λήγουν είτε\nσε 5 είτε σε 0!");
                tips.Add("Προσοχή! Μην με\nμπερδέψεις με το 9!");
            } 
            else if (lesson == 3)
            {
                tables.Add(Properties.Resources.propaidia7);
                tables.Add(Properties.Resources.propaidia8);
                tables.Add(Properties.Resources.propaidia9);
                tables.Add(Properties.Resources.propaidia10);
                numbers.Add(Properties.Resources._7);
                numbers.Add(Properties.Resources._8);
                numbers.Add(Properties.Resources._9);
                numbers.Add(Properties.Resources._10);
                tips.Add("Το δεύτερο ψηφίο των\nαποτελεσμάτων μου είναι\nπάντα διαφορετικό!");
                tips.Add("Εάν αφαιρέσεις 2 από το\nψηφίο των μονάδων\nγια βοήθεια!");
                tips.Add("Άμα προσθέσεις τα ψηφία\nτων αποτελεσμάτων μου\nβγάζουν πάντα 9!");
                tips.Add("Πρόσθεσε ένα 0 στο τέλος\nτου αριθμού με τον οποίο\nπολλαπλασιάζομαι!");
            }
            label2.Text = "Κεφάλαιο " + lesson.ToString();
            label3.Text = "Σελίδα 1 από " + tables.Count.ToString(); 
        }

        private void Lesson_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = tables[currentIndex];
            pictureBox2.Image = numbers[currentIndex];
            label1.Text = tips[currentIndex];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentIndex++;
            if (currentIndex == tables.Count)
            {
                StudentDashboard studentDashboard = new StudentDashboard(student);
                this.Hide();
                studentDashboard.ShowDialog();
                this.Close();
            }
            else
            {
                pictureBox1.Image = tables[currentIndex];
                pictureBox2.Image = numbers[currentIndex];
                label1.Text = tips[currentIndex];
                label3.Text = "Σελίδα " + (currentIndex + 1).ToString() + " από " + tables.Count.ToString();
                if (currentIndex == tables.Count - 1)
                {
                    button1.Text = "ΤΕΛΟΣ";
                }
            }
            if (currentIndex == 1)
            {
                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentIndex--;
            if (currentIndex == tables.Count - 2)
            {
                button1.Text = "ΕΠΟΜΕΝΟ";
            }
            if (currentIndex == 0)
            {
                button2.Visible = false;
            } 
            pictureBox1.Image = tables[currentIndex];
            pictureBox2.Image = numbers[currentIndex];
            label1.Text = tips[currentIndex];
            label3.Text = "Σελίδα " + (currentIndex + 1).ToString() + " από " + tables.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StudentDashboard studentDashboard = new StudentDashboard(student);
            this.Hide();
            studentDashboard.ShowDialog();
            this.Close();
        }
    }
}
