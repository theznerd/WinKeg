using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class KegHistory
    {
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        
        public ICollection<HistoryEvent> HistoryEvents { get; set; }
        public Keg Keg { get; set; }
        public Beverage Beverage { get; set; }
    }
}
