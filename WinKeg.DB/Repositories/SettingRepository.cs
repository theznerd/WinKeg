using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(WinKegContext context) : base(context) { }

        public Setting GetSettingByName(string settingName)
        {
            return WinKegContext.Settings.Where(s => s.Name == settingName).FirstOrDefault();
        }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
