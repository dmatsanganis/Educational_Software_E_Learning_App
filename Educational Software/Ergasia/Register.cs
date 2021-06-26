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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        DatabaseHelper database = new DatabaseHelper();
        
        // Register Handling
        private void button1_Click(object sender, EventArgs e)
        {
            // If all fields are filled
            if (textBox1.Text!="Εισάγετε το όνομα σας εδώ" && textBox2.Text != "Εισάγετε το επώνυμο σας εδώ" && textBox3.Text != "Εισάγετε το email σας εδώ" && textBox1.Text != "Εισάγετε τον κωδικό σας εδώ")
            {
                // If email and password are valid
                if (emailValidation() && passwordValidation())
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

                    if (database.checkEmailInUse(textBox3.Text))
                    {
                        MessageBox.Show("Το email χρησιμοποιείται ήδη");
                    } 
                    else
                    {
                        if (role == "Student")
                        {
                            
                            User user = database.register(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, role);
                            StudentDashboard studentDashboard = new StudentDashboard(user);
                            this.Hide();
                            studentDashboard.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            User user = database.register(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, role);
                            TeacherDashboard teacherDashboard = new TeacherDashboard(user);
                            this.Hide();
                            teacherDashboard.ShowDialog();
                            this.Close();
                        }
                        
                    }
                } 
            }
            else
            {
                MessageBox.Show("Συμπληρώστε όλα τα πεδία");
            }
        }
        // Return to Login Form
        private void label5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
        // Retrieve only the letters typed in first name and last name textboxes
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        // Method that checks the email format
        private bool emailValidation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, pattern))
            {
                MessageBox.Show("Λάθος μορφή email");
                textBox3.Text = "Εισάγετε το email σας εδώ";
                return false;
            }
            else
            {
                return true;
            }
        }
        // Method that checks the password format
        private bool passwordValidation()
        {
            if(textBox4.TextLength < 6)
            {
                MessageBox.Show("Ο κωδικός πρέπει να έχει τουλάχιστον 6 χαρακτήρες");
                return false;
            }
            else
            {
                return true;
            }
        }

        // Placeholders Handling
        private void Register_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Εισάγετε το όνομα σας εδώ";
            textBox2.Text = "Εισάγετε το επώνυμο σας εδώ";
            textBox3.Text = "Εισάγετε το email σας εδώ";
            textBox4.UseSystemPasswordChar = false;
            textBox4.Text = "Εισάγετε τον κωδικό σας εδώ";
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Εισάγετε το όνομα σας εδώ")
            {
                textBox1.Text = "";
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Εισάγετε το όνομα σας εδώ";
            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Εισάγετε το επώνυμο σας εδώ")
            {
                textBox2.Text = "";
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Εισάγετε το επώνυμο σας εδώ";
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Εισάγετε το email σας εδώ")
            {
                textBox3.Text = "";
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Εισάγετε το email σας εδώ";
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Εισάγετε τον κωδικό σας εδώ")
            {
                textBox4.UseSystemPasswordChar = true;
                textBox4.Text = "";
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.UseSystemPasswordChar = false;
                textBox4.Text = "Εισάγετε τον κωδικό σας εδώ";
            }
        }
    }
}