using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.UI.Views;

namespace WinKeg.UI.ViewModels.Menus
{
    public class KegAdminViewModel : INotifyPropertyChanged
    {
        private readonly INavService _navigationService;

        public KegAdminViewModel(INavService navService)
        {
            _navigationService = navService;

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {

            }

            CloseMenu = new RelayCommand(CloseMenuPressed);
            SetBeverage = new RelayCommand(SetBeveragePressed);
        }

        private ObservableCollection<Keg> _kegs;
        public ObservableCollection<Keg> Kegs
        {
            get => _kegs;
            set
            {
                _kegs = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Kegs"));
            }
        }

        private Keg _selectedKeg;
        public Keg SelectedKeg
        {
            get => _selectedKeg;
            set
            {
                _selectedKeg = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedKeg"));
            }
        }

        public RelayCommand CloseMenu { get; private set; }
        public void CloseMenuPressed()
        {
            _navigationService.Navigate(typeof(MenuPageView));
        }

        public RelayCommand SetBeverage { get; private set; }
        private async void SetBeveragePressed()
        {
            Dialogs.BeverageDialog beverageDialog = new Dialogs.BeverageDialog();
            beverageDialog.XamlRoot = App.rootFrame.XamlRoot;
            await beverageDialog.ShowAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
