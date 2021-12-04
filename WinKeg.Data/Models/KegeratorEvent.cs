using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Data.Models
{
    public class KegeratorEvent : INotifyPropertyChanged
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

        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                if (_createdOn != value)
                {
                    _createdOn = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CreatedOn"));
                }
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if(_message != value)
                {
                    _message = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Message"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
