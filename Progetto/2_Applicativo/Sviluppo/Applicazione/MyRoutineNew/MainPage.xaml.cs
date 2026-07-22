using MyRoutineNew.Models;
using Microsoft.Maui.Controls.Shapes;

namespace MyRoutineNew
{
    public partial class MainPage : BaseContentPage
    {

        int count = 0;

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
        }
        private void test1(object sender, EventArgs e) { MainCS.PerImpostazioni(Navigation); }
        private void test2(object sender, EventArgs e) { MainCS.PerProfilo(Navigation); }
        private void test3(object sender, EventArgs e) { MainCS.PerRoutine(Navigation); }
        private void test4(object sender, EventArgs e) { MainCS.PerStatistiche(Navigation); }


        public MainPage()
        {
            InitializeComponent();
            AggiornaPagina();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AggiornaPagina();
        }


        // Aggiorna tutto: intestazione + progress + task
        private void AggiornaPagina()
        {
            // Intestazione
            string nome    = MainCS.Nome    ?? "Utente";
            string cognome = MainCS.Cognome ?? "";
            LabelBenvenuto.Text = $"{nome} {cognome}".Trim();
            string ini = "";
            if (!string.IsNullOrEmpty(MainCS.Nome))    ini += char.ToUpper(MainCS.Nome[0]);
            if (!string.IsNullOrEmpty(MainCS.Cognome)) ini += char.ToUpper(MainCS.Cognome[0]);
            LabelAvatarIni.Text = ini.Length > 0 ? ini : "?";

            // Progresso
            var taskOggi = TaskManager.TaskOggi();
            int totali      = taskOggi.Count;
            int completate  = taskOggi.Count(t => t.Completata);
            double pct      = totali > 0 ? (double)completate / totali : 0;

            ProgressOggi.Progress    = pct;
            LabelProgressTesto.Text  = $"{completate} / {totali}";
            LabelPctTesto.Text       = $"{(int)(pct * 100)}% completato";
            LabelRimanenti.Text      = totali - completate > 0
                                       ? $"{totali - completate} rimanenti"
                                       : "✓ tutto fatto!";

            // Task
            MostraTask();
        }

        private void MostraTask()
        {
            StackTask.Children.Clear();

            var prossime = TaskManager.TaskOggi(); // tutte le task di oggi in ordine
            var corrente = TaskManager.TaskCorrente();

            if (prossime.Count == 0)
            {
                CardNessuna.IsVisible = true;
                return;
            }

            CardNessuna.IsVisible = false;

            // Mostra max 3 task: prima la corrente (se esiste), poi le prossime non completate
            var damostrare = new List<Attivita>();
            if (corrente != null && !damostrare.Contains(corrente))
                damostrare.Add(corrente);

            foreach (var t in prossime.Where(t => !t.Completata && t != corrente).Take(3 - damostrare.Count))
                damostrare.Add(t);

            // Se non arriva a 3, completa con le completate di oggi
            foreach (var t in prossime.Where(t => t.Completata).Take(3 - damostrare.Count))
                damostrare.Insert(0, t); // le completate vanno in cima

            // Prendi solo le prime 3 in ordine cronologico
            damostrare = prossime.Take(3).ToList();

            foreach (var task in damostrare)
            {
                bool isCorrente = corrente != null && task.Id == corrente.Id;
                StackTask.Children.Add(CreaCardTask(task, isCorrente));
            }
        }

        // Costruisce una card task visualmente
        private View CreaCardTask(Attivita task, bool isCorrente)
        {
            var sfondo  = isCorrente ? Color.FromArgb("#FED7AA") : Color.FromArgb("#FFFFFF");
            var bordo   = isCorrente ? Color.FromArgb("#F97316") : Color.FromArgb("#E8E0D4");
            var spessore = isCorrente ? 1.5 : 1.0;

            var dotColor = task.Completata
                ? Color.FromArgb("#10B981")
                : isCorrente
                    ? Color.FromArgb("#F97316")
                    : Color.FromArgb("#EDE5D8");

            var nomeLabel = new Label
            {
                Text            = task.Nome,
                FontSize        = 14,
                FontAttributes  = FontAttributes.Bold,
                TextColor       = isCorrente ? Color.FromArgb("#F97316") : Color.FromArgb("#1C1409")
            };

            var catLabel = new Label
            {
                Text      = $"{task.Categoria} · {task.DateTimeInizio:HH:mm}–{task.DateTimeFine:HH:mm}",
                FontSize  = 11,
                TextColor = Color.FromArgb("#7A6850")
            };

            var infoStack = new VerticalStackLayout { Spacing = 2 };
            infoStack.Children.Add(nomeLabel);
            infoStack.Children.Add(catLabel);

            if (isCorrente)
            {
                var badge = new Border
                {
                    BackgroundColor = Color.FromArgb("#F97316"),
                    StrokeThickness = 0,
                    Padding         = new Thickness(6, 2),
                    HorizontalOptions = LayoutOptions.Start,
                    StrokeShape     = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 6 }
                };
                badge.Content = new Label { Text = "IN CORSO", FontSize = 9, FontAttributes = FontAttributes.Bold, TextColor = Colors.White };
                infoStack.Children.Add(badge);
            }

            var dot = new Ellipse
            {
                WidthRequest  = 10,
                HeightRequest = 10,
                Fill          = new SolidColorBrush(dotColor),
                VerticalOptions = LayoutOptions.Center
            };

            if (!task.Completata)
            {
                dot.Stroke = new SolidColorBrush(dotColor);
                dot.StrokeThickness = task.Completata || isCorrente ? 0 : 2;
            }

            var oraLabel = new Label
            {
                Text      = $"{task.DateTimeInizio:HH:mm}",
                FontSize  = 11,
                FontAttributes = FontAttributes.Bold,
                TextColor = isCorrente ? Color.FromArgb("#F97316") : Color.FromArgb("#B8A88A"),
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.Center
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition(new GridLength(16)),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                },
                ColumnSpacing = 12
            };
            grid.Children.Add(dot);           Grid.SetColumn(dot, 0);
            grid.Children.Add(infoStack);     Grid.SetColumn(infoStack, 1);
            grid.Children.Add(oraLabel);      Grid.SetColumn(oraLabel, 2);

            var card = new Border
            {
                Margin          = new Thickness(20, 0, 20, 10),
                BackgroundColor = sfondo,
                Stroke          = new SolidColorBrush(bordo),
                StrokeThickness = spessore,
                Padding         = new Thickness(16, 14),
                StrokeShape     = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 18 },
                Content         = grid
            };

            // Tap → toglie/mette completata
            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                TaskManager.ToggleCompletata(task.Id);
                AggiornaPagina();
            };
            card.GestureRecognizers.Add(tap);

            return card;
        }

        private void OnAvatarTapped(object sender, TappedEventArgs e)
        {
            MainCS.PerProfilo(Navigation);
        }

        // Pull-to-refresh rimosso (usare OnAppearing per aggiornare)
    }
}
