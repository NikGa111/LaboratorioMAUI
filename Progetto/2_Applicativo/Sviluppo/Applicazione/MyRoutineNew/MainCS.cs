using MyRoutine.Models;
using MyRoutineNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoutineNew
{
    public class MainCS
    {
        // ── Variabili statiche (invariate) ──────────────────
        private static string _nome;
        private static string _cognome;
        private static DateTime _dataOra;
        private static string _tema;
        private static bool _task;
        private static Attivita attivita;
        private static List<Attivita> _attivitaInsieme = new List<Attivita>();
        private static int _attivitaOggi;
        private static int _attivitaOggiCompletate;

        private static List<Badge> _badgeInsieme = new List<Badge>();
        


        private static ImpostazioniClasse _impostazioni;

        private static int _attivitaMese;        
        private static int _attivitaMeseCompletate;
        private static int _attivitaMesePrecedente;
        private static int _attivitaMeseCompletatePrecedente;     


        //da implementare
        private static int _streak;//almeno un attivita al giorno
        private static int _recordStreak;        
        
        // ── Nuova variabile: data di nascita ────────────────
        private static DateTime _dataNascita;





        //implementazioni future
        //
        //giorno della settimana
        //ora del giorno



        // ── Getter/Setter esistenti (invariati) ─────────────
        public static void AddAttivita(Attivita newAttivita)
        {
            _attivitaInsieme.Add(newAttivita);
        }
        public static void RemoveAttivita(Attivita newAttivita)
        {
            _attivitaInsieme.Remove(newAttivita);
        }

        public static void UpdateAttivita(Attivita newAttivita)
        {
            try
            {
                Attivita oldAttivita = _attivitaInsieme.Find(currentAttivita => currentAttivita.DateTimeInizio == newAttivita.DateTimeInizio);
                RemoveAttivita(oldAttivita);
                AddAttivita(newAttivita);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<Attivita> GetAllAttivita()
        {
            return _attivitaInsieme;
        }

        public static Attivita GetAttivita(int index)
        {
            return _attivitaInsieme.ElementAt(index);
        }
        //metodi per insieme badge
        public static void AddBadge(Badge newBadge)
        {
            _badgeInsieme.Add(newBadge);
        }

        public static void RemoveBadge(Badge newBadge)
        {
            _badgeInsieme.Remove(newBadge);
        }

        public static void UpdateBadge(Badge newBadge)
        {
            try
            {
                Badge oldBadge = _badgeInsieme.Find(currentBadge => currentBadge.Nome == newBadge.Nome);
                RemoveBadge(oldBadge);
                AddBadge(newBadge);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<Badge> GetAllBadge()
        {
            return _badgeInsieme;
        }

        public static Badge GetBadge(int index)
        {
            return _badgeInsieme.ElementAt(index);
        }
        //metodi per badge counter
        public static int BadgeGuadagnati()
        {
            return _badgeInsieme.Where(x => x.Ottenuto).Count();
        }

        public static int BadgeMancanti()
        {
            return _badgeInsieme.Count - BadgeGuadagnati();
        }
        //metodi per statistiche
        public static int DeltaMese()
        {
            return _attivitaMeseCompletate - _attivitaMeseCompletatePrecedente ;
        }

        public static int CompletamentoPct()
        {
            try
            {
                return (_attivitaMeseCompletate / _attivitaMese) * 100;
            }catch  (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
        public static int DeltaCompletamento()
        {
            try
            {
                return CompletamentoPct() - (_attivitaMeseCompletatePrecedente / _attivitaMesePrecedente) * 100;
            }catch  (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public static int TotaleAttivitaCompletate()
        {
            return _attivitaInsieme.Where(x => x.Completata).Count();
        }
        

        //metodi getter/setter

        public static string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public static string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }
        public static DateTime DataOra
        {
            get { return _dataOra; }
            set { _dataOra = value; }
        }
        public static string Tema
        {
            get { return _tema; }
            set { _tema = value; }
        }
        public static bool Task
        {
            get { return _task; }
            set { _task = value; }
        }
        public static Attivita Attivita
        {
            get { return attivita; }
            set { attivita = value; }
        }
        public static int AttivitaOggi
        {
            get { return _attivitaOggi; }
            set { _attivitaOggi = value; }
        }
        public static int AttivitaOggiCompletate
        {
            get { return _attivitaOggiCompletate; }
            set { _attivitaOggiCompletate = value; }
        }
        public static int AttivitaMese
        {
            get { return _attivitaMese; }
            set { _attivitaMese = value; }
        }
        public static int AttivitaMesePrecedente
        {
            get { return _attivitaMesePrecedente; }
            set { _attivitaMesePrecedente = value; }
        }
        public static int AttivitaMeseCompletate
        {
            get { return _attivitaMeseCompletate; }
            set { _attivitaMeseCompletate = value; }
        }
        public static int AttivitaMeseCompletatePrecedente
        {
            get { return _attivitaMeseCompletatePrecedente; }
            set { _attivitaMeseCompletatePrecedente = value; }
        }
        public static int Streak
        {
            get { return _streak; }
            set { _streak = value; }
        }
        public static int RecordStreak
        {
            get { return _recordStreak; }
            set { _recordStreak = value; }
        }
        public static ImpostazioniClasse Impostazioni
        {
            get { return _impostazioni; }
            set { _impostazioni = value; }
        }

        // ── Nuovo getter/setter: data di nascita ────────────
        public static DateTime DataNascita
        {
            get { return _dataNascita; }
            set { _dataNascita = value; }
        }

        // ── Metodi di navigazione (invariati) ───────────────
        public static async void PerImpostazioni(INavigation navigation)
        {
            await navigation.PushAsync(new Impostazioni());
        }
        public static async void PerProfilo(INavigation navigation)
        {
            await navigation.PushAsync(new Profilo());
        }
        public static async void PerRoutine(INavigation navigation)
        {
            await navigation.PushAsync(new Routine());
        }
        public static async void PerStatistiche(INavigation navigation)
        {
            await navigation.PushAsync(new Statistiche());
        }

        // ── Nuovi metodi: salva/carica dati utente ──────────

        /// <summary>
        /// Salva nome, cognome e data di nascita nelle Preferences.
        /// Chiama questo metodo al termine dell'onboarding.
        /// </summary>
        public static void SalvaDatiUtente()
        {
            Preferences.Default.Set("nome",          _nome    ?? "");
            Preferences.Default.Set("cognome",        _cognome ?? "");
            Preferences.Default.Set("data_nascita",   _dataNascita.ToString("O"));
            Preferences.Default.Set("setup_ok",       true);
        }

        /// <summary>
        /// Carica nome, cognome e data di nascita dalle Preferences nelle
        /// variabili statiche. Chiama questo all'avvio dell'app se setup_ok == true.
        /// </summary>
        public static void CaricaDatiUtente()
        {
            _nome    = Preferences.Default.Get("nome",    "");
            _cognome = Preferences.Default.Get("cognome", "");

            string dataStr = Preferences.Default.Get("data_nascita", "");
            if (DateTime.TryParse(dataStr, null,
                System.Globalization.DateTimeStyles.RoundtripKind, out var d))
                _dataNascita = d;
        }

        /// <summary>
        /// Restituisce true se l'onboarding è già stato completato.
        /// </summary>
        public static bool SetupCompletato =>
            Preferences.Default.Get("setup_ok", false);

        /// <summary>
        /// Azzera tutti i dati utente e le task (usato in "Riconfigura").
        /// </summary>
        public static void ResetCompleto()
        {
            _nome         = null;
            _cognome      = null;
            _dataNascita  = default;
            Preferences.Default.Remove("nome");
            Preferences.Default.Remove("cognome");
            Preferences.Default.Remove("data_nascita");
            Preferences.Default.Remove("setup_ok");
            TaskManager.EliminaTutte();
        }
    }
}
