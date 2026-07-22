using System;

namespace MyRoutineNew.Models
{
    public class Attivita
    {
        private string _nome;
        private string _descrizione;
        private DateTime _dateTimeInizio;
        private DateTime _dateTimeFine;
        private string _categoria;
        private bool _completata;

        // ── Proprietà esistenti (invariate) ─────────────────
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string Descrizione
        {
            get { return _descrizione; }
            set { _descrizione = value; }
        }
        public DateTime DateTimeInizio
        {
            get { return _dateTimeInizio; }
            set { _dateTimeInizio = value; }
        }
        public DateTime DateTimeFine
        {
            get { return _dateTimeFine; }
            set { _dateTimeFine = value; }
        }
        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }
        public bool Completata
        {
            get { return _completata; }
            set { _completata = value; }
        }


        // ── Nuove proprietà aggiunte ─────────────────────────
        public string Id          { get; set; }
        

        // Costruttore senza parametri (necessario per JSON)
        public Attivita()
        {
            Id         = Guid.NewGuid().ToString();
            Completata = false;
        }
    }
}
