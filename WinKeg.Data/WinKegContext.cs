using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data
{
    public class WinKegContext : DbContext
    {
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<BeverageImage> BeverageImages { get; set; }
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<HistoryEvent> HistoryEvents { get; set; }
        public DbSet<Keg> Kegs { get; set; }
        public DbSet<Kegerator> Kegerators { get; set; }
        public DbSet<KegeratorEvent> KegeratorEvents { get; set; }
        public DbSet<KegHistory> KegHistories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; private set; }

        public WinKegContext()
        {
            var folder = Environment.SpecialFolder.CommonApplicationData;
            var path = Environment.GetFolderPath(folder)+$"{Path.DirectorySeparatorChar}WinKeg";
            DbPath = $"{path}{Path.DirectorySeparatorChar}WinKeg.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
