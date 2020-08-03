using System;
using System.Collections.Generic;
using System.Text;

namespace WinKeg.DB.Models
{
    public class Hardware
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }

        public Keg Keg { get; set; }
        public Kegerator Kegerator { get; set; }
    }
}
