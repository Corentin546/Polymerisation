using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Poly4
{
    public partial class Menu : Window
    {
        private const int DUREE_BASE_CYCLE = 12;
        private const int DUREE_NETTOYAGE = 5;

        public Menu()
        {
            InitializeComponent();
            UpdateEstimation();

            // ── Timer web : scrute C:\Temp\web_cmd.txt chaque seconde ──
            DispatcherTimer timerWeb = new DispatcherTimer();
            timerWeb.Interval = TimeSpan.FromSeconds(1);
            timerWeb.Tick += (s, e) =>
            {
                string path = @"C:\Temp\web_cmd.txt";
                if (!File.Exists(path)) return;

                foreach (var line in File.ReadAllLines(path))
                {
                    if (line.StartsWith("cycles=") && int.TryParse(line.Substring(7), out int cycles))
                        SliderCycles.Value = Math.Max(1, Math.Min(20, cycles));

                    if (line.StartsWith("taille=") && int.TryParse(line.Substring(7), out int taille))
                        SliderTaille.Value = Math.Max(1, Math.Min(30, taille));

                    // nettoyage= none | cycle | seul
                    if (line.StartsWith("nettoyage="))
                    {
                        string val = line.Substring(10).Trim().ToLower();
                        ChkAucunNettoyage.IsChecked = val == "none";
                        ChkNettoyageCycle.IsChecked = val == "cycle";
                        ChkNettoyageSeul.IsChecked = val == "seul";
                    }
                }

                File.Delete(path);
                BtnLancer_Click(null, null);
            };
            timerWeb.Start();
        }

        private void SliderCycles_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtCycles != null)
            {
                TxtCycles.Text = ((int)SliderCycles.Value).ToString();
                UpdateEstimation();
            }
        }

        private void SliderTaille_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtTaille != null)
            {
                TxtTaille.Text = ((int)SliderTaille.Value).ToString();
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

            // Nettoyage seul : durée fixe, cycles ignorés
            if (ChkNettoyageSeul?.IsChecked == true)
            {
                TxtTempsEstime.Text = $"{DUREE_NETTOYAGE} min";
                return;
            }

            int dureeParCycle = DUREE_BASE_CYCLE + (taille / 10);
            int total = cycles * dureeParCycle;

            if (ChkNettoyageCycle?.IsChecked == true)
                total += cycles * DUREE_NETTOYAGE;

            int heures = total / 60;
            int minutes = total % 60;

            TxtTempsEstime.Text = heures > 0
                ? $"{heures}h {minutes:D2}min"
                : $"{total} min";
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

            int dureeParCycle = DUREE_BASE_CYCLE + (taille / 10);
            int totalMin = nettoyageSeul
                ? DUREE_NETTOYAGE
                : cycles * dureeParCycle + (nettoyageCycle ? cycles * DUREE_NETTOYAGE : 0);
            int totalSec = totalMin * 60;

            Progression prog = new Progression(cycles, taille, nettoyageCycle, nettoyageSeul, totalSec);
            prog.Owner = this;
            prog.Show();
            this.Hide();
        }
    }
}
