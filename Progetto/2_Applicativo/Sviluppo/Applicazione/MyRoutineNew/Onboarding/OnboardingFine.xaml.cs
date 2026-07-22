namespace MyRoutineNew.Onboarding;

public partial class OnboardingFine : ContentPage
{

    public OnboardingFine()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        string nome    = MainCS.Nome    ?? "";
        string cognome = MainCS.Cognome ?? "";

        LabelRiepilogo.Text = $"Benvenuto/a, {nome}!\nLa tua routine personale ti aspetta.";
        LabelNomeCard.Text  = $"{nome} {cognome}".Trim();
        LabelDataCard.Text  = MainCS.DataNascita.ToString("dd MMMM yyyy",
                                  new System.Globalization.CultureInfo("it-IT"));
    }

    private void OnIniziaClicked(object sender, EventArgs e)
    {
        // Salva tutto nelle Preferences
        MainCS.SalvaDatiUtente();

        // Sostituisce tutta la navigazione con la Shell principale
        Application.Current.MainPage = new AppShell();
    }
}
