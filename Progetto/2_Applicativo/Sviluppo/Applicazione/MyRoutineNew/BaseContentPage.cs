namespace MyRoutineNew;

public class BaseContentPage : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        this.AbortAnimation("FadeTo");

        Opacity = 0;
        await this.FadeTo(1, 500, Easing.CubicOut);
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        await this.FadeTo(0, 300, Easing.CubicIn);
    }
}