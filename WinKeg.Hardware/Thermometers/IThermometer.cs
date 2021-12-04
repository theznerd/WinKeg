using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Thermometers
{
    public interface IThermometer
    {
        Task<double?> ReadTemperatureAsync();
    }
}
