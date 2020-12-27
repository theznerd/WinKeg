using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    public class KegRepository : Repository<Keg>, IKegRepository
    {
        public KegRepository(WinKegContext context) : base(context) { }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
