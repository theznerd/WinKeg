using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class KegeratorEventRepository : Repository<KegeratorEvent>, IKegeratorEventRepository
    {
        public KegeratorEventRepository(WinKegContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<KegeratorEvent> GetEventsByType(string[] eventTypes)
        {
            return WinKegContext.KegeratorEvents.Where(e => eventTypes.Contains(e.Type)).ToList();
        }

        public KegeratorEvent GetLatestEventByType(string[] eventTypes)
        {
            return WinKegContext.KegeratorEvents.Where(e => eventTypes.Contains(e.Type)).OrderByDescending(e => e.CreatedOn).FirstOrDefault();
        }

        public WinKegContext WinKegContext
        {
            get { return _dbContext as WinKegContext; }
        }
    }
}
