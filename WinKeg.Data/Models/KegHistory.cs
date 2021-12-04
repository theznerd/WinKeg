using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class KegHistory : INotifyPropertyChanged
    {
        public int Id { get; set; }
        // public Guid GUID { get; set; } // I don't believe this is actually necessary anymore

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

        private int _kegID;
        public int KegID
        {
            get => _kegID;
            set
            {
                if(_kegID != value)
                {
                    _kegID = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("KegID"));
                }
            }
        }

        private Keg _keg;
        public Keg Keg
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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}