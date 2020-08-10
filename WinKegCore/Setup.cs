using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinKegCore
{
    public class Setup
    {
        public static void CreateDatabase()
        {
            WinKeg.DB.WinKegContext winKegContext = new WinKeg.DB.WinKegContext();
            winKegContext.Database.Migrate();
        }

        public static bool SetupComplete()
        {
            StorageFolder publisherCache = ApplicationData.Current.GetPublisherCacheFolder("WinKegData");
            string databasePath = publisherCache.Path + "\\WinKeg.db";
            if(System.IO.File.Exists(databasePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
