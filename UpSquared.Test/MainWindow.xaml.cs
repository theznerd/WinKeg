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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UpSquared.Test
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        BMP180 thermometer;
        Timer timer;

        public MainWindow()
        {
            this.InitializeComponent();
            thermometer = new BMP180("0");
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            double? temp = await thermometer.ReadTemperatureAsync();
            DispatcherQueue.TryEnqueue(() => 
                {
                    TempBlock.Text = temp.ToString();
                    TempBlockF.Text = string.Format("{0:F1}",(temp * 1.8)+32);
                });
            
        }
    }
}
