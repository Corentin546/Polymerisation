using System.Windows;

namespace Poly4
{
    public partial class Parametres : Window
    {
        public Parametres()
        {
            InitializeComponent();
        }

        private void SliderDureeBase_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtDureeBase != null)
                TxtDureeBase.Text = $"{(int)SliderDureeBase.Value} min";
        }

        private void SliderDureeNettoyage_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtDureeNettoyage != null)
                TxtDureeNettoyage.Text = $"{(int)SliderDureeNettoyage.Value} min";
        }

        private void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
