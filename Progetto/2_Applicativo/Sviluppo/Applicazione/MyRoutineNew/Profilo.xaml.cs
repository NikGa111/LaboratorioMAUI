using MyRoutine.Models;

namespace MyRoutineNew;

public partial class Profilo : BaseContentPage
{

    // ── CODICE DEL COMPAGNO (invariato) ────────────────────
    // (nessun metodo esistente da preservare)
    // ───────────────────────────────────────────────────────

    public Profilo()
    {
        InitializeComponent();

        // Nome e cognome da MainCS
        string nome = MainCS.Nome ?? "Utente";
        string cognome = MainCS.Cognome ?? "";
        LabelNomeCognome.Text = $"{nome} {cognome}".Trim();

        // Iniziali avatar
        string ini = "";
        if (!string.IsNullOrEmpty(MainCS.Nome)) ini += MainCS.Nome[0];
        if (!string.IsNullOrEmpty(MainCS.Cognome)) ini += MainCS.Cognome[0];
        LabelAvatarIni.Text = ini.Length > 0 ? ini.ToUpper() : "?";

        


        AddTestBadge();//serve per inizializzare la lista di badge, in futuro che li caricheremo da file non sarà necessario: Leonardo

        LabelStatTask.Text =$"{MainCS.TotaleAttivitaCompletate()}";
        LabelStatBadge.Text = $"{MainCS.BadgeGuadagnati()}";
        LabelStatStreak.Text = $"{MainCS.Streak}";

        // Carica badge nella griglia (dati di esempio)
        // Sostituire con sorgente reale quando disponibile || fatto
        BadgeCollection.ItemsSource = MainCS.GetAllBadge();
    }

    // Restituisce la lista badge di esempio.
    // Quando il compagno implementerà il salvataggio, sostituire
    // con il caricamento dal repository/servizio dati.

    //questi badge, o comunque quelli che inseriremo, è meglio aggiungerli nella pagina inziale, quella che si apre una sola volta: Leonardo
    private void AddTestBadge()
    {
        MainCS.AddBadge(new Badge("🏆 Prima settimana", "Completa 7 giorni di routine", 0, "🏆"));
        MainCS.AddBadge(new Badge("🔥 Streak 7gg", "7 giorni consecutivi", 0, "🔥"));
        MainCS.AddBadge(new Badge("💪 Fitness x10", "10 attività fitness", 0, "💪"));
        MainCS.AddBadge(new Badge("📚 Lettore", "10 sessioni di lettura", 0, "📚"));
        MainCS.AddBadge(new Badge("🌅 Mattiniero", "Completa 5 task prima delle 8", 0, "🌅"));
        MainCS.AddBadge(new Badge("⚡ 50 Task", "50 task completate in totale", 0, "⚡"));
        MainCS.AddBadge(new Badge("🎯 Puntuale", "10 task iniziate in orario", 0, "🎯"));
        MainCS.AddBadge(new Badge("🌙 Notturno", "5 task dopo le 22:00", 0, "🌙"));
        MainCS.AddBadge(new Badge("🧘 Zen 30gg", "30 giorni di meditazione", 0, "🧘"));
        MainCS.AddBadge(new Badge("🚀 100 Task", "100 task completate", 0, "🚀"));
        MainCS.AddBadge(new Badge("👑 Campione", "Tutti i badge sbloccati", 0, "👑"));
        MainCS.AddBadge(new Badge("🌟 Perfetto", "100% completamento per 7 giorni", 0, "🌟"));
    }
}