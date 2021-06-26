using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ergasia
{
    class Numbers
    {
        private PictureBox[] boxes;
        public Numbers(PictureBox Right1, PictureBox Right2, PictureBox Equal1, PictureBox Equal2, PictureBox Equal3)
        {
            boxes = new PictureBox[5] { Right1, Right2, Equal1, Equal2, Equal3 };
        }

        public void Check(object sender, Exercise exercise)
        {
            Button button = (Button)sender;
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Tag.ToString() == "empty")
                {
                    AddNumber(button, boxes[i]);
                    AddValue(button, i, exercise);
                    break;
                }
            }
        }

        private void AddNumber(Button clicked, PictureBox pictureBox)
        {
            pictureBox.Tag = clicked.Tag.ToString();
            pictureBox.Image = clicked.BackgroundImage;
        }
        
        private void AddValue(Button clicked, int i, Exercise exercise)
        {
            if (i == 0)
            {
                exercise.rightNumber += Convert.ToInt32(clicked.Tag.ToString());
            }
            else if (i == 1)
            {
                exercise.rightNumber = (exercise.rightNumber * 10) + Convert.ToInt32(clicked.Tag.ToString());
            }
            else if (i == 2)
            {
                exercise.resultNumber += Convert.ToInt32(clicked.Tag.ToString());
            }
            else if (i >= 3)
            {
                exercise.resultNumber = (exercise.resultNumber * 10) + Convert.ToInt32(clicked.Tag.ToString());
            }
        }

        public void DeleteNumber(Exercise exercise)
        {
            for (int i = boxes.Length - 1; i >= 0; i--)
            {
                if (boxes[i].Tag.ToString() != "empty" && boxes[i].Tag.ToString() != "given")
                {
                    DeleteValue(i, exercise);
                    boxes[i].Tag = "empty";
                    boxes[i].Image = null;
                    break;
                }
            }
        }

        private void DeleteValue(int i, Exercise exercise)
        {
            if (i == 0)
            {
                exercise.rightNumber -= Convert.ToInt32(boxes[i].Tag.ToString());
            }
            else if (i == 1)
            {
                exercise.rightNumber = Convert.ToInt32(exercise.rightNumber.ToString().Remove(1));
            }
            else if (i == 2)
            {
                exercise.resultNumber -= Convert.ToInt32(boxes[i].Tag.ToString());
            }
            else if (i == 3)
            {
                exercise.resultNumber = Convert.ToInt32(exercise.resultNumber.ToString().Remove(1));
            }
            else
            {
                exercise.resultNumber = Convert.ToInt32(exercise.resultNumber.ToString().Remove(2));
            }
        }
        
    }
}
