using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class Hardware : INotifyPropertyChanged
    {
        public int Id { get; set; }

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

        private string _class;
        public string Class
        {
            get => _class;
            set
            {
                if(_class != value)
                {
                    _class = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Class"));
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

        private Kegerator _kegerator;
        public Kegerator Kegerator
        {
            get => _kegerator;
            set
            {
                if(_kegerator != value)
                {
                    _kegerator = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Kegerator"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}