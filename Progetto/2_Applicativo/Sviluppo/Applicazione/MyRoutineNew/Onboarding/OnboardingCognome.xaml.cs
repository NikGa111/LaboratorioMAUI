namespace MyRoutineNew.Onboarding;

public partial class OnboardingCognome : ContentPage
{

    public OnboardingCognome()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        // Mostra il nome già inserito nel titolo
        if (!string.IsNullOrEmpty(MainCS.Nome))
            LabelTitolo.Text = $"Ciao, {MainCS.Nome}!";
    }

    private async void OnAvantiClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryCognome.Text))
        {
            LabelErrore.IsVisible = true;
            return;
        }

        MainCS.Cognome = EntryCognome.Text.Trim();
        await Navigation.PushAsync(new OnboardingData(), true);
    }
}
