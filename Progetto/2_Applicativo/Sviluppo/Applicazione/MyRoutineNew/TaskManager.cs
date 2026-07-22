using MyRoutineNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MyRoutineNew
{
    /// <summary>
    /// Gestisce il caricamento e il salvataggio delle task tramite Preferences.
    /// Unico punto di accesso ai dati delle attività nell'app.
    /// </summary>
    public static class TaskManager
    {
        private const string PREF_KEY = "tasks_v1";

        // ── Carica/Salva ────────────────────────────────────

        /// <summary>Restituisce tutte le task salvate.</summary>
        public static List<Attivita> CaricaTutte()
        {
            string json = Preferences.Default.Get(PREF_KEY, "");
            if (string.IsNullOrEmpty(json)) return new List<Attivita>();

            try
            {
                return JsonSerializer.Deserialize<List<Attivita>>(json)
                       ?? new List<Attivita>();
            }
            catch
            {
                return new List<Attivita>();
            }
        }

        /// <summary>Salva l'intera lista di task.</summary>
        public static void Salva(List<Attivita> lista)
        {
            string json = JsonSerializer.Serialize(lista);
            Preferences.Default.Set(PREF_KEY, json);
        }

        /// <summary>Aggiunge una nuova task e salva.</summary>
        public static void Aggiungi(Attivita task)
        {
            var lista = CaricaTutte();
            lista.Add(task);
            Salva(lista);
        }

        /// <summary>Rimuove una task per Id e salva.</summary>
        public static void Rimuovi(string id)
        {
            var lista = CaricaTutte();
            lista.RemoveAll(t => t.Id == id);
            Salva(lista);
        }

        /// <summary>Aggiorna lo stato Completata di una task e salva.</summary>
        public static void ToggleCompletata(string id)
        {
            var lista = CaricaTutte();
            var task  = lista.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Completata = !task.Completata;
                Salva(lista);
            }
        }

        // ── Query utili ─────────────────────────────────────

        /// <summary>Task di oggi ordinate per orario di inizio.</summary>
        public static List<Attivita> TaskOggi()
        {
            var oggi = DateTime.Today;
            return CaricaTutte()
                .Where(t => t.DateTimeInizio.Date == oggi)
                .OrderBy(t => t.DateTimeInizio)
                .ToList();
        }

        /// <summary>
        /// Task attualmente "in corso": l'orario corrente è tra Inizio e Fine.
        /// Restituisce null se nessuna è in corso.
        /// </summary>
        public static Attivita TaskCorrente()
        {
            var now = DateTime.Now;
            return CaricaTutte()
                .Where(t => t.DateTimeInizio.Date == DateTime.Today
                         && t.DateTimeInizio <= now
                         && t.DateTimeFine   >= now
                         && !t.Completata)
                .OrderBy(t => t.DateTimeInizio)
                .FirstOrDefault();
        }

        /// <summary>
        /// Prossime task non completate di oggi, in ordine cronologico.
        /// Usata nella Home per la lista delle prossime attività.
        /// </summary>
        public static List<Attivita> ProssimeTask(int quante = 3)
        {
            var now  = DateTime.Now;
            var oggi = DateTime.Today;
            return CaricaTutte()
                .Where(t => t.DateTimeInizio.Date == oggi && !t.Completata)
                .OrderBy(t => t.DateTimeInizio)
                .Take(quante)
                .ToList();
        }

        /// <summary>
        /// Task dei prossimi N giorni (incluso oggi), raggruppate per data.
        /// Usata nell'Agenda.
        /// </summary>
        public static Dictionary<DateTime, List<Attivita>> ProssimiGiorni(int giorni = 7)
        {
            var from = DateTime.Today;
            var to   = from.AddDays(giorni);

            return CaricaTutte()
                .Where(t => t.DateTimeInizio.Date >= from && t.DateTimeInizio.Date < to)
                .GroupBy(t => t.DateTimeInizio.Date)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.OrderBy(t => t.DateTimeInizio).ToList());
        }


        public static int CalculateCurrentStreak()
        {
            var completedDays = CaricaTutte()
                .Where(t => t.Completata)
                .Select(t => t.DateTimeInizio.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            int streak = 0;
            var day = DateTime.Today;

            while (completedDays.Contains(day))
            {
                streak++;
                day = day.AddDays(-1);
            }

            return streak;
        }

        public static int CalculateRecordStreak()
        {
            var days = CaricaTutte()
                .Where(t => t.Completata)
                .Select(t => t.DateTimeInizio.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            int best = 0;
            int current = 0;
            DateTime? previous = null;

            foreach (var day in days)
            {
                if (previous == null || day == previous.Value.AddDays(1))
                    current++;
                else
                    current = 1;

                best = Math.Max(best, current);
                previous = day;
            }

            return best;
        }

        /// <summary>Elimina tutte le task (usato nel reset profilo).</summary>
        public static void EliminaTutte()
        {
            Preferences.Default.Remove(PREF_KEY);
        }
    }
}
