using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppQuiz.Models
{
    public class TrueFalseQuestion : QuestionBase
    {
        private string _correctAnswer;

        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }

        public TrueFalseQuestion(string text, int points, string correctAnswer, string image)
            : base(text, points)
        {
            _correctAnswer = correctAnswer;
        }

        public TrueFalseQuestion(string v1, string v2, string v3, string v4)
        {
        }

        public override bool CheckAnswer(string userAnswer)
        {
            return bool.Parse(userAnswer.ToString()) == bool.Parse(_correctAnswer.ToString());
        }
    }
}
