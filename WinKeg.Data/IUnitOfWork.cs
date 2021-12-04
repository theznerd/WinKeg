using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.DAL;

namespace WinKeg.Data
{
    internal interface IUnitOfWork : IDisposable
    {
        IBeverageImageRepository BeverageImages { get; }
        IBeverageRepository Beverages { get; }
        IHardwareRepository Hardware { get; }
        IHistoryEventRepository HistoryEvents { get; }
        IKegeratorRepository Kegerator { get; }
        IKegeratorEventRepository KegeratorEvents { get; }
        IKegHistoryRepository KegHistories { get; }
        IKegRepository Kegs { get; }
        IUserRepository Users { get; }
        ISettingRepository Settings { get; }
        int Complete();
    }
}
