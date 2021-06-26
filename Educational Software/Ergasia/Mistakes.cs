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
    public partial class Mistakes : Form
    {
        DatabaseHelper databaseHelper = new DatabaseHelper();
        User user;
        bool fromTeacher;
        List<MistakeClass> mistakes1 = new List<MistakeClass>();
        List<MistakeClass> mistakes2 = new List<MistakeClass>();
        List<MistakeClass> mistakes3 = new List<MistakeClass>();

        public Mistakes(User user, bool fromTeacher)
        {
            InitializeComponent();
            this.user = user;
            this.fromTeacher = fromTeacher;
            mistakes1 = databaseHelper.getMistakes(user, 1);
            mistakes2 = databaseHelper.getMistakes(user, 2);
            mistakes3 = databaseHelper.getMistakes(user, 3);
        }

        private void Mistakes_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < mistakes1.Count; i++)
            {
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = mistakes1[i].getWrong();
                dataGridView1.Rows[row].Cells[1].Value = mistakes1[i].getCorrect();
            }
            count1.Text = "Συνολικά λάθη: " + mistakes1.Count.ToString();
            for (int i = 0; i < mistakes2.Count; i++)
            {
                int row = dataGridView2.Rows.Add();
                dataGridView2.Rows[row].Cells[0].Value = mistakes2[i].getWrong();
                dataGridView2.Rows[row].Cells[1].Value = mistakes2[i].getCorrect();
            }
            count2.Text = "Συνολικά λάθη: " + mistakes2.Count.ToString();
            for (int i = 0; i < mistakes3.Count; i++)
            {
                int row = dataGridView3.Rows.Add();
                dataGridView3.Rows[row].Cells[0].Value = mistakes3[i].getWrong();
                dataGridView3.Rows[row].Cells[1].Value = mistakes3[i].getCorrect();
            }
            count3.Text = "Συνολικά λάθη: " + mistakes3.Count.ToString();
            int[] array = { mistakes1.Count, mistakes2.Count, mistakes3.Count };
            if (array.Max() == 0)
            {
                label6.Text = "Δεν έχει γίνει ακόμα κάποια λάθος απάντηση";
            } 
            else
            {
                label6.Text = "Τα περισσότερα λάθη ήταν στο Κεφάλαιο " + (array.ToList().IndexOf(array.Max()) + 1).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fromTeacher)
            {
                TeacherDashboard teacherDashboard = new TeacherDashboard(user);
                this.Hide();
                teacherDashboard.ShowDialog();
                this.Close();
            }
            else
            {
                StudentDashboard studentDashboard = new StudentDashboard(user);
                this.Hide();
                studentDashboard.ShowDialog();
                this.Close();
            }
        }
    }
}
