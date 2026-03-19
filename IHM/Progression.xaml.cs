using System;
using System.Windows;
using System.Windows.Threading;

namespace Poly4
{
    public partial class Progression : Window
    {
        private DispatcherTimer _timer;
        private int _totalSeconds;
        private int _elapsedSeconds;
        private int _cycles;
        private int _taille;
        private bool _nettoyageCycle;
        private bool _nettoyageSeul;
        private bool _forceClose = false;

        public Progression(int cycles, int taille, bool nettoyageCycle, bool nettoyageSeul, int totalSeconds)
        {
            InitializeComponent();

            _cycles = cycles;
            _taille = taille;
            _nettoyageCycle = nettoyageCycle;
            _nettoyageSeul = nettoyageSeul;
            _totalSeconds = totalSeconds > 0 ? totalSeconds : 60; // fallback 60s

            // Affiche les infos
            InfoCycles.Text = cycles.ToString();
            InfoTaille.Text = $"{taille} mm";
            InfoTempsRestant.Text = FormatTime(_totalSeconds);

            // Démarre le timer (simulation en temps accéléré — 1 tick = 1 sec simulée)
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50); // 50ms = accéléré pour démo
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedSeconds++;

            if (_elapsedSeconds >= _totalSeconds)
            {
                _timer.Stop();
                OnCycleComplete();
                return;
            }

            double globalPct = (_elapsedSeconds / (double)_totalSeconds) * 100.0;
            MainProgressBar.Value = globalPct;
            TxtPourcentage.Text = $"{(int)globalPct}%";

            // Cycle actuel
            int secParCycle = _totalSeconds / (_cycles > 0 ? _cycles : 1);
            int cycleActuel = (_elapsedSeconds / secParCycle) + 1;
            if (cycleActuel > _cycles) cycleActuel = _cycles;
            InfoCycleActuel.Text = cycleActuel.ToString();

            // Progression dans le cycle actuel
            int secDansCycle = _elapsedSeconds % (secParCycle > 0 ? secParCycle : 1);
            double cyclePct = (secDansCycle / (double)secParCycle) * 100.0;
            CycleProgressBar.Value = cyclePct;

            // Temps restant / écoulé
            int restant = _totalSeconds - _elapsedSeconds;
            InfoTempsRestant.Text = FormatTime(restant);
            TxtTempsEcoule.Text = FormatTime(_elapsedSeconds);

            // Phase label
            if (_nettoyageSeul)
            {
                TxtPhase.Text = "Nettoyage en cours...";
            }
            else
            {
                double pct = globalPct;
                if (pct < 10) TxtPhase.Text = "Préchauffage...";
                else if (pct < 80) TxtPhase.Text = $"Polymérisation — Cycle {cycleActuel} sur {_cycles}";
                else if (_nettoyageCycle) TxtPhase.Text = "Nettoyage final...";
                else TxtPhase.Text = "Finalisation...";
            }
        }

        private void OnCycleComplete()
        {
            MainProgressBar.Value = 100;
            CycleProgressBar.Value = 100;
            TxtPourcentage.Text = "100%";
            TxtPhase.Text = "✓ Traitement terminé !";
            InfoTempsRestant.Text = "0 sec";
            BtnArret.IsEnabled = false;

            MessageBox.Show("Le traitement est terminé avec succès.", "Polymérisation terminée",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            ReturnToMenu();
        }

        private void BtnArret_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Voulez-vous vraiment arrêter le cycle en cours ?\nLa progression sera perdue.",
                "Confirmation d'arrêt",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _timer.Stop();
                ReturnToMenu();
            }
        }

        private void ReturnToMenu()
        {
            _forceClose = true;
            if (this.Owner != null)
                this.Owner.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_forceClose)
            {
                e.Cancel = true; // Empêche la fermeture via la croix
            }
            else
            {
                _timer?.Stop();
            }
        }

        private string FormatTime(int seconds)
        {
            if (seconds <= 0) return "0 sec";
            int h = seconds / 3600;
            int m = (seconds % 3600) / 60;
            int s = seconds % 60;
            if (h > 0) return $"{h}h {m:D2}m {s:D2}s";
            if (m > 0) return $"{m}m {s:D2}s";
            return $"{s} sec";
        }
    }
}
