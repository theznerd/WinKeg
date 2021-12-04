using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public interface IKegeratorEventRepository : IRepository<KegeratorEvent>
    {
        public IEnumerable<KegeratorEvent> GetEventsByType(string[] eventTypes);
        public KegeratorEvent GetLatestEventByType(string[] eventTypes);
    }
}
