using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class Keg : INotifyPropertyChanged
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

        private double _initialVolume;
        public double InitialVolume
        {
            get => _initialVolume;
            set
            {
                if(_initialVolume != value)
                {
                    _initialVolume = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("InitialVolume"));
                }
            }
        }

        private double _currentVolume;
        public double CurrentVolume
        {
            get => _currentVolume;
            set
            {
                if(_currentVolume != value)
                {
                    _currentVolume = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentVolume"));
                }
            }
        }

        private int _flowCalibration;
        public int FlowCalibration
        {
            get => _flowCalibration;
            set
            {
                if(_flowCalibration != value)
                {
                    _flowCalibration = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FlowCalibration"));
                }
            }
        }

        private Beverage _beverage;
        public Beverage Beverage
        {
            get => _beverage;
            set
            {
                if(_beverage != value)
                {
                    _beverage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Beverage"));
                }
            }
        }

        private KegHistory _currentHistory;
        public KegHistory CurrentHistory
        {
            get => _currentHistory;
            set
            {
                if(_currentHistory != value)
                {
                    _currentHistory = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentHistory"));
                }
            }
        }

        private IList<Hardware> _kegHardware;
        public IList<Hardware> KegHardware
        {
            get => _kegHardware;
            set
            {
                if(_kegHardware != value)
                {
                    _kegHardware = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("KegHardware"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}