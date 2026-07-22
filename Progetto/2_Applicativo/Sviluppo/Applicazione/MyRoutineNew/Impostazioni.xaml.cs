
using MyRoutineNew.Models;
using System.Text.Json;



namespace MyRoutineNew;

public partial class Impostazioni : BaseContentPage
{

    public Impostazioni()
    {
        InitializeComponent();

        MainCS.Impostazioni = new ImpostazioniClasse(false, true, false, "Italiano", new TimeOnly(6, 5), "Soleil");

        SwitchPromemoria.IsToggled = MainCS.Impostazioni.Promemoria;
        SwitchBadge.IsToggled = MainCS.Impostazioni.Avvisi;
        SwitchReport.IsToggled = MainCS.Impostazioni.Report;
        LabelLinguaValore.Text = MainCS.Impostazioni.Lingua;
        LabelInizioGiornata.Text = MainCS.Impostazioni.InizioGiornata.ToString("HH:mm");

        // Aggiorna il selettore di tema in base al tema attuale
        //AggiornaSelettoreTema();

    }

    // ── Switch Notifiche ────────────────────────────────────

    private void OnSwitchPromemoriaToggled(object sender, ToggledEventArgs e)
    {
        MainCS.Impostazioni.Promemoria = e.Value;
    }

    private void OnSwitchBadgeToggled(object sender, ToggledEventArgs e)
    {
        MainCS.Impostazioni.Avvisi = e.Value;
    }

    private void OnSwitchReportToggled(object sender, ToggledEventArgs e)
    {
        MainCS.Impostazioni.Report = e.Value;
    }

    // ── Preferenze ──────────────────────────────────────────

    private async void OnLinguaTapped(object sender, TappedEventArgs e)
    {
        string scelta = await DisplayActionSheet(
            "Lingua", "Annulla", null,
            "Italiano", "English", "Español", "Français");

        if (!string.IsNullOrEmpty(scelta) && scelta != "Annulla")
        {
            LabelLinguaValore.Text = scelta;
            MainCS.Impostazioni.Lingua = scelta;
        }
    }

    private async void OnInizioGiornataTapped(object sender, TappedEventArgs e)
    {
        string ora = await DisplayPromptAsync(
            "Inizio giornata", "Inserisci l'orario (HH:mm):",
            accept: "Salva", cancel: "Annulla",
            initialValue: LabelInizioGiornata.Text,
            keyboard: Keyboard.Numeric, maxLength: 5);

        if (!string.IsNullOrWhiteSpace(ora))
        {
            LabelInizioGiornata.Text = ora;
            if (TimeOnly.TryParse(ora, out TimeOnly nuovoOrario))
            {
                MainCS.Impostazioni.InizioGiornata = nuovoOrario;
            }
        }
    }

    // ── SELETTORE TEMA ──────────────────────────────────────
    /*
    private void AggiornaSelettoreTema()
    {
        // Ripristina lo stato dei bordi di tutti i temi
        ThemeSoleil.StrokeThickness = 0;
        ThemeNightForest.StrokeThickness = 0;

        // Evidenzia il tema attualmente selezionato
        if (MainCS.Impostazioni.Tema == "Soleil")
        {
            ThemeSoleil.StrokeThickness = 3;
            ThemeSoleil.Stroke = Color.FromArgb("#F97316");
        }
        else if (MainCS.Impostazioni.Tema == "NightForest")
        {
            ThemeNightForest.StrokeThickness = 3;
            ThemeNightForest.Stroke = Color.FromArgb("#00D9FF");
        }
    }

    private void OnThemeSoleilTapped(object sender, TappedEventArgs e)
    {
        CambiatTema("Soleil");
    }

    private void OnThemeNightForestTapped(object sender, TappedEventArgs e)
    {
        CambiatTema("NightForest");
    }

    private void CambiatTema(string nomeTema)
    {
        // Salva il tema nelle impostazioni
        MainCS.Impostazioni.Tema = nomeTema;

        // Carica i colori appropriati
        CaricaColoriTema(nomeTema);

        // Aggiorna il selettore visivo
        AggiornaSelettoreTema();
    }


    private void CaricaColoriTema(string nomeTema)
    {
        var appResources = Application.Current?.Resources;
        if (appResources == null) return;

        if (nomeTema == "Soleil")
        {
            // ── TEMA SOLEIL (Arancio/Caldo) ──────────────────────────────────
            appResources["BgPage"] = Color.FromArgb("#FDF7F0");
            appResources["BgCard"] = Color.FromArgb("#FFFFFF");
            appResources["BgSurface"] = Color.FromArgb("#F5EFE6");
            appResources["BgMuted"] = Color.FromArgb("#EDE5D8");

            appResources["Accent"] = Color.FromArgb("#F97316");
            appResources["AccentSoft"] = Color.FromArgb("#FED7AA");
            appResources["AccentGreen"] = Color.FromArgb("#10B981");
            appResources["AccentRed"] = Color.FromArgb("#F43F5E");

            appResources["TextPrimary"] = Color.FromArgb("#1C1409");
            appResources["TextSecondary"] = Color.FromArgb("#7A6850");
            appResources["TextMuted"] = Color.FromArgb("#B8A88A");

            appResources["BorderColor"] = Color.FromArgb("#E8E0D4");
            appResources["NavBg"] = Color.FromArgb("#FFFFFF");
            appResources["NavUnselected"] = Color.FromArgb("#B8A88A");

            appResources["ProgressBg"] = Color.FromArgb("#EDE5D8");
            appResources["ProgressFill"] = Color.FromArgb("#F97316");

            appResources["BadgeEarned"] = Color.FromArgb("#F97316");
            appResources["BadgeLocked"] = Color.FromArgb("#EDE5D8");

            // Colori di compatibilità
            appResources["Primary"] = Color.FromArgb("#F97316");
            appResources["PrimaryDark"] = Color.FromArgb("#EA580C");
            appResources["Secondary"] = Color.FromArgb("#FED7AA");
            appResources["Tertiary"] = Color.FromArgb("#10B981");
            appResources["Gray100"] = Color.FromArgb("#F5EFE6");
            appResources["Gray200"] = Color.FromArgb("#EDE5D8");
            appResources["Gray300"] = Color.FromArgb("#E8E0D4");
            appResources["Gray400"] = Color.FromArgb("#B8A88A");
            appResources["Gray500"] = Color.FromArgb("#7A6850");
            appResources["Gray600"] = Color.FromArgb("#5C4A36");
            appResources["Gray900"] = Color.FromArgb("#2E1F0E");
            appResources["Gray950"] = Color.FromArgb("#1C1409");
        }
        else if (nomeTema == "NightForest")
        {
            // ── TEMA NIGHT FOREST (Blu/Freddo) ──────────────────────────────
            appResources["BgPage"] = Color.FromArgb("#0F1419");
            appResources["BgCard"] = Color.FromArgb("#1A232F");
            appResources["BgSurface"] = Color.FromArgb("#243447");
            appResources["BgMuted"] = Color.FromArgb("#2D3E4F");

            appResources["Accent"] = Color.FromArgb("#00D9FF");
            appResources["AccentSoft"] = Color.FromArgb("#4DF2FF");
            appResources["AccentGreen"] = Color.FromArgb("#00E5A0");
            appResources["AccentRed"] = Color.FromArgb("#FF4757");

            appResources["TextPrimary"] = Color.FromArgb("#E8F4F8");
            appResources["TextSecondary"] = Color.FromArgb("#A8C5D4");
            appResources["TextMuted"] = Color.FromArgb("#7A8FA3");

            appResources["BorderColor"] = Color.FromArgb("#364A5C");
            appResources["NavBg"] = Color.FromArgb("#1A232F");
            appResources["NavUnselected"] = Color.FromArgb("#7A8FA3");

            appResources["ProgressBg"] = Color.FromArgb("#2D3E4F");
            appResources["ProgressFill"] = Color.FromArgb("#00D9FF");

            appResources["BadgeEarned"] = Color.FromArgb("#00D9FF");
            appResources["BadgeLocked"] = Color.FromArgb("#2D3E4F");

            // Colori di compatibilità
            appResources["Primary"] = Color.FromArgb("#00D9FF");
            appResources["PrimaryDark"] = Color.FromArgb("#0099B3");
            appResources["Secondary"] = Color.FromArgb("#4DF2FF");
            appResources["Tertiary"] = Color.FromArgb("#00E5A0");
            appResources["Gray100"] = Color.FromArgb("#243447");
            appResources["Gray200"] = Color.FromArgb("#2D3E4F");
            appResources["Gray300"] = Color.FromArgb("#364A5C");
            appResources["Gray400"] = Color.FromArgb("#7A8FA3");
            appResources["Gray500"] = Color.FromArgb("#A8C5D4");
            appResources["Gray600"] = Color.FromArgb("#C0D3E0");
            appResources["Gray900"] = Color.FromArgb("#1A232F");
            appResources["Gray950"] = Color.FromArgb("#0F1419");
        }

        // Aggiorna il colore di sfondo della pagina corrente
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (appResources.TryGetValue("BgPage", out var bgColor) && bgColor is Color color)
            {
                this.BackgroundColor = color;
            }
        });
    }*/

    // ── Riconfigura dati  ────────────────────────────────

    private async void OnRiconfiguraTapped(object sender, TappedEventArgs e)
    {
        bool conferma = await DisplayAlert(
            "Riconfigura i miei dati",
            "Vuoi ripetere la configurazione iniziale?\n" +
            "I tuoi dati personali verranno aggiornati.\n" +
            "Le task salvate verranno eliminate.",
            "Sì, riconfigura",
            "Annulla");

        if (!conferma) return;

        // Reset completo (dati utente + task)
        MainCS.ResetCompleto();

        // Rilancia l'onboarding
        Application.Current.MainPage = new NavigationPage(new Onboarding.OnboardingNome())
        {
            BarBackgroundColor = Color.FromArgb("#FDF7F0"),
            BarTextColor = Color.FromArgb("#1C1409")
        };
    }

    // ── Esci ────────────────────────────────────────────────

    private async void OnEsciTapped(object sender, TappedEventArgs e)
    {
        bool conferma = await DisplayAlert(
            "Esci", "Sei sicuro di voler uscire?",
            "Sì, esci", "Annulla");

        if (!conferma) return;

        MainCS.Nome = null;
        MainCS.Cognome = null;
        MainCS.Tema = MainCS.Impostazioni.Tema;
    }

    private async void ExportDataClicked(object sender, EventArgs e)
    {
        try
        {
            var backupData = new UserBackupData
            {
                Nome = MainCS.Nome,
                Cognome = MainCS.Cognome,

                Tasks = TaskManager.CaricaTutte(),
                Badges = BadgeManager.GetBadges(),

                CurrentStreak = MainCS.Streak,
                BestStreak = MainCS.RecordStreak,

                TotalCompletedTasks = MainCS.TotaleAttivitaCompletate(),

                ExportDate = DateTime.Now
            };

            string json = JsonSerializer.Serialize(
                backupData,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            string fileName = $"MyRoutineBackup_{DateTime.Now:yyyyMMdd_HHmmss}.json";

            string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            File.WriteAllText(filePath, json);

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Esporta dati",
                File = new ShareFile(filePath)
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", ex.Message, "OK");
        }
    }

}
