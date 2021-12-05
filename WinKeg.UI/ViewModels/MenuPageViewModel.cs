using System;
using WinKeg.UI.Views;
using WinKeg.UI.Views.Menus;

namespace WinKeg.UI.ViewModels
{
    public class MenuPageViewModel
    {
        private INavService _navigationService;

        public MenuPageViewModel(INavService navigationService)
        {
            _navigationService = navigationService;
            CloseMenu = new RelayCommand(onCloseClick);
            TemperatureMenu = new RelayCommand(TemperatureButtonTapped);
            UserMenu = new RelayCommand(UserMenuTapped);
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

        public RelayCommand CloseMenu { get; private set; }
        public RelayCommand TemperatureMenu { get; private set; }
        public RelayCommand UserMenu { get; private set; }
    }
}
