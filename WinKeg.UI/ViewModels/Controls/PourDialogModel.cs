using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WinKeg.Data.Models;

namespace WinKeg.UI.ViewModels.Controls
{
    public class PourDialogModel : INotifyPropertyChanged
    {
        private Keg _keg;
        private Timer timer;
        public PourDialogModel(Keg k)
        {
            _keg = k;
            PourBeer = new RelayCommand(PourBeerPressed);
            timer = new Timer();
            timer.Interval = 5;
            timer.Elapsed += Timer_Elapsed;
        }

        private int pulses = 0;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            pulses++;
            App.m_window.DispatcherQueue.TryEnqueue(() => { PropertyChanged.Invoke(this, new PropertyChangedEventArgs("OzPouredString")); });
        }

        public string BeverageName
        {
            get => _keg.Beverage.Name;
        }

        public double OzPoured
        {
            get => (pulses / (double)_keg.FlowCalibration);
        }

        public string PourButtonText
        {
            get => currentlyPouring ? "Tap to Stop" : "Tap to Pour";
        }

        private bool currentlyPouring = false;
        public RelayCommand PourBeer { get; private set; }
        public async void PourBeerPressed()
        {
            currentlyPouring = !currentlyPouring;
            PropertyChanged(this, new PropertyChangedEventArgs("PourButtonText"));

            if(currentlyPouring)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        public string OzPouredString
        {
            get => OzPoured.ToString("N1");
        }

        public void OnClosing()
        {
            // ensure we stop pouring beer homie!
            timer.Stop();

            // write out the values!
            _keg.CurrentVolume = _keg.CurrentVolume - OzPoured;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
