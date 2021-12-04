using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Data.Models
{
    public class Kegerator : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Owner"));                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if(_location != value)
                {
                    _location = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }

        private IList<Hardware> _kegeratorHardware;
        public IList<Hardware> KegeratorHardware
        {
            get => _kegeratorHardware;
            set
            {
                if(_kegeratorHardware != value)
                {
                    _kegeratorHardware = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("KegeratorHardware"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
