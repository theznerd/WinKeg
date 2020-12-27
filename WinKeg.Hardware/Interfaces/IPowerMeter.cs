using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Interfaces
{
    public interface IPowerMeter
    {
        Task<double> GetCurrentWattageAsync();
    }
}
