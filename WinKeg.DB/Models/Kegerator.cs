using System;
using System.Collections.Generic;
using System.Text;

namespace WinKeg.DB.Models
{
    public class Kegerator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Location { get; set; }
        
        public ICollection<Hardware> KegeratorHardware { get; set; }
    }
}
