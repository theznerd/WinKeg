using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using WinKeg.Views;

namespace WinKeg.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private INavService _navigationService;

        public MainPageViewModel(INavService navigationService)
        {
            _navigationService = navigationService;
            OpenMenu = new RelayCommand(onLogoClick);
        }

        private async void onLogoClick()
        {
            Dialogs.PasscodeDialog adminPasscode = new Dialogs.PasscodeDialog(true);
            await adminPasscode.ShowAsync();

            var result = adminPasscode.Result;
            if(result == Dialogs.PasscodeResult.SignInOK)
                _navigationService.NavigateWithAnimation(typeof(MenuPageView), new Windows.UI.Xaml.Media.Animation.SlideNavigationTransitionInfo() { Effect = Windows.UI.Xaml.Media.Animation.SlideNavigationTransitionEffect.FromBottom });
        }

        public RelayCommand OpenMenu { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
