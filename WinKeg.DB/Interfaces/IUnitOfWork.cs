using System;
using System.Collections.Generic;
using System.Text;

namespace WinKeg.DB.Interfaces
{
    interface IUnitOfWork : IDisposable
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
