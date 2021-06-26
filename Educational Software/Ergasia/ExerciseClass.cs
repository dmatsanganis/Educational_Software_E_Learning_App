using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    public class ExerciseClass
    {
        User student;
        int exercise1, tries1, exercise2, tries2, exercise3, tries3, exercise4, tries4;

        public ExerciseClass(User student, int exercise1, int tries1, int exercise2, int tries2, int exercise3, int tries3, int exercise4, int tries4)
        {
            this.student = student;
            this.exercise1 = exercise1;
            this.tries1 = tries1;
            this.exercise2 = exercise2;
            this.tries2 = tries2;
            this.exercise3 = exercise3;
            this.tries3 = tries3;
            this.exercise4 = exercise4;
            this.tries4 = tries4;
        }

        public User getStudent()
        {
            return student;
        }
        public int getExercise1()
        {
            return exercise1;
        }
        public int getExercise2()
        {
            return exercise2;
        }
        public int getExercise3()
        {
            return exercise3;
        }
        public int getExercise4()
        {
            return exercise4;
        }
        public int getTries1()
        {
            return tries1;
        }
        public int getTries2()
        {
            return tries2;
        }
        public int getTries3()
        {
            return tries3;
        }
        public int getTries4()
        {
            return tries4;
        }

        public void setExercise1(int score)
        {
            this.exercise1 = score;
            this.tries1++;
        }
        public void setExercise2(int score)
        {
            this.exercise2 = score;
            this.tries2++;
        }
        public void setExercise3(int score)
        {
            this.exercise3 = score;
            this.tries3++;
        }
        public void setExercise4(int score)
        {
            this.exercise4 = score;
            this.tries4++;
        }

        public void setTries1()
        {
            this.tries1++;
        }
        public void setTries2()
        {
            this.tries2++;
        }
        public void setTries3()
        {
            this.tries3++;

        }
        public void setTries4()
        {
            this.tries4++;
        }
    }
}
