using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string? _name;
        public string? Name
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

        private bool _isBeverageRestricted = false;
        public bool IsBeverageRestricted
        {
            get => _isBeverageRestricted;
            set
            {
                if(_isBeverageRestricted != value)
                {
                    _isBeverageRestricted = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBeverageRestricted"));
                }
            }
        }

        private bool _isAdministrator = false;
        public bool IsAdministrator
        {
            get => _isAdministrator;
            set
            {
                if(_isAdministrator != value)
                {
                    _isAdministrator = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsAdministrator"));
                }
            }
        }

        private string? _encryptedPasscode;
        public string? EncryptedPasscode
        {
            get => _encryptedPasscode;
            set
            {
                if(_encryptedPasscode != value)
                {
                    _encryptedPasscode = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("EncryptedPasscode"));
                }
            }
        }

        private string? _pcSalt;
        public string? PCSalt
        {
            get => _pcSalt;
            set
            {
                if(_pcSalt != value)
                {
                    _pcSalt = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("PCSalt"));
                }
            }
        }

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get => _lastModified;
            set
            {
                if(_lastModified != value)
                {
                    _lastModified = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LastModified"));
                }
            }
        }

        private List<HistoryEvent>? _historyEvents;
        public List<HistoryEvent>? HistoryEvents
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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}