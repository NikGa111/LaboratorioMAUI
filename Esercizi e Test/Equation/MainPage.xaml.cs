using Microsoft.Extensions.Options;

namespace Equation
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalcolaClicked(object sender, EventArgs e)
        {

                double valoreA;
                double valoreB;
                double valoreC;
                double delta;
                double x_1;
                double x_2;


            if (string.IsNullOrWhiteSpace(EntA?.Text) || string.IsNullOrWhiteSpace(EntB?.Text) || string.IsNullOrWhiteSpace(EntC?.Text))
            {
                LblRisultato.TextColor = Colors.Orange;
                LblRisultato.Text = "Hai inserito uno o più campi nulli.";
                return;
            }
            else
            {
                try
                {
                    string testoA = EntA.Text;
                    valoreA = double.Parse(testoA);
                    string testoB = EntB.Text;
                    valoreB = double.Parse(testoB);
                    string testoC = EntC.Text;
                    valoreC = double.Parse(testoC);
                    delta = Math.Pow(valoreB, 2) - (4 * valoreA * valoreC);

                    if (delta > 0) 
                    {
                        x_1 = (-valoreB + Math.Sqrt(delta) / (2 * valoreA));
                        x_2 = (-valoreB - Math.Sqrt(delta) / (2 * valoreA));

                        LblRisultato.Text = $"Due soluzioni reali:\nx1 = {x_1:F2}\nx2 = {x_2:F2}";
                        LblRisultato.TextColor = Colors.Green;
                    }else if (delta == 0) 
                    {
                        x_1 = -valoreB / (2 * valoreA);
                        LblRisultato.Text = $"Una soluzione reale coincidente:\nx = {x_1:F2}";
                        LblRisultato.TextColor = Colors.Blue;
                    }else if(delta < 0) 
                    {
                        LblRisultato.Text = "Nessuna soluzione reale";
                        LblRisultato.TextColor = Colors.Red;
                    }
                    else
                    {
                        LblRisultato.Text = "CHE CAZZO SUCCEDE ";
                        LblRisultato.TextColor = Colors.Red;
                    }

                }
                catch (FormatException)
                {
                    LblRisultato.TextColor = Colors.Red;
                    LblRisultato.Text = "Uno o più campi non sono numeri validi.";
                    return;
                }
                catch (OverflowException)
                {
                    LblRisultato.TextColor = Colors.Red;
                    LblRisultato.Text = "Uno o più campi hanno un valore troppo grande.";
                    return;
                }
                if (valoreA == 0)
                {
                    LblRisultato.TextColor = Colors.Orange;
                    LblRisultato.Text = "Il valore a è 0. Equazione non di secondo grado.";
                }

            }







        }
    }

}
