    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Ergasia
{
    class DatabaseHelper
    {
        SqlConnection connection;
        public DatabaseHelper()
        {
            createConnection();
        }

        public void createConnection()
        {
            string connectionString = "Data Source=DESKTOP-A1KBM41;Initial Catalog=Ergasia;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }
        public string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        public string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }
        public bool checkEmailInUse(string email)
        {
            connection.Open();
            string query = "Select * from Users where email=@email;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }
        public User login(string email, string password, string role)
        {
            User user = null;
            string hashedPassword, salt;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select * from Users where email = @email and role = @role;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@role", role);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    hashedPassword = reader.GetString(4);
                    salt = reader.GetString(5);
                    if (ComputeHash(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt)) == hashedPassword)
                    {
                        user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(6));
                    }
                }
            }
            connection.Close();
            return user;
        }
        public User register(string first_name, string last_name, string email, string password, string role)
        {
            User user = null;

            connection.Open();
            string salt = GenerateSalt();
            string hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt));

            if (connection.State == ConnectionState.Open)
            {
                string query = "insert into Users(first_name, last_name, email, password, salt, role) values (@first_name, @last_name, @email, @password, @salt, @role)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@first_name", first_name);
                command.Parameters.AddWithValue("@last_name", last_name);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@salt", salt);
                command.Parameters.AddWithValue("@role", role);
                command.ExecuteNonQuery();
                connection.Close();

                connection.Open();
                query = "select id from Users where email=@email";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User(reader.GetInt32(0), first_name, last_name, email, role);
                }
                connection.Close();

                if (role == "Student")
                {
                    connection.Open();
                    query = "insert into Lessons(studentID) values (@studentID)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@studentID", user.getId());
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    query = "insert into Exercises(studentID) values (@studentID)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@studentID", user.getId());
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    query = "insert into Game(studentID) values (@studentID)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@studentID", user.getId());
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            connection.Close();
            return user;
        }
        public void updatePassword(User student, string password)
        {
            connection.Open();
            string salt = GenerateSalt();
            string hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt));

            if (connection.State == ConnectionState.Open)
            {
                string query = "update Users set password=@password, salt=@salt where id=@id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", student.getId());
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@salt", salt);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public LessonClass getLessons(User student)
        {
            LessonClass lessons = null;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select * from Lessons where studentID = @id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", student.getId());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lessons = new LessonClass(student, reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                }
            }
            connection.Close();
            return lessons;
        }
        public void setLessonRead(LessonClass lessons)
        {
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = String.Format("Update Lessons set lesson1read = @read1, lesson2read = @read2, lesson3read = @read3 where studentID = @id;");
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", lessons.getStudent().getId());
                command.Parameters.AddWithValue("@read1", lessons.getRead1());
                command.Parameters.AddWithValue("@read2", lessons.getRead2());
                command.Parameters.AddWithValue("@read3", lessons.getRead3());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public ExerciseClass getExercises(User student)
        {
            ExerciseClass exercises = null;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select * from Exercises where studentID = @id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", student.getId());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    exercises = new ExerciseClass(student, reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                        reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                }
            }
            connection.Close();
            return exercises;
        }
        public void setExercise(ExerciseClass exercises)
        {
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = String.Format("Update Exercises set exercise1score = @score1, exercise1tries = @tries1, " +
                    "exercise2score = @score2, exercise2tries = @tries2, exercise3score = @score3, exercise3tries = @tries3, " +
                    "exercise4score = @score4, exercise4tries = @tries4 where studentID = @id;");
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", exercises.getStudent().getId());
                command.Parameters.AddWithValue("@score1", exercises.getExercise1());
                command.Parameters.AddWithValue("@tries1", exercises.getTries1());
                command.Parameters.AddWithValue("@score2", exercises.getExercise2());
                command.Parameters.AddWithValue("@tries2", exercises.getTries2());
                command.Parameters.AddWithValue("@score3", exercises.getExercise3());
                command.Parameters.AddWithValue("@tries3", exercises.getTries3());
                command.Parameters.AddWithValue("@score4", exercises.getExercise4());
                command.Parameters.AddWithValue("@tries4", exercises.getTries4());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Game getGameScores(User student)
        {
            Game gameScores = null;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select score1, score2, score3 from Game where studentID = @id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", student.getId());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    gameScores = new Game(student, reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                }
            }
            connection.Close();
            return gameScores;
        }
        public void setGameScores(Game game)
        {
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = String.Format("Update Game set score1 = @score1, score2 = @score2, score3 = @score3 where studentID = @id;");
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", game.getStudent().getId());
                command.Parameters.AddWithValue("@score1", game.getEasy());
                command.Parameters.AddWithValue("@score2", game.getMedium());
                command.Parameters.AddWithValue("@score3", game.getHard());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<Question> getQuestions(int lesson)
        {
            List<Question> questions = new List<Question>();
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                if (lesson == 4)
                {
                    string query = "Select id, leftNumber, rightNumber from Questions;";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        questions.Add(new Question(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }
                }
                else
                {
                    string query = "Select id, leftNumber, rightNumber from Questions where lesson = @lesson;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@lesson", lesson);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        questions.Add(new Question(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }
                }
            }
            connection.Close();
            return questions;
        }
        public List<MistakeClass> getMistakes(User user, int lesson)
        {
            List<MistakeClass> mistakes = new List<MistakeClass>();
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "select distinct questionID, lesson, leftNumber, rightNumber, answer from Mistakes " +
                               "inner join Questions on Mistakes.questionID = Questions.id " +
                               "where lesson=@lesson and studentID=@id " +
                               "order by answer";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lesson", lesson);
                command.Parameters.AddWithValue("@id", user.getId());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mistakes.Add(new MistakeClass(user, reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2) + " x " + reader.GetInt32(3) + " = " + (reader.GetInt32(2) * reader.GetInt32(3)).ToString(), reader.GetString(4)));
                }
            }
            connection.Close();
            return mistakes;
        }
        public void setMistake(MistakeClass mistake)
        {
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = String.Format("Insert into Mistakes(studentID, questionID, answer) values (@studentID, @questionID, @answer);");
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentID", mistake.getStudent().getId());
                command.Parameters.AddWithValue("@questionID", mistake.getQuestionId());
                command.Parameters.AddWithValue("@answer", mistake.getWrong());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<User> getAllStudents()
        {
            List<User> students = new List<User>();
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select id, first_name, last_name, email from Users where role = 'Student';";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "Student"));
                }
            }
            connection.Close();
            return students;
        }
        public User getStudent(int studentId)
        {
            User student = null;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select id, first_name, last_name, email from Users where role = 'Student' and id=@id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", studentId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    student = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "Student");
                }
            }
            connection.Close();
            return student;
        }

        public bool checkIfStudentExists(int studentId)
        {
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string query = "Select * from Users where role = 'Student' and id=@id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", studentId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
    }
}
