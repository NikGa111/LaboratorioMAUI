using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppQuiz.Models
{
    public class OpenQuestion : QuestionBase
    {
        private string _correctAnswer;

        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }

        public OpenQuestion(string text, int points, string correctAnswer, string image)
            : base(text, points)
        {
            _correctAnswer = correctAnswer;
        }

        public OpenQuestion(string v1, string v2, string v3, string v4)
        {
        }

        public override bool CheckAnswer(string userAnswer)
        {
            return userAnswer.Equals(_correctAnswer);
        }
    }
}
