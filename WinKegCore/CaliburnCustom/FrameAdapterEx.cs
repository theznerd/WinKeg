using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Caliburn.Micro;
using Windows.UI.Xaml.Media.Animation;

namespace WinKegCore.CaliburnCustom
{
    public class FrameAdapterEx : FrameAdapter
    {
        private readonly Frame frame;
        private readonly bool treatViewAsLoaded;

        public FrameAdapterEx(Frame frame, bool treatViewAsLoaded = false) : base(frame, treatViewAsLoaded)
        {
            this.frame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;
        }

        public virtual bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo transition)
        {
            return frame.Navigate(sourcePageType, parameter, transition);
        }
    }
}