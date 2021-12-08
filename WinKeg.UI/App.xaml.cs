using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinKeg.Data;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinKeg.UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            using(var UnitOfWork = new UnitOfWork(new WinKegContext()))
            {
                // load the database to speed up the first database read
                UnitOfWork.Kegerator.GetAll();
                UnitOfWork.Dispose();
            }
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new Window();
            m_window.Content = rootFrame = new Frame();
            rootFrame.Navigate(typeof(Views.MainPageView));

            m_window.Activate();

            _appWindow = GetAppWindowForWindow(m_window);
            _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen); // bug in WinUI 3 1.0 where the bounds are not properly set
                                                                        // https://github.com/microsoft/microsoft-ui-xaml/issues/6245
                                                                        // https://github.com/microsoft/WindowsAppSDK/issues/1853
        }

        internal static Frame rootFrame;
        internal static Window m_window;
        private AppWindow _appWindow;

        private AppWindow GetAppWindowForWindow(Window w)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(w);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }
    }
}
