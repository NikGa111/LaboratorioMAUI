using System.Text.Json;
using MyRoutine.Models;

namespace MyRoutineNew;

public static class BadgeManager
{
    private const string PREF_KEY = "badges_v2";

    public static List<Badge> GetBadges()
    {
        var json = Preferences.Default.Get(PREF_KEY, string.Empty);
        List<Badge>? badges = null;
        if (!string.IsNullOrWhiteSpace(json))
        {
            badges = JsonSerializer.Deserialize<List<Badge>>(json);
        }

        badges ??= CreateDefaultBadges();
        UpdateBadges(badges);
        SaveBadges(badges);
        return badges;
    }

    public static void SaveBadges(List<Badge> badges)
    {
        Preferences.Default.Set(PREF_KEY, JsonSerializer.Serialize(badges));
    }

    private static List<Badge> CreateDefaultBadges() => new()
    {
        new Badge("Task Grinder", "Completa 100 task", 100, "🚀"),
        new Badge("Night Owl", "Completa 5 task dopo le 22", 5, "🌙"),
        new Badge("Consistency King", "Mantieni 30 giorni di streak", 30, "🧘")
    };

    public static void UpdateBadges(List<Badge> badges)
    {
        var allTasks = TaskManager.CaricaTutte();
        var completed = allTasks.Count(t => t.Completata);
        var night = allTasks.Count(t => t.Completata && t.DateTimeFine.Hour >= 22);
        var streak = TaskManager.CalculateCurrentStreak();

        badges.First(x=>x.Nome=="Task Grinder").AggiornaProgress(completed);
        badges.First(x=>x.Nome=="Night Owl").AggiornaProgress(night);
        badges.First(x=>x.Nome=="Consistency King").AggiornaProgress(streak);
    }
}
