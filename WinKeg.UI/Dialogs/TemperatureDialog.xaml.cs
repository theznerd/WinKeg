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
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinKeg.Data;
using WinKeg.Data.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinKeg.UI.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemperatureDialog : ContentDialog
    {
        public TemperatureDialog()
        {
            this.InitializeComponent();

            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                temperatureSetting  = unitOfWork.Settings.GetSettingByName("SetTemperature");
                unitOfWork.Dispose();
            }
            if(temperatureSetting == null)
            {
                temperatureSetting = new Setting()
                {
                    Name = "SetTemperature",
                    Value = "40"
                };
            }

            int temperature;
            Temp.Value = int.TryParse(temperatureSetting.Value, out temperature) ? temperature : 40;
        }

        private Setting temperatureSetting;

        private void Primary_Click(object sender, RoutedEventArgs e)
        {
            temperatureSetting.Value = Temp.Value.ToString();
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                if(temperatureSetting.Id == 0)
                {
                    unitOfWork.Settings.Add(temperatureSetting);
                }
                else
                {
                    unitOfWork.Settings.Edit(temperatureSetting);
                }
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
            this.Hide();
        }

    }
}
