using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;

namespace WinKeg.DB.Interfaces
{
    public interface IKegeratorEventRepository : IRepository<KegeratorEvent>
    {
        IEnumerable<KegeratorEvent> GetEventsByType(string[] eventTypes);
        KegeratorEvent GetLatestEventByType(string[] eventTypes);
    }
}
