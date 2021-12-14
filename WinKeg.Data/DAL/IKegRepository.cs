using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public interface IKegRepository : IRepository<Keg>
    {
        IEnumerable<KegHistory> GetKegHistories(int id);
        IEnumerable<Beverage> GetCurrentBeveragesFromAllKegs();
        IEnumerable<Keg> GetAllWithBeverageAndCurrentHistory();
    }
}
