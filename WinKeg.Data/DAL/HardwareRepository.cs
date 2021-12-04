using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class HardwareRepository : Repository<Hardware>, IHardwareRepository
    {
        public HardwareRepository(WinKegContext dbContext) : base(dbContext)
        {
        }

        public Hardware GetCompressor()
        {
            return WinKegContext.Hardware.Where(h => h.Type == "Compressor").FirstOrDefault();
        }

        public Hardware GetThermometer()
        {
            return WinKegContext.Hardware.Where(h => h.Type == "Thermometer").FirstOrDefault();
        }

        public WinKegContext WinKegContext
        {
            get { return _dbContext as WinKegContext; }
        }
    }
}
