﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public interface IHardwareRepository : IRepository<Hardware>
    {
        public Hardware GetCompressor();
        public Hardware GetThermometer();
        public Hardware GetPowerMeter();
    }
}
