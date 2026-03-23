using System;
using System.Windows;

namespace Poly4
{
    public partial class Menu : Window
    {
        // Durée de base par cycle en minutes (à adapter selon vos besoins)
        private const int DUREE_BASE_CYCLE = 12;
        private const int DUREE_NETTOYAGE = 5;

        public Menu()
        {
            InitializeComponent();
            UpdateEstimation();
        }

        private void SliderCycles_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtCycles != null)
            {
                int val = (int)SliderCycles.Value;
                TxtCycles.Text = val.ToString();
                UpdateEstimation();
            }
        }
         // TEST sauvegarde
        private void SliderTaille_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtTaille != null)
            {
                int val = (int)SliderTaille.Value;
                TxtTaille.Text = val.ToString();
                UpdateEstimation();
            }
        }

        private void Options_Changed(object sender, RoutedEventArgs e)
        {
            UpdateEstimation();
        }

        private void UpdateEstimation()
        {
            if (TxtTempsEstime == null) return;

            int cycles = (int)SliderCycles.Value;
            int taille = (int)SliderTaille.Value;

            // Calcul basique : cycles * durée de base + facteur taille
            int dureeParCycle = DUREE_BASE_CYCLE + (taille / 5);
            int total = cycles * dureeParCycle;

            // Nettoyage fin de cycle
            if (ChkNettoyageCycle != null && ChkNettoyageCycle.IsChecked == true)
                total += cycles * DUREE_NETTOYAGE;

            // Nettoyage seul (remplace tout)
            if (ChkNettoyageSeul != null && ChkNettoyageSeul.IsChecked == true)
            {
                total = DUREE_NETTOYAGE;
                TxtTempsEstime.Text = $"{total} min";
                return;
            }

            int heures = total / 60;
            int minutes = total % 60;

            if (heures > 0)
                TxtTempsEstime.Text = $"{heures}h {minutes:D2}min";
            else
                TxtTempsEstime.Text = $"{total} min";
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Parametres param = new Parametres();
            param.Owner = this;
            param.ShowDialog();
        }

        private void BtnLancer_Click(object sender, RoutedEventArgs e)
        {
            int cycles = (int)SliderCycles.Value;
            int taille = (int)SliderTaille.Value;
            bool nettoyageCycle = ChkNettoyageCycle.IsChecked == true;
            bool nettoyageSeul = ChkNettoyageSeul.IsChecked == true;

            // Calcul durée totale en secondes pour la progression
            int dureeParCycle = DUREE_BASE_CYCLE + (taille / 5);
            int totalMin = nettoyageSeul ? DUREE_NETTOYAGE : cycles * dureeParCycle + (nettoyageCycle ? cycles * DUREE_NETTOYAGE : 0);
            int totalSec = totalMin * 60;

            Progression prog = new Progression(cycles, taille, nettoyageCycle, nettoyageSeul, totalSec);
            prog.Owner = this;
            prog.Show();
            this.Hide();
        }
    }
}
