using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Models
{
    internal class Spesa : VoceBase
    {
        public float Importo { get; set; }
        public int Quantita { get; set; }

        public Spesa(string descrizione, float importo, int quantita = 1)
        {
            Descrizione = descrizione;
            Importo = importo;
            Quantita = quantita;
        }


        public override string ToRiga()
        {
            return $"{Descrizione},{Importo},{Quantita}";
        }


        public static Spesa? FromRiga(string riga)
        {
            if (string.IsNullOrWhiteSpace(riga))
                return null;

            var parti = riga.Split(',');
            if (parti.Length < 2)
                return null;

            try
            {
                string descrizione = parti[0].Trim();
                float importo = float.Parse(parti[1].Trim());
                int quantita = 1;
                if (parti.Length >= 3)
                    int.TryParse(parti[2].Trim(), out quantita);

                return new Spesa(descrizione, importo, quantita);
            }
            catch
            {
                return null;
            }
        }
    }
}

