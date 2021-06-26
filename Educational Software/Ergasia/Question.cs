using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia
{
    class Question
    {
        int id, leftNumber, rightNumber, result;
        List<int> leftDigits = new List<int>();
        List<int> rightDigits = new List<int>();
        List<int> resultDigits = new List<int>();

        public Question(int id, int leftSideNumber, int rightSideNumber)
        {
            this.id = id;
            this.leftNumber = leftSideNumber;
            leftDigits = getDigits(leftNumber);
            this.rightNumber = rightSideNumber;
            this.result = leftNumber * rightNumber;
            rightDigits = getDigits(rightSideNumber);
            resultDigits = getDigits(result);
        }

        public Question(int leftSideNumber, int rightSideNumber)
        {
            this.leftNumber = leftSideNumber;
            leftDigits = getDigits(leftNumber);
            this.rightNumber = rightSideNumber;
            this.result = leftNumber * rightNumber;
            rightDigits = getDigits(rightSideNumber);
            resultDigits = getDigits(result);
        }

        public List<int> getDigits(int number)
        {
            List<int> digits = new List<int>();
            do
            {
                int digit = number % 10;
                digits.Add(digit);
                number /= 10;
            } while (number > 0);
            return digits;
        }

        public int getId()
        {
            return id;
        }
        public int getLeftNumber()
        {
            return leftNumber;
        }
        public int getRightNumber()
        {
            return rightNumber;
        }
        public int getResult()
        {
            return result;
        }
        public List<int> getLeftDigits()
        {
            return leftDigits;
        }
        public List<int> getRightDigits()
        {
            return rightDigits;
        }
        public List<int> getResultDigits()
        {
            return resultDigits;
        }
    }
}
