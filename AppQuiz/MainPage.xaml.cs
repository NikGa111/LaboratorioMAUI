using AppQuiz.Models;

namespace AppQuiz
{
    public partial class MainPage : ContentPage
    {
        private List<QuestionBase> _questions = new List<QuestionBase>();
        private int _currentIndex = 0;
        private int _score = 0;
        private string content;
        private string question;
        private string[] questionContent;
        private string questionPath = Path.Combine(FileSystem.AppDataDirectory, "domande.txt");

        public MainPage()
        {
            InitializeComponent();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            if (!File.Exists(questionPath))
            {
                File.WriteAllText(questionPath, "");
            }

            try
            {
                content = File.ReadAllText(questionPath);
                for (int i = 0; i < content.Split('/').Length; i++)
                {
                    question = content.Split("/")[i];
                    questionContent = question.Split(';');
                    if (question.StartsWith("TF"))
                    {
                        _questions.Add(new TrueFalseQuestion(questionContent[1], questionContent[2], questionContent[3], questionContent[4]));
                    }
                    else if (question.StartsWith("OPEN"))
                    {
                        _questions.Add(new OpenQuestion(questionContent[1], questionContent[2], questionContent[3], questionContent[4]));
                    }
                    else
                    {
                        DisplayAlert("Errore", "Domanda invalida", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Errore", "Lettura fallita" + ex.Message, "OK");
            }
        }
        private void ShowQuestion()
        {
            if (_currentIndex < _questions.Count)
            {
                QuestionBase current = _questions[_currentIndex];
                if (current.GetType() == typeof(TrueFalseQuestion))
                {
                    OpenAnswer.IsVisible = false;
                    TrueFalseAnswer.IsVisible = true;
                }
                else if (current.GetType() == typeof(OpenQuestion))
                {
                    TrueFalseAnswer.IsVisible = false;
                    OpenAnswer.IsVisible = true;
                }
                QuestionTextLabel.Text = current.Text;
                ScoreLabel.Text = $"Punti: {_score}";
            }
            else
            {
                OnQuizFinished();
            }
        }
    
        private async void OnAnswerClicked(object sender, EventArgs e)
        {
            string userAnswer;
            var btn = (Button)sender;
            if(btn.CommandParameter.ToString().Equals("Open"))
            {
                userAnswer = QuestionTextEntry.Text;
            }
            else
            {
                userAnswer = btn.CommandParameter.ToString();
            }

            if (_questions[_currentIndex].CheckAnswer(userAnswer))
            {
                _score += _questions[_currentIndex].Points;
                await DisplayAlert("Esatto!", "Hai indovinato.", "OK");
            }
            else
            {
                await DisplayAlert("Errore!", "Riprova alla prossima.", "OK");
            }
            _currentIndex++;
            ShowQuestion();
        }

        private async void OnQuizFinished()
        {
            await Navigation.PushAsync(new ResultPage(_score));
        }
    }

}
