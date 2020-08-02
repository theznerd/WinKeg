using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBeverageRestricted { get; set; }
        public bool IsAdministrator { get; set; }
        public string EncryptedPasscode { get; set; }
        public string PCSalt { get; set; }
        public DateTime LastModified { get; set; }

        public ICollection<HistoryEvent> HistoryEvents { get; set; }
    }
}
