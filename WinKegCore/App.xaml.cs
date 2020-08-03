using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using WinKegCore.ViewModels;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using WinKegCore.Views.Setup;
using WinKegCore.ViewModels.Setup;

namespace WinKegCore
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen; // Launch App Fullscreen

            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();

            container.PerRequest<HomeViewModel>();
            container.PerRequest<SetupStartViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true; // Make the titlebar match the view

            // Check to see if initial setup is complete
            if (false && !Setup.SetupComplete())
            {
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