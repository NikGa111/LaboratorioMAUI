using MyRoutineNew.Models;

namespace MyRoutineNew;

public partial class Routine : BaseContentPage
{
    // ── CODICE DEL COMPAGNO (invariato) ────────────────────
    // (nessun metodo esistente da preservare)
    // ───────────────────────────────────────────────────────

    // Tiene traccia dei pannelli aperti (key = data.ToString("d"))
    private readonly Dictionary<string, bool> _espansi = new();

    public Routine()
    {
        InitializeComponent();
        LabelMeseCorrente.Text = DateTime.Now.ToString("MMMM yyyy",
    new System.Globalization.CultureInfo("it-IT"));
        RicostruisciAgenda();
    }

    // ── Costruzione UI ──────────────────────────────────────

    private void RicostruisciAgenda()
    {
        StackGiorni.Children.Clear();

        var gruppi = TaskManager.ProssimiGiorni(14); // 14 giorni in avanti

        // Aggiungi sempre il giorno di oggi anche se vuoto
        if (!gruppi.ContainsKey(DateTime.Today))
            gruppi[DateTime.Today] = new List<Attivita>();

        foreach (var (data, tasks) in gruppi.OrderBy(g => g.Key))
        {
            string chiave = data.ToString("d");
            if (!_espansi.ContainsKey(chiave))
                _espansi[chiave] = data == DateTime.Today; // oggi aperto di default

            StackGiorni.Children.Add(CreaCardGiorno(data, tasks, chiave));
        }
    }

    private View CreaCardGiorno(DateTime data, List<Attivita> tasks, string chiave)
    {
        bool isOggi   = data == DateTime.Today;
        bool isAperto = _espansi[chiave];

        // Header numero giorno
        var numLabel = new Label
        {
            Text       = data.Day.ToString(),
            FontSize   = 15, FontAttributes = FontAttributes.Bold,
            TextColor  = isOggi ? Colors.White : Color.FromArgb("#1C1409"),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions   = LayoutOptions.Center
        };
        var numBorder = new Border
        {
            WidthRequest    = 36, HeightRequest = 36,
            BackgroundColor = isOggi ? Color.FromArgb("#F97316") : Color.FromArgb("#EDE5D8"),
            StrokeThickness = 0,
            StrokeShape     = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 12 },
            Content         = numLabel,
            VerticalOptions = LayoutOptions.Center
        };

        var cultIT = new System.Globalization.CultureInfo("it-IT");
        string giorno = isOggi
            ? $"{data.ToString("dddd", cultIT)} · Oggi"
            : data.ToString("dddd d MMMM", cultIT);

        var giornoLabel = new Label
        {
            Text = char.ToUpper(giorno[0]) + giorno.Substring(1),
            FontSize = 13, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#1C1409")
        };
        var subLabel = new Label
        {
            Text = tasks.Count == 0 ? "Nessuna task" : $"{tasks.Count} task",
            FontSize = 10, TextColor = Color.FromArgb("#7A6850")
        };
        var infoStack = new VerticalStackLayout { Spacing = 1, VerticalOptions = LayoutOptions.Center };
        infoStack.Children.Add(giornoLabel);
        infoStack.Children.Add(subLabel);

        var pillBorder = new Border
        {
            BackgroundColor = Color.FromArgb("#EDE5D8"),
            StrokeThickness = 0, Padding = new Thickness(8, 3),
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 99 },
            VerticalOptions = LayoutOptions.Center
        };
        pillBorder.Content = new Label { Text = tasks.Count.ToString(), FontSize = 10, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#7A6850") };

        var chevron = new Label
        {
            Text = isAperto ? "▾" : "▸",
            FontSize = 14, TextColor = Color.FromArgb("#B8A88A"),
            VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center
        };

        var headerGrid = new Grid
        {
            Padding = new Thickness(16, 14),
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(new GridLength(48)),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(new GridLength(24))
            },
            ColumnSpacing = 12
        };
        headerGrid.Children.Add(numBorder);     Grid.SetColumn(numBorder, 0);
        headerGrid.Children.Add(infoStack);     Grid.SetColumn(infoStack, 1);
        headerGrid.Children.Add(pillBorder);    Grid.SetColumn(pillBorder, 2);
        headerGrid.Children.Add(chevron);       Grid.SetColumn(chevron, 3);

        // Pannello task (collassabile)
        var sep = new BoxView { HeightRequest = 1, BackgroundColor = Color.FromArgb("#E8E0D4"), IsVisible = isAperto };

        var taskStack = new VerticalStackLayout
        {
            Padding   = new Thickness(16, 10, 16, 14),
            Spacing   = 8,
            IsVisible = isAperto
        };
        foreach (var t in tasks)
            taskStack.Children.Add(CreaRigaTask(t));

        if (tasks.Count == 0 && isAperto)
            taskStack.Children.Add(new Label { Text = "Nessuna task per questo giorno.", FontSize = 12, TextColor = Color.FromArgb("#B8A88A") });

        // Contenitore card
        var cardContent = new VerticalStackLayout();
        cardContent.Children.Add(headerGrid);
        cardContent.Children.Add(sep);
        cardContent.Children.Add(taskStack);

        var card = new Border
        {
            Margin          = new Thickness(16, 0, 16, 10),
            BackgroundColor = Colors.White,
            Stroke          = new SolidColorBrush(isOggi ? Color.FromArgb("#F97316") : Color.FromArgb("#E8E0D4")),
            StrokeThickness = isOggi ? 1.5 : 1,
            StrokeShape     = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 18 },
            Content         = cardContent
        };

        // Tap header → espandi/comprimi
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) =>
        {
            _espansi[chiave] = !_espansi[chiave];
            sep.IsVisible      = _espansi[chiave];
            taskStack.IsVisible = _espansi[chiave];
            chevron.Text       = _espansi[chiave] ? "▾" : "▸";
        };
        headerGrid.GestureRecognizers.Add(tap);

        return card;
    }

    private View CreaRigaTask(Attivita task)
    {
        // Strip colore sinistra (per categoria)
        var strip = new BoxView
        {
            WidthRequest = 4, HeightRequest = 36,
            BackgroundColor = task.Completata
                ? Color.FromArgb("#10B981")
                : Color.FromArgb("#F97316"),
            VerticalOptions = LayoutOptions.Center,
            CornerRadius    = 2
        };

        var nome = new Label
        {
            Text          = task.Nome,
            FontSize      = 12, FontAttributes = FontAttributes.Bold,
            TextColor     = task.Completata
                ? Color.FromArgb("#B8A88A")
                : Color.FromArgb("#1C1409")
        };
        if (task.Completata)
            nome.TextDecorations = TextDecorations.Strikethrough;

        var sub = new Label
        {
            Text      = $"{task.Categoria} · {task.DateTimeInizio:HH:mm}–{task.DateTimeFine:HH:mm}",
            FontSize  = 10, TextColor = Color.FromArgb("#7A6850")
        };
        var info = new VerticalStackLayout { Spacing = 2, VerticalOptions = LayoutOptions.Center };
        info.Children.Add(nome);
        info.Children.Add(sub);

        // Checkbox
        var check = new Label
        {
            Text      = task.Completata ? "✓" : "○",
            FontSize  = 16,
            TextColor = task.Completata ? Color.FromArgb("#10B981") : Color.FromArgb("#B8A88A"),
            VerticalOptions   = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        var row = new Grid
        {
            Padding  = new Thickness(12, 10),
            BackgroundColor = Color.FromArgb("#F5EFE6"),
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(new GridLength(4)),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(new GridLength(32))
            },
            ColumnSpacing = 10
        };
        row.Children.Add(strip);  Grid.SetColumn(strip, 0);
        row.Children.Add(info);   Grid.SetColumn(info,  1);
        row.Children.Add(check);  Grid.SetColumn(check, 2);

        var rowBorder = new Border
        {
            StrokeThickness = 0,
            StrokeShape     = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 12 },
            Content         = row
        };

        // Tap → completa/decompleta
        var tapCheck = new TapGestureRecognizer();
        tapCheck.Tapped += (s, e) =>
        {
            TaskManager.ToggleCompletata(task.Id);
            RicostruisciAgenda();
        };
        rowBorder.GestureRecognizers.Add(tapCheck);

        // Long press → elimina
        var lpg = new PointerGestureRecognizer();
        // Usa TapGestureRecognizer con NumberOfTapsRequired = 2 come alternativa
        // per eliminare (double tap)
        var tapDel = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
        tapDel.Tapped += async (s, e) =>
        {
            bool ok = await Application.Current.MainPage.DisplayAlert(
                "Elimina task",
                $"Vuoi eliminare \"{task.Nome}\"?",
                "Sì", "No");
            if (ok)
            {
                TaskManager.Rimuovi(task.Id);
                RicostruisciAgenda();
            }
        };
        rowBorder.GestureRecognizers.Add(tapDel);

        return rowBorder;
    }

    // ── Aggiungi task ───────────────────────────────────────

    private async void OnAggiungiClicked(object sender, EventArgs e)
    {
        // Step 1: Nome
        string nome = await DisplayPromptAsync(
            "Nuova Task", "Nome dell'attività:",
            accept: "Avanti", cancel: "Annulla", maxLength: 60);
        if (string.IsNullOrWhiteSpace(nome)) return;

        // Step 2: Categoria
        string categoria = await DisplayActionSheet(
            "Categoria", "Annulla", null,
            "Fitness", "Benessere", "Studio", "Lavoro", "Hobby", "Altro");
        if (string.IsNullOrEmpty(categoria) || categoria == "Annulla") categoria = "Altro";

        
        // Step 3: Data (usa DateTime.Today come default)
        string dataStr = await DisplayPromptAsync(
            "Nuova Task", "Data (gg/mm/aaaa):",
            accept: "Avanti", cancel: "Annulla",
            initialValue: DateTime.Today.ToString("dd/MM/yyyy"),
            maxLength: 10);

        if (!DateTime.TryParse(
                dataStr,
                new System.Globalization.CultureInfo("it-IT"),
                System.Globalization.DateTimeStyles.None,
                out var data))
        {
            await DisplayAlert(
                "Errore",
                $"Data non valida: {dataStr}",
                "OK");
            return;
        }

        

        // Step 4: Ora inizio
        string inizio = await DisplayPromptAsync(
            "Nuova Task", "Ora inizio (HH:mm):",
            accept: "Avanti", cancel: "Annulla",
            initialValue: DateTime.Now.ToString("HH:00"),
            maxLength: 5);
        if (!TimeSpan.TryParseExact(inizio, @"hh\:mm",
            System.Globalization.CultureInfo.InvariantCulture, out var tsInizio))
            tsInizio = TimeSpan.FromHours(DateTime.Now.Hour);

        // Step 5: Ora fine
        string fine = await DisplayPromptAsync(
            "Nuova Task", "Ora fine (HH:mm):",
            accept: "Salva", cancel: "Annulla",
            initialValue: (DateTime.Now.Hour + 1).ToString("00") + ":00",
            maxLength: 5);
        if (!TimeSpan.TryParseExact(fine, @"hh\:mm",
            System.Globalization.CultureInfo.InvariantCulture, out var tsFine))
            tsFine = tsInizio.Add(TimeSpan.FromHours(1));

        var nuova = new Attivita
        {
            Nome           = nome.Trim(),
            Descrizione    = "",
            Categoria      = categoria,
            DateTimeInizio = data.Date + tsInizio,
            DateTimeFine   = data.Date + tsFine
        };

        TaskManager.Aggiungi(nuova);
        RicostruisciAgenda();
    }
}
