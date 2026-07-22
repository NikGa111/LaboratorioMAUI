

namespace AppConvertitore
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnConverti_Clicked(object sender, EventArgs e)
        {

            try
            {
                string valoreImporto = entConversione.Text;

                double franchi = double.Parse(valoreImporto);

                // lblRisultato.Text = (franchi * 1.07).ToString("F2");

                lblRisultato.TextColor = Colors.Green;
                lblRisultato.Text = $"{(franchi * 1.07):0.00}";

                // lblRisultato.Text = String.Format("Risultato: {0}", franchi * 1.07).ToString("F2"));

                // double valConverted = franchi * 1.07;
                // lblRisultato.Text = "Risultato: " + valConverted;

                // oppure: lblRisultato.Text = (valConverted * 1.07).toString();
            }
            catch (ArgumentNullException aex)
            {
                lblRisultato.TextColor = Colors.Red;
                lblRisultato.Text = "Perfavore inserisci qualcosa nel campo importo CHF";
                // DisplayAlert("ArgumentNullException", "Perfavore inserisci qualcosa nel campo importo CHF", "OK");
            }
            catch (FormatException fex)
            {
                lblRisultato.TextColor = Colors.Red;
                lblRisultato.Text = "Perfavore inserisci un numero valido nel campo imprto CHF";
                // DisplayAlert("FormatException", "Perfavore inserisci un numero valido nel campo imprto CHF", "OK");
            }
            catch (OverflowException oex)
            {
                lblRisultato.TextColor = Colors.Red;
                lblRisultato.Text = "Perfavore inserisci un double valido nel campo imprto CHF";
                // DisplayAlert("OverflowException", "Perfavore inserisci un double valido nel campo imprto CHF", "OK");
            }

        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblRisultato.TextColor = Colors.Black;
            lblRisultato.Text = "Pronto per convertire...";
            entConversione.Text = string.Empty;
        }
    }

}
