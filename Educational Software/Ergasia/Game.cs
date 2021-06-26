using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    public class Game
    {
        User student;
        int easy, medium, hard;

        public Game(User student, int easy, int medium, int hard)
        {
            this.student = student;
            this.easy = easy;
            this.medium = medium;
            this.hard = hard;
        }

        public User getStudent()
        {
            return student;
        }
        public int getEasy()
        {
            return easy;
        }
        public int getMedium()
        {
            return medium;
        }
        public int getHard()
        {
            return hard;
        }
        public void setEasy(int score)
        {
            this.easy = score;
        }
        public void setMedium(int score)
        {
            this.medium = score;
        }
        public void setHard(int score)
        {
            this.hard = score;
        }
    }
}
