using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class KegeratorRepository : Repository<Kegerator>, IKegeratorRepository
    {
        public KegeratorRepository(WinKegContext dbContext) : base(dbContext)
        {
        }
    }
}
