using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ergasia
{
    public partial class Login : Form
    {
        DatabaseHelper database = new DatabaseHelper();
        public Login()
        {
            InitializeComponent();
        }
        // Login Handling
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "Εισάγετε το email σας εδώ" && textBox2.Text != "Εισάγετε τον κωδικό σας εδώ")  
            {
                string role;
                if (radioButton1.Checked)
                {
                    role = "Student";
                }
                else
                {
                    role = "Teacher";
                }
                User user = database.login(textBox1.Text, textBox2.Text, role);
                if (user == null)
                {
                    MessageBox.Show("Δεν υπάρχει χρήστης με αυτά τα στοιχεία");
                }
                else
                {
                    if (user.getRole() == "Student")
                    {
                        StudentDashboard studentDashboard = new StudentDashboard(user);
                        this.Hide();
                        studentDashboard.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        TeacherDashboard teacherDashboarad = new TeacherDashboard(user);
                        this.Hide();
                        teacherDashboarad.ShowDialog();
                        this.Close();
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Συμπηρώστε όλα τα πεδία");
            }
        }
        // Go to Register Form
        private void label5_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Close();
        }
        // Placeholder Handling
        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Εισάγετε το email σας εδώ";
            textBox2.UseSystemPasswordChar = false;
            textBox2.Text = "Εισάγετε τον κωδικό σας εδώ";
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Εισάγετε το email σας εδώ")
            {
                textBox1.Text = "";
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Εισάγετε το email σας εδώ";
            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Εισάγετε τον κωδικό σας εδώ")
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
                textBox2.Text = "Εισάγετε τον κωδικό σας εδώ";
            }
        }
    }
}
