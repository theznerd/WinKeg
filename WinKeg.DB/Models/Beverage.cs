using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class Beverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public double ABV { get; set; }
        public double IBU { get; set; }
        public bool IsRestricted { get; set; }

        public BeverageImage BeverageImage { get; set; }
        public ICollection<KegHistory> KegHistories { get; set; }
    }
}
