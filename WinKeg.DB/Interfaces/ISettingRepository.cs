using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;

namespace WinKeg.DB.Interfaces
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Setting GetSettingByName(string settingName);
    }
}
