namespace PizzaApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        
        private void listFrutti_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Dichiara e inizializza una variabile locale di tipo Frutto che contiene l'oggetto selezionato
            //(Frutto) permette di fare un Casting dal tipo Object al tipo Frutto
            Frutto selectedFruit = (Frutto)listFrutti.SelectedItem;

            //Popola l'Entry con il nome del frutto selezionato. Richiama la proprietà Nome dell'oggetto
            lbFrutto.Text = selectedFruit.Nome;
        }

    } 
}
