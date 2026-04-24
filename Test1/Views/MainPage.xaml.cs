using Test1.Models;

namespace Test1
{
    public partial class MainPage : ContentPage
    {

        private readonly string _fileIndice;

        public MainPage()
        {
            InitializeComponent();

            _fileIndice = Path.Combine(FileSystem.AppDataDirectory, "indice.txt");


        }


        private async void OnSalvaClicked(object sender, EventArgs e)
        {
            string nomeLista = EntryLista.Text;
            string descrizione = EntryDescrizione.Text;
            string importoTesto = EntryImporto.Text;
            string quantitaTesto = EntryQuantita.Text;


            if (string.IsNullOrEmpty(nomeLista) || string.IsNullOrEmpty(descrizione))
            {
                await DisplayAlert("Errore", "Il nome della lista e la descrizione non possono essere vuoti.", "OK");
                return;
            }

            float importo = 0;
            if (!string.IsNullOrEmpty(importoTesto))
            {
                if (!float.TryParse(importoTesto, out importo)) // lo avevo nel manoscritto non so cosa sia :) (parlo di out)
                {
                    await DisplayAlert("Errore", "L'importo inserito non è valido.", "OK");
                    return;
                }
            }


            string nomeFile = $"{nomeLista}.txt";
            VediListe.Text = $"{nomeFile} \n";
            string percorsoLista = Path.Combine(FileSystem.AppDataDirectory, nomeFile);


            var spesa = new Spesa(descrizione, importo);
            string riga = spesa.ToRiga();


            await File.AppendAllTextAsync(percorsoLista, riga + Environment.NewLine);



            EntryDescrizione.Text = "";
            EntryImporto.Text = "";
            EntryQuantita.Text = "";

            await DisplayAlert("Salvato", $"Spesa aggiunta alla lista: {nomeLista}.", "OK");
            
        }

        private async void OnVediClicked(object sender, EventArgs e)
        {
            string nomeLista = EntryLista.Text;
            if (string.IsNullOrEmpty(nomeLista))
            {
                await DisplayAlert("Errore", "Il nome della lista non può essere vuoto.", "OK");
                return;
            }
            string nomeFile = $"{nomeLista}.txt";
            string percorsoLista = Path.Combine(FileSystem.AppDataDirectory, nomeFile);
            if (!File.Exists(percorsoLista))
            {
                await DisplayAlert("Errore", $"La lista '{nomeLista}' non esiste.", "OK");
                return;
            }
            var dettaglioPage = new DettaglioPage(percorsoLista);
            await Navigation.PushAsync(dettaglioPage);
        }

    }
}
