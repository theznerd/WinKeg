using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;

namespace WinKeg.DB.Interfaces
{
    public interface IHardwareRepository : IRepository<Hardware>
    {
        Hardware GetThermometer();
        Hardware GetCompressor();
    }
}
