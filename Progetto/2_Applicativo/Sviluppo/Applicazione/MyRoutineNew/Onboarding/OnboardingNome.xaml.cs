namespace MyRoutineNew.Onboarding;

public partial class OnboardingNome : ContentPage
{

    public OnboardingNome()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnAvantiClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryNome.Text))
        {
            LabelErrore.IsVisible = true;
            return;
        }

        MainCS.Nome = EntryNome.Text.Trim();
        await Navigation.PushAsync(new OnboardingCognome(), true);
    }
}
