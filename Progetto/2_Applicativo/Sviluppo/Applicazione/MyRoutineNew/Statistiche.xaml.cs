using MyRoutine.Models;

namespace MyRoutineNew;

public partial class Statistiche : BaseContentPage
{
    private readonly List<BoxView> _bars;

    public Statistiche()
    {
        InitializeComponent();
        _bars = new() { BarLun, BarMar, BarMer, BarGio, BarVen, BarSab, BarDom };
        CaricaStatistiche();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CaricaStatistiche();
    }

    private void CaricaStatistiche()
    {
        LabelStatsMese.Text = DateTime.Now.ToString("MMMM yyyy", new System.Globalization.CultureInfo("it-IT"));

        var tasks = TaskManager.CaricaTutte();
        var now = DateTime.Now;
        var mese = tasks.Where(t => t.DateTimeInizio.Month == now.Month && t.DateTimeInizio.Year == now.Year).ToList();
        var mesePrev = tasks.Where(t => t.DateTimeInizio.Month == now.AddMonths(-1).Month && t.DateTimeInizio.Year == now.AddMonths(-1).Year).ToList();

        int completateMese = mese.Count(t => t.Completata);
        int completatePrev = mesePrev.Count(t => t.Completata);
        int pct = mese.Count == 0 ? 0 : (int)((double)completateMese / mese.Count * 100);
        int pctPrev = mesePrev.Count == 0 ? 0 : (int)((double)completatePrev / mesePrev.Count * 100);

        var badges = BadgeManager.GetBadges();

        AggiornaStat(completateMese, completateMese - completatePrev,
            TaskManager.CalculateCurrentStreak(), TaskManager.CalculateRecordStreak(),
            pct, pct - pctPrev, badges.Count(x=>x.Ottenuto), badges.Count(x=>!x.Ottenuto));

        AggiornaGrafico(tasks);
        AggiornaBadgeMissioni(badges);
    }

    private void AggiornaGrafico(List<Models.Attivita> tasks)
    {
        for (int i = 0; i < 7; i++)
        {
            var dayTasks = tasks.Where(t => ((int)t.DateTimeInizio.DayOfWeek + 6) % 7 == i).ToList();
            double pct = dayTasks.Count == 0 ? 0 : (double)dayTasks.Count(t => t.Completata) / dayTasks.Count;
            _bars[i].HeightRequest = Math.Max(8, pct * 70);
            _bars[i].BackgroundColor = pct >= 1 ? Color.FromArgb("#F97316") : Color.FromArgb("#FDBA74");
        }
    }

    private void AggiornaStat(int taskMese, int deltaMese, int streak, int recordStreak, int completamentoPct, int deltaComp, int badgeGuadagnati, int badgeMancanti)
    {
        LabelTaskMese.Text = taskMese.ToString();
        LabelTaskMeseDelta.Text = $"{(deltaMese >= 0 ? "↑ +" : "↓ ")}{deltaMese} vs scorso mese";
        LabelStreak.Text = $"{streak}🔥";
        LabelStreakRecord.Text = $"Record: {recordStreak} giorni";
        LabelCompletamento.Text = $"{completamentoPct}%";
        LabelCompletamentoDelta.Text = $"{(deltaComp >= 0 ? "↑ +" : "↓ ")}{deltaComp}% vs scorso mese";
        LabelBadgeCount.Text = badgeGuadagnati.ToString();
        LabelBadgeMancanti.Text = $"{badgeMancanti} ancora da sbloccare";
    }

    private void AggiornaBadgeMissioni(List<Badge> badges)
    {
        var nearest = badges.OrderByDescending(b => b.Percentuale).Take(3).ToList();
        if (nearest.Count < 3) return;

        ApplyBadge(nearest[0], LabelMis1Nome, LabelMis1Desc, LabelMis1Pct, ProgressMis1);
        ApplyBadge(nearest[1], LabelMis2Nome, LabelMis2Desc, LabelMis2Pct, ProgressMis2);
        ApplyBadge(nearest[2], LabelMis3Nome, LabelMis3Desc, LabelMis3Pct, ProgressMis3);
    }

    private void ApplyBadge(Badge badge, Label title, Label desc, Label pct, ProgressBar progress)
    {
        title.Text = $"{badge.Emoji} {badge.Nome}";
        desc.Text = badge.Descrizione;
        pct.Text = $"{badge.Progress}/{badge.Goal}";
        progress.Progress = badge.Percentuale;

        if (badge.Ottenuto)
        {
            title.TextColor = Color.FromArgb("#F97316");
            pct.TextColor = Color.FromArgb("#F97316");
            progress.ProgressColor = Color.FromArgb("#F97316");
        }
    }
}
