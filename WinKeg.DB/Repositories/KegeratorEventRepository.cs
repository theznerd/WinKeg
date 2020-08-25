using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;

namespace WinKeg.DB.Repositories
{
    class KegeratorEventRepository : Repository<KegeratorEvent>, IKegeratorEventRepository
    {
        public KegeratorEventRepository(WinKegContext context) : base(context) { }

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
            get { return Context as WinKegContext; }
        }
    }
}
