using Microsoft.UI.Xaml.Media;
using System;
using System.ComponentModel;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.UI.Views;
using WinKeg.UI.Views.Menus;

namespace WinKeg.UI.ViewModels
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        private INavService _navigationService;

        public MenuPageViewModel(INavService navigationService)
        {
            _navigationService = navigationService;
            CloseMenu = new RelayCommand(onCloseClick);
            TemperatureMenu = new RelayCommand(TemperatureButtonTapped);
            UserMenu = new RelayCommand(UserMenuTapped);
            CompressorPower = new RelayCommand(CompressorPowerTapped);
            BeverageMenu = new RelayCommand(BeverageMenuTapped);
            KegMenu = new RelayCommand(KegMenuTapped);

            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                compressorPower = unitOfWork.Settings.GetSettingByName("CompressorPower");
                unitOfWork.Dispose();
            }

            if(compressorPower == null)
            {
                compressorPower = new Setting()
                {
                    Name = "CompressorPower",
                    Value = "false"
                };
            }
            CompressorOn = bool.Parse(compressorPower.Value);
        }

        private void BeverageMenuTapped()
        {
            _navigationService.Navigate(typeof(BeverageAdminView));
        }

        private Setting compressorPower;

        private bool _CompressorOn;
        public bool CompressorOn
        {
            get { return _CompressorOn; }
            set
            {
                _CompressorOn = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CompressorOn"));
                PropertyChanged(this, new PropertyChangedEventArgs("CompressorText"));
                PropertyChanged(this, new PropertyChangedEventArgs("CompressorColor"));
            }
        }

        public string CompressorText
        {
            get { return CompressorOn ? "Turn Compressor Off" : "Turn Compressor On"; }
        }

        // Need a way to reference the staticresource, probably a XAML converter
        public string CompressorColor
        {
            get { return CompressorOn ? "#FF754220" : "#FF000000"; }
        }

        private void CompressorPowerTapped()
        {
            CompressorOn = !CompressorOn;
            compressorPower.Value = CompressorOn.ToString();

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                if(compressorPower.Id == 0)
                {
                    unitOfWork.Settings.Add(compressorPower);
                }
                else
                {
                    unitOfWork.Settings.Edit(compressorPower);
                }
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        private void onCloseClick()
        {
            _navigationService.Navigate(typeof(MainPageView));
        }

        private async void TemperatureButtonTapped()
        {
            Dialogs.TemperatureDialog temperature = new Dialogs.TemperatureDialog();
            temperature.XamlRoot = App.rootFrame.XamlRoot;
            await temperature.ShowAsync();
        }

        public void UserMenuTapped()
        {
            _navigationService.Navigate(typeof(UserAdminView));
        }

        public void KegMenuTapped()
        {
            _navigationService.Navigate(typeof(KegAdminView));
        }

        public RelayCommand CloseMenu { get; private set; }
        public RelayCommand TemperatureMenu { get; private set; }
        public RelayCommand UserMenu { get; private set; }
        public RelayCommand CompressorPower { get; private set; }
        public RelayCommand BeverageMenu { get; private set;}
        public RelayCommand KegMenu { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
