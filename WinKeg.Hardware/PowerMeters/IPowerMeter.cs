using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.PowerMeters
{
    public interface IPowerMeter
    {
        Task<double?> GetCurrentWattageAsync();
    }
}
