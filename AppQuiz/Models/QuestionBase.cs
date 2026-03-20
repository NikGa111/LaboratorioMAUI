using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppQuiz.Models
{
    public abstract class QuestionBase
    {
        private string _text;
        private int _points;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public int Points
        {
            get { return _points; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                _points = value;
            }
        }
    
        public QuestionBase(string text, int points)
        {
            _text = text;
            _points = points;
        }

        public abstract bool CheckAnswer(string userAnswer);
    }
}
