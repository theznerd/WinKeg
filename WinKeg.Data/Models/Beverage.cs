using System.ComponentModel;

namespace WinKeg.Data.Models
{
    public class Beverage : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private string _style;
        public string Style
        {
            get => _style;
            set
            {
                if (value != _style)
                {
                    _style = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Style"));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                }
            }
        }

        private double _abv;
        public double ABV
        {
            get => _abv;
            set
            {
                if (value != _abv)
                {
                    _abv = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ABV"));
                }
            }
        }

        private double _ibu;
        public double IBU
        {
            get => _ibu;
            set
            {
                if (_ibu != value)
                {
                    _ibu = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IBU"));
                }
            }
        }

        private bool _isRestricted;
        public bool IsRestricted
        {
            get => _isRestricted;
            set
            {
                if (_isRestricted != value)
                {
                    _isRestricted = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRestricted"));
                }
            }
        }

        private BeverageImage? _image;
        public BeverageImage? Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Image"));
                }
            }
        }

        private IList<KegHistory>? _kegHistories;
        public IList<KegHistory>? KegHistories
        {
            get => _kegHistories;
            set
            {
                if (_kegHistories != value)
                {
                    _kegHistories = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("KegHistories"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}