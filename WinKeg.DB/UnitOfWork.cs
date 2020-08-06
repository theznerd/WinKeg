using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Interfaces;

namespace WinKeg.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WinKegContext _context;

        public UnitOfWork(WinKegContext context)
        {
            _context = context;
        }

        public IBeverageImageRepository BeverageImages { get; private set; }
        public IBeverageRepository Beverages { get; private set; }
        public IHardwareRepository Hardware { get; private set; }
        public IHistoryEventRepository HistoryEvents { get; private set; }
        public IKegeratorEventRepository KegeratorEvents { get; private set; }
        public IKegHistoryRepository KegHistories { get; private set; }
        public IKegRepository Kegs { get; private set; }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
