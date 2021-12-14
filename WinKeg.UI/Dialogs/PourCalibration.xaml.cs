using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Timers;
using WinKeg.Data.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinKeg.UI.Dialogs
{
    public sealed partial class PourCalibration : ContentDialog
    {
        private Keg _keg;
        private int pourCounter;
        private Timer timer;
        private bool pouring = false;

        public PourCalibration(Keg keg)
        {
            this.InitializeComponent();
            _keg = keg;
            timer = new Timer(5);
            pourCounter = 0;
            timer.Elapsed += Timer_Elapsed;
            KegName.Text = _keg.Name;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            pourCounter++;
        }

        private void Primary_Click(object sender, RoutedEventArgs e)
        {
            _keg.FlowCalibration = (int)System.Math.Ceiling((double)(pourCounter / int.Parse(measuredOz.Text))); // pulses per oz
            this.Hide();
        }

        private void Secondary_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Pour_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            pouring = !pouring;
            PourButton.Content = pouring ? "Tap to Stop" : "Tap to Pour";
            if(pouring)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }
    }
}
