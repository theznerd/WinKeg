using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;

namespace WinKeg.UI.ViewModels.Controls
{
    public class BeverageDialogModel : INotifyPropertyChanged
    {
        public BeverageDialogModel()
        {
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                Beverages = unitOfWork.Beverages.GetAllWithImages().ToList();
                unitOfWork.Dispose();
            }
        }

        private List<Beverage> _beverages;
        public List<Beverage> Beverages
        {
            get => _beverages;
            set
            {
                _beverages = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Beverages"));
            }
        }

        private Beverage _selectedBeverage;
        public Beverage SelectedBeverage
        {
            get => _selectedBeverage;
            set
            {
                _selectedBeverage = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedBeverage"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
