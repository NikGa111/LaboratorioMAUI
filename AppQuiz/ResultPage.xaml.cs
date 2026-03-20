namespace AppQuiz;

public partial class ResultPage : ContentPage
{
	private int _score;
    private string scorePath;
    private string content;
    public ResultPage(int score)
	{
		InitializeComponent();
		_score = score;
        SaveBest(_score);
		ScoreLabel.Text = _score.ToString();
        BestScoreLabel.Text = "BEST: " + LoadBest();
    }

    private async void OnReplayClicked(object sender, EventArgs e)
    {
        SaveBest(_score);
        await Navigation.PushAsync(new MainPage());
    }

    private void SaveBest(int score)
    {
        scorePath = Path.Combine(FileSystem.AppDataDirectory, "bestscore.txt");
        int bestNew;
        string bestName;

        if (LoadBest() != null)
        {
            bestName = NameEntry.Text;
            bestNew = int.Parse(LoadBest().Split(';')[1]);
        }
        else
        {
            bestNew = 0;
            bestName = "?";
        }

        if (score >= bestNew)
        {
            try
            {
                content = bestName + ";" + score.ToString() + ";" + DateTime.Now.ToString("d");
                File.WriteAllText(scorePath, content);
                BestScoreLabel.Text = "BEST: " + LoadBest();
            }
            catch (Exception ex)
            {
                DisplayAlert("Errore", "Impossibile salvare: " + ex.Message, "OK");
            }
        }
    }

    private string? LoadBest()
    {
        if (!File.Exists(scorePath))
        {
            File.WriteAllText(scorePath, "");
            content = "?;0;?";
        }

        try
        {
            content = File.ReadAllText(scorePath);
            int bestNew;
            DateTime dateNew;

            if (int.TryParse(content.Split(';')[1], out bestNew) && DateTime.TryParse(content.Split(';')[2], out dateNew))
            {
                return content;
            }
            else
            {
                DisplayAlert("Errore", "Valore non valido nel file", "OK");
                return null;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Errore", "Lettura fallita" + ex.Message, "OK");
            return null;
        }
    }
}