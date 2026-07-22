using QuizApp.Models;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        private List<QuestionBase> _questions = new List<QuestionBase>();
        private int _currentIndex = 0;
        private int _score = 0;
        public MainPage()
        {
            InitializeComponent();
            _questions.Add(new TrueFalseQuestion("Caricamento", 0, true));
            _questions.Add(new TrueFalseQuestion("Il C# è un linguaggio a oggetti?", 10, true));
            _questions.Add(new TrueFalseQuestion("Python è un linguaggio compilato?", 10, true));
            _questions.Add(new TrueFalseQuestion("Clicca per terminare il quiz", 0, true));
        }

        private void ShowQuestion()
        {
            //Controlla se il numero della domanda corrente è minore della lunghezza della lista
            if (_currentIndex < _questions.Count)
            {
                //Variabile locale che contiene la domanda corrente
                QuestionBase current = _questions[_currentIndex];
                //Modifica del testo del label con il testo della domanda corrente
                QuestionTextLabel.Text = current.Text;
                //Modifica del testo dello score con il nuovo punteggio raggiunto
                ScoreLabel.Text = $"Punti: {_score}";
            }
            else
            {
                QuestionTextLabel.Text = $"Fine! Punteggio finale: {_score}";
                TrueButton.IsVisible = false;
                FalseButton.IsVisible = false;
            }
        }

        private async void OnAnswerClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            bool userAnswer = bool.Parse(btn.CommandParameter.ToString());

            if (_questions[_currentIndex].CheckAnswer(userAnswer))
            {
                _score += _questions[_currentIndex].Points;
                await DisplayAlert("Esatto!", "Hai indovinato.", "OK");
            }
            else
            {
                await DisplayAlert("Errore", "Riprova alla prossima.", "OK");
            }

            _currentIndex++;
            ShowQuestion();
        }
    }

}
