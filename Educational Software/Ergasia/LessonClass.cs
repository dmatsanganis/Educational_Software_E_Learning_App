using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    public class LessonClass
    {
        User student;
        int read1, read2, read3;

        public LessonClass(User student, int read1, int read2, int read3)
        {
            this.student = student;
            this.read1 = read1;
            this.read2 = read2;
            this.read3 = read3;
        }

        public User getStudent()
        {
            return student;
        }
        public int getRead1()
        {
            return read1;
        }
        public void setRead1(int read1)
        {
            this.read1 = read1;
        }
        public int getRead2()
        {
            return read2;
        }
        public void setRead2(int read2)
        {
            this.read2 = read2;
        }
        public int getRead3()
        {
            return read3;
        }
        public void setRead3(int read3)
        {
            this.read3 = read3;
        }
    }
}
