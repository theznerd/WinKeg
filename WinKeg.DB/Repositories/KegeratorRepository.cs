using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    public class KegeratorRepository : Repository<Kegerator>, IKegeratorRepository
    {
        public KegeratorRepository(WinKegContext context) : base(context) { }

        public Dictionary<string, string> GetKegeratorInfo()
        {
            Kegerator kegerator = this.Get(1);
            if(null != kegerator)
            {
                return new Dictionary<string, string>()
                {
                    { "Name", kegerator.Name },
                    { "Owner", kegerator.Owner },
                    { "Location", kegerator.Location }
                };
            }
            else
            {
                return null;
            }
        }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
