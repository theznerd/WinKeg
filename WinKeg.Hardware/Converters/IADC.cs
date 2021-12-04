using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Converters
{
    public interface IADC
    {
        Task<double?> ReadAnalogValueAsync();
    }
}
