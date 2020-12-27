using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    public class HardwareRepository : Repository<Hardware>, IHardwareRepository
    {
        public HardwareRepository(WinKegContext context) : base(context) { }

        public Hardware GetThermometer()
        {
            return WinKegContext.Hardware.Where(h => h.Type == "Thermometer").FirstOrDefault();
        }

        public Hardware GetCompressor()
        {
            return WinKegContext.Hardware.Where(h => h.Type == "Compressor").FirstOrDefault();
        }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
