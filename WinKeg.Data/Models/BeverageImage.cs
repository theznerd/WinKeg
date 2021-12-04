using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class BeverageImage : INotifyPropertyChanged
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

        private byte[] _imageBlob;
        public byte[] ImageBlob
        {
            get => _imageBlob;
            set
            {
                if(_imageBlob != value)
                {
                    _imageBlob = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ImageBlob"));
                }
            }
        }

        private IList<Beverage> _beverages;
        public IList<Beverage> Beverages
        {
            get => _beverages;
            set
            {
                if(_beverages != value)
                {
                    _beverages = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Beverages"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}