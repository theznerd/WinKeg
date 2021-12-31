using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.UI.Views;

namespace WinKeg.UI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private INavService _navigationService;

        public MainPageViewModel(INavService navigationService)
        {
            _navigationService = navigationService;
            OpenMenu = new RelayCommand(onLogoClick);
            PourBeverage = new RelayCommand<Keg>(PourBeveragePressed, CanPourBeverage);

            // load data!
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                var kegs = unitOfWork.Kegs.GetAllWithBeverageAndCurrentHistory();
                Kegs = kegs.ToList();
                _kegerator = unitOfWork.Kegerator.GetById(1);
            }
        }

        private Kegerator _kegerator;

        public string KegeratorName
        {
            get { return _kegerator.Name; }
        }

        public RelayCommand<Keg> PourBeverage { get; private set; }
        private async void PourBeveragePressed(Keg k)
        {
            Dialogs.PourDialog pourDialog;
            if (k.Beverage.IsRestricted)
            {
                Dialogs.PasscodeDialog restrictPasscode = new Dialogs.PasscodeDialog(Dialogs.SigninType.BeverageRestriction);
                restrictPasscode.XamlRoot = App.rootFrame.XamlRoot;
                await restrictPasscode.ShowAsync();

                var result = restrictPasscode.Result;
                if (result != Dialogs.PasscodeResult.SignInOK)
                    return;
                pourDialog = new Dialogs.PourDialog(k, restrictPasscode.UserId);
            }
            else
            {
                pourDialog = new Dialogs.PourDialog(k);
            }
            pourDialog.XamlRoot = App.rootFrame.XamlRoot;
            await pourDialog.ShowAsync();
        }
        private bool CanPourBeverage(Keg k)
        {
            return null != k.Beverage;
        }

        public RelayCommand OpenMenu { get; private set; }
        private async void onLogoClick()
        {
            Dialogs.PasscodeDialog adminPasscode = new Dialogs.PasscodeDialog(Dialogs.SigninType.Admin);
            adminPasscode.XamlRoot = App.rootFrame.XamlRoot;
            await adminPasscode.ShowAsync();

            var result = adminPasscode.Result;
            if(result == Dialogs.PasscodeResult.SignInOK)
                _navigationService.NavigateWithAnimation(typeof(MenuPageView), new Microsoft.UI.Xaml.Media.Animation.SlideNavigationTransitionInfo() { Effect = Microsoft.UI.Xaml.Media.Animation.SlideNavigationTransitionEffect.FromBottom });
        }

        private List<Keg> _kegs;
        public List<Keg> Kegs
        {
            get => _kegs;
            set
            {
                _kegs = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Kegs"));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
