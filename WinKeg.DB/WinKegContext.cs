using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.DB.Models;

namespace WinKeg.DB
{
    public class WinKegContext : DbContext
    {
        public WinKegContext()
        {

        }

        // Entities
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KegeratorEvent> KegeratorEvents { get; set; }
        public DbSet<Keg> Kegs { get; set; }
        public DbSet<KegHistory> KegHistories { get; set; }
        public DbSet<HistoryEvent> HistoryEvents { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<BeverageImage> BeverageImages { get; set; }
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<Kegerator> Kegerator { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=WinKeg.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
