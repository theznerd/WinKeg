using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using WinKegCore.ViewModels;
using WinKegCore.Views;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using WinKegCore.Views.Setup;
using WinKegCore.ViewModels.Setup;
using WinKegCore.CaliburnCustom;
using System.Dynamic;
using Windows.UI.Xaml;
using Windows.Storage;
using Microsoft.EntityFrameworkCore;

namespace WinKegCore
{
    public sealed partial class App
    {
        private WinRTContainerEx container;

        public App()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen; // Launch App Fullscreen

            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainerEx();
            container.RegisterWinRTServices();

            container.PerRequest<HomeViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationServiceEx(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            StorageFolder publisherCache = ApplicationData.Current.GetPublisherCacheFolder("WinKegData");
            string databasePath = publisherCache.Path + "\\WinKeg.db";
            var connectionString = @"Data Source=" + databasePath + ";";
            WinKeg.DB.Configuration.ConnectionString = connectionString;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true; // Make the titlebar match the view

            // Check to see if initial setup is complete
            if (!Setup.SetupComplete())
            {
                Setup.CreateDatabase();
                container.PerRequest<SetupStartViewModel>();
                container.PerRequest<SetupKegeratorViewModel>();
                container.PerRequest<SetupUserViewModel>();
                DisplayRootView<SetupStartView>();
            }
            else
            {
                DisplayRootView<HomeView>();
            }
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}