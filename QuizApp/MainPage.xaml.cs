using QuizApp.Models;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        private List<QuestionBase> _questions = new List<QuestionBase>();
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        public MainPage()
        {
            InitializeComponent();
            _questions.Add(new TrueFalseQuestion("Il C# è un linguaggio a oggetti?", 10, true));
            _questions.Add(new TrueFalseQuestion("Python è un linguaggio compilato?", 0, true));
        }

        private void OnAnswerClicked(object sender, EventArgs e)
        {
            
        }
    }

}
