using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Storage;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace WinKeg.Service
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Check to make sure that setup is complete first
            // if so, then we can continue, otherwise - let's wait for 30 seconds

            StorageFolder publisherCache = ApplicationData.Current.GetPublisherCacheFolder("WinKegData");
            string databasePath = publisherCache.Path + "\\WinKeg.db";
            var connectionString = @"Data Source=" + databasePath + ";";
            DB.Configuration.ConnectionString = connectionString;

            deferral = taskInstance.GetDeferral();

            Monitoring.TemperatureMonitor.MonitorTemperature();
            Monitoring.PowerMonitor.MonitorPower();
            Controllers.CompressorController.MaintainTemperature();
        }
    }
}
