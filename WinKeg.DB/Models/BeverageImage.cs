using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class BeverageImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBlob { get; set; }

        public ICollection<Beverage> Beverages { get; set; }
    }
}
