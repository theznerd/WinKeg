using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class KegHistory : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private DateTime _createdOn;

        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                if(_createdOn != value)
                {
                    _createdOn = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CreatedOn"));
                }
            }
        }

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get => _lastModified;
            set
            {
                if (_lastModified != value)
                {
                    _lastModified = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LastModified"));
                }
            }
        }

        private IList<HistoryEvent> _historyEvents;
        public IList<HistoryEvent> HistoryEvents
        {
            get => _historyEvents;
            set
            {
                if(_historyEvents != value)
                {
                    _historyEvents = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("HistoryEvents"));
                }
            }
        }

        public int? KegID { get; set; }
        private Keg? _keg;
        public Keg? Keg
        {
            get => _keg;
            set
            {
                if(_keg != value)
                {
                    _keg = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Keg"));
                }
            }
        }

        public int? BeverageID { get; set; }
        private Beverage? _beverage;
        public Beverage? Beverage
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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}