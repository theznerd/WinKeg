using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class HistoryEvent : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                if(_user != value)
                {
                    _user = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("User"));
                }
            }
        }

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

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if(_type != value)
                {
                    _type = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
                }
            }
        }

        private string _data;
        public string Data
        {
            get => _data;
            set
            {
                if(_data != value)
                {
                    _data = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Data"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}