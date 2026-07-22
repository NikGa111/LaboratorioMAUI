using Test1.Models;

namespace Test1;

public partial class DettaglioPage : ContentPage
{
        public DettaglioPage(string percorsoFile)
        {
            InitializeComponent();

            CaricaSpese(percorsoFile);
        }

        private void CaricaSpese(string percorsoFile)
        {
        
            if (!File.Exists(percorsoFile))
            {
                VediSpese.Text = "Dentro non ce sta nulla dentro sta lista.\nSalva almeno una spesa prima di visualizzarla.";
                return;
            }

            var righe = File.ReadAllLines(percorsoFile);
            

            var spese = righe
                .Select(r => Spesa.FromRiga(r))
                .Where(s => s != null)
                .Cast<Spesa>()
                .ToList();

        //Messo anche questo nel manoscritto, l'ho cercato con l'AI ieri sera, serve per filtrare le righe che non sono spese valide,
        //se ci sono righe vuote o malformate, così non si blocca tutto e mostra solo quelle valide, se ce ne sono
        //infine le casta in una lista

        if (spese.Count == 0)
            {
                VediSpese.Text = "Nun ce stanno spese valide nel file.";
                return;
            }

            
            var sb = new System.Text.StringBuilder();

            LabelNomeLista.Text = $"Lista: {Path.GetFileNameWithoutExtension(percorsoFile)}"; // Prende il nome del file senza estensione per mostrarlo come titolo della lista

        foreach (var spesa in spese)
            {
                sb.AppendLine($"DESCRIZIONE: {spesa.Descrizione}");
                sb.AppendLine($"IMPORTO:     {spesa.Importo:F2}");
                if (spesa.Quantita >= 1)
                    sb.AppendLine($"QUANTITÀ:    {spesa.Quantita}");
                sb.AppendLine("------------------");
            }

            
            float totale = spese.Sum(s => s.Importo * s.Quantita);
            sb.AppendLine();
            sb.AppendLine($"TOTALE: {totale:F2} €");

            VediSpese.Text = sb.ToString();
        }

        private async void OnTornaClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
       }
}