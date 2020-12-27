using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.DB.Models
{
    public class Keg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double InitialVolume { get; set; }
        public double CurrentVolume { get; set; }
        public int FlowCalibration { get; set; }

        public ICollection<KegHistory> KegHistories { get; set; }
        public ICollection<Hardware> KegHardware { get; set; }
    }
}
