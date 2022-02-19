using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinKeg.Hardware.Thermometers;
using WinKeg.Hardware.PowerMeters;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UpSquared.Test
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        BMP280IoT thermometer;
        SCT013020 powerMeter;
        Timer timer;

        public MainWindow()
        {
            this.InitializeComponent();
            thermometer = new BMP280IoT("0");
            powerMeter = new SCT013020("ADS1015;0;72");
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task<double?> temp = thermometer.ReadTemperatureAsync();
            double? watts = await powerMeter.GetCurrentWattageAsync();
            double? tempR = temp.GetAwaiter().GetResult();

            DispatcherQueue.TryEnqueue(() => 
                {
                    TempBlock.Text = tempR.ToString();
                    TempBlockF.Text = string.Format("{0:F1}",(tempR * 1.8)+32);
                    WattageBlock.Text = watts.ToString();
                });
            
        }
    }
}
