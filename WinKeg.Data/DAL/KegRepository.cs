using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class KegRepository : Repository<Keg>, IKegRepository
    {
        private readonly WinKegContext _context;
        public KegRepository(WinKegContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<KegHistory> GetKegHistories(int id)
        {
            return _context.KegHistories.Where(x => x.KegID == id).AsEnumerable();
        }
    }
}
