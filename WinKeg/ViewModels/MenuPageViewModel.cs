using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Views;
using WinKeg.Views.Menus;

namespace WinKeg.ViewModels
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
