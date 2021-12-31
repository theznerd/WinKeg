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
                Kegs = new ObservableCollection<Keg>(unitOfWork.Kegs.GetAllWithBeverageAndCurrentHistory());
                unitOfWork.Dispose();
            }

            CloseMenu = new RelayCommand(CloseMenuPressed);
            SetBeverage = new RelayCommand(SetBeveragePressed, CanSetBeverage);
            CalibratePour = new RelayCommand(CalibratePourPressed, CanCalibratePour);
            SaveKeg = new RelayCommand(SaveKegPressed, CanSaveKeg);
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
                SetBeverage.RaiseCanExecuteChanged();
                CalibratePour.RaiseCanExecuteChanged();
                SaveKeg.RaiseCanExecuteChanged();
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
            if(null != beverageDialog.selectedBeverage)
            {
                SelectedKeg.Beverage = beverageDialog.selectedBeverage;
                SelectedKeg.BeverageId = SelectedKeg.Beverage.Id;
            }
            KegHistory kegHistory = new KegHistory()
            {
                KegID = SelectedKeg.Id,
                BeverageID = SelectedKeg.Beverage.Id
            };
            SelectedKeg.CurrentHistory = kegHistory;
        }
        public bool CanSetBeverage()
        {
            return null != SelectedKeg;
        }

        public RelayCommand CalibratePour { get;private set; }
        public async void CalibratePourPressed()
        {
            Dialogs.PourCalibration pourCalibration = new Dialogs.PourCalibration(SelectedKeg);
            pourCalibration.XamlRoot = App.rootFrame.XamlRoot;
            await pourCalibration.ShowAsync();
        }
        public bool CanCalibratePour()
        {
            return null != SelectedKeg;
        }

        public RelayCommand SaveKeg { get; private set; }
        public void SaveKegPressed()
        {
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                if(SelectedKeg.CurrentHistory.Id == 0)
                {
                    unitOfWork.KegHistories.Add(SelectedKeg.CurrentHistory);
                }
                unitOfWork.Kegs.Edit(SelectedKeg);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }
        public bool CanSaveKeg()
        {
            return null != SelectedKeg;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
