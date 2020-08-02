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
        public int RelayPin { get; set; }
        public int FlowPin { get; set; }
        public int FlowMeterCalibration { get; set; }

        public ICollection<KegHistory> KegHistories { get; set; }
    }
}
