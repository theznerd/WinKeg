using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WinKegCore.CaliburnCustom
{
    public class WinRTContainerEx : WinRTContainer
    {
        /// <summary>
        /// Registers the Caliburn.Micro navigation service with the container.
        /// </summary>
        /// <param name="rootFrame">The application root frame.</param>
        /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
        /// <param name="cacheViewModels">if set to <c>true</c> then navigation service cache view models for resuse.</param>
        public INavigationService RegisterNavigationServiceEx(Frame rootFrame, bool treatViewAsLoaded = false, bool cacheViewModels = false)
        {
            if (HasHandler(typeof(INavigationService), null))
                return this.GetInstance<INavigationService>(null);

            if (rootFrame == null)
                throw new ArgumentNullException("rootFrame");
#if WINDOWS_UWP
            var frameAdapter = new FrameAdapterEx(rootFrame, treatViewAsLoaded);
#else
            var frameAdapter = new FrameAdapterEx(rootFrame, treatViewAsLoaded);
#endif

            RegisterInstance(typeof(FrameAdapterEx), null, frameAdapter);

            return frameAdapter;
        }

    }
}
