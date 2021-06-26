using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    class MistakeClass
    {
        User student;
        int questionId, lesson;
        string correct, wrong;

        public MistakeClass(User student, int questionId, int lesson, string correct, string wrong)
        {
            this.student = student;
            this.questionId = questionId;
            this.lesson = lesson;
            this.correct = correct;
            this.wrong = wrong;
        }

        public User getStudent()
        {
            return student;
        }
        public int getQuestionId()
        {
            return questionId;
        }
        public int getLesson()
        {
            return lesson;
        }
        public string getCorrect()
        {
            return correct;
        }
        public string getWrong()
        {
            return wrong;
        }
    }
}
