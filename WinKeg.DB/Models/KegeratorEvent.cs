using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class KegeratorEvent
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; }
    }
}
