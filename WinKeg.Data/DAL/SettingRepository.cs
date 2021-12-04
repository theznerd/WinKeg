using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(WinKegContext dbContext) : base(dbContext)
        {

        }

        public Setting GetSettingByName(string settingName)
        {
            return DBContext.Settings.Where(s => s.Name == settingName).FirstOrDefault();
        }

        private WinKegContext DBContext
        {
            get { return _dbContext as WinKegContext; }
        }
    }
}
