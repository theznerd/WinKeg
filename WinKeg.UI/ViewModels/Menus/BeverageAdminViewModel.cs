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
    public class BeverageAdminViewModel : INotifyPropertyChanged
    {
        private readonly INavService _navService;
        public BeverageAdminViewModel(INavService nav)
        {
            _navService = nav;

            // Load the beverage data
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                Beverages = new ObservableCollection<Beverage>(unitOfWork.Beverages.GetAllWithImages());
                CurrentlyUsedBeverages = new Collection<Beverage>(unitOfWork.Kegs.GetCurrentBeveragesFromAllKegs().ToList());
                unitOfWork.Dispose();
            }

            // Wire up controls
            AddBeverage = new RelayCommand(AddBeveragePressed);
            DeleteBeverage = new RelayCommand(DeleteBeveragePressed, CanDeleteBeverage);
            SaveBeverage = new RelayCommand(SaveBeveragePressed, CanSaveBeverage);
            SetPicture = new RelayCommand(SetPicturePressed, CanSetPicture);
            CloseMenu = new RelayCommand(CloseMenuPressed);
        }

        private Collection<Beverage> CurrentlyUsedBeverages{ get; set; }

        private ObservableCollection<Beverage> _beverages;
        public ObservableCollection<Beverage> Beverages
        {
            get => _beverages;
            set
            {
                _beverages = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Beverages"));
            }
        }

        private Beverage _selectedBeverage;
        public Beverage SelectedBeverage
        {
            get => _selectedBeverage;
            set
            {
                _selectedBeverage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBeverage"));
                SetPicture.RaiseCanExecuteChanged();
                SaveBeverage.RaiseCanExecuteChanged();
                DeleteBeverage.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CloseMenu { get; private set; }
        private void CloseMenuPressed()
        {
            _navService.Navigate(typeof(MenuPageView));
        }

        public RelayCommand SetPicture { get; private set; }
        private async void SetPicturePressed()
        {
            Dialogs.BeverageImageDialog imageDialog = new Dialogs.BeverageImageDialog();
            imageDialog.XamlRoot = App.rootFrame.XamlRoot;
            await imageDialog.ShowAsync();

            if(null != imageDialog.selectedBeverageImage)
            {
                SelectedBeverage.Image = imageDialog.selectedBeverageImage;
            }
        }
        private bool CanSetPicture()
        {
            return SelectedBeverage != null;
        }

        public RelayCommand SaveBeverage { get; private set; }
        private void SaveBeveragePressed()
        {
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                BeverageImage bi = null;
                if (SelectedBeverage.Id == 0)
                {
                    bi = unitOfWork.BeverageImages.GetById(SelectedBeverage.Image.Id);
                    SelectedBeverage.Image = null;
                    unitOfWork.Beverages.Add(SelectedBeverage);
                }
                if (null != bi) { SelectedBeverage.Image = bi; }
                unitOfWork.Beverages.Edit(SelectedBeverage);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }
        private bool CanSaveBeverage()
        {
            return SelectedBeverage != null;
        }

        public RelayCommand DeleteBeverage { get; private set; }
        private void DeleteBeveragePressed()
        {
            if(SelectedBeverage.Id != 0)
            {
                using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    unitOfWork.Beverages.Delete(SelectedBeverage);
                    unitOfWork.Complete();
                    unitOfWork.Dispose();
                }
            }
            Beverages.Remove(SelectedBeverage);
            SelectedBeverage = null;
        }
        private bool CanDeleteBeverage()
        {
            if(SelectedBeverage == null)
                return false;

            if(CurrentlyUsedBeverages.Contains(SelectedBeverage))
            {
                return false;
            }
            return true;
        }

        
        public RelayCommand AddBeverage { get; private set; }
        private void AddBeveragePressed()
        {
            Beverage beverage = new Beverage();
            Beverages.Add(beverage);
            PropertyChanged(this, new PropertyChangedEventArgs("Beverages"));
            SelectedBeverage = beverage;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
