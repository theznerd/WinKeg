using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace WinKeg.Data.DAL
{
    public class BeverageRepository : Repository<Beverage>, IBeverageRepository
    {
        public BeverageRepository(WinKegContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Beverage> GetAllWithImages()
        {
            return WinKegContext.Beverages.Include("Image").ToList();
        }

        public WinKegContext WinKegContext
        {
            get { return _dbContext as WinKegContext; }
        }
    }
}
