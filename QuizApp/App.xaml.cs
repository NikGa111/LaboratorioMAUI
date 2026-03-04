namespace QuizApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        private async void OnQuizFinished()
        {
            await Navigation.PushAsync(new ResultPage());
        }
    }
}
