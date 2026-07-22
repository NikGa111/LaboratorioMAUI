namespace MyRoutineNew.Onboarding;

public partial class OnboardingData : ContentPage
{

    public OnboardingData()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        // Data di default: 18 anni fa
        PickerData.Date = DateTime.Today.AddYears(-18);
    }

    private async void OnAvantiClicked(object sender, EventArgs e)
    {
        MainCS.DataNascita = PickerData.Date;
        await Navigation.PushAsync(new OnboardingFine(), true);
    }
}
