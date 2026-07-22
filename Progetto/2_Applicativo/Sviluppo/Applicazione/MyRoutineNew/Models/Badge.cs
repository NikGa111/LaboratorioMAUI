using System;

namespace MyRoutine.Models
{
    public class Badge
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public bool Ottenuto { get; set; }
        public int Progress { get; set; }
        public int Goal { get; set; }
        public string Emoji { get; set; }

        public double Percentuale => Goal <= 0 ? 0 : Math.Min(1, (double)Progress / Goal);

        public Badge() {}

        public Badge(string nome, string descrizione, int goal, string emoji)
        {
            Nome = nome;
            Descrizione = descrizione;
            Goal = goal;
            Emoji = emoji;
            Progress = 0;
            Ottenuto = false;
        }

        public void AggiornaProgress(int value)
        {
            Progress = Math.Min(value, Goal);
            Ottenuto = Progress >= Goal;
        }
    }
}
