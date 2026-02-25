using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    internal class TrueFalseQuestion : QuestionBase
    {
        private bool _correctAnswer;

        public bool CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }
        public TrueFalseQuestion(string text, int points, bool correctAnswer)
            : base(text, points)
            { 
                _correctAnswer = correctAnswer;
            }
        
    }
}
