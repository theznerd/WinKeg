using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;

namespace WinKeg.DB.Interfaces
{
    public interface IKegeratorRepository : IRepository<Kegerator>
    {
        Dictionary<string, string> GetKegeratorInfo();
    }
}
