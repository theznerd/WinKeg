using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Interfaces
{
    public interface IADC
    {
        string DisplayName { get; }
        Task<double> ReadDeviceAsync();
    }
}
