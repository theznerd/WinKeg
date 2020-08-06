using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    public class BeverageImageRepository : Repository<BeverageImage>, IBeverageImageRepository
    {
        public BeverageImageRepository(WinKegContext context) : base(context) { }

        public BeverageImage GetBeverageImageWithBeverages(int id)
        {
            return WinKegContext.BeverageImages.Include(bi => bi.Beverages).SingleOrDefault(bi => bi.Id == id);
        }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
