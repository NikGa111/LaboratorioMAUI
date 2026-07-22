namespace MyRoutineNew
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (MainCS.SetupCompletato)
            {
                // Utente già configurato → carica dati e vai alla shell principale
                MainCS.CaricaDatiUtente();
                MainPage = new AppShell();
            }
            else
            {
                // Prima apertura → avvia onboarding
                MainPage = new NavigationPage(new Onboarding.OnboardingNome())
                {
                    BarBackgroundColor = Color.FromArgb("#FDF7F0"),
                    BarTextColor       = Color.FromArgb("#1C1409")
                };
            }
        }
    }
}
