﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public interface ISettingRepository : IRepository<Setting>
    {
        public Setting GetSettingByName(string settingName);
    }
}
