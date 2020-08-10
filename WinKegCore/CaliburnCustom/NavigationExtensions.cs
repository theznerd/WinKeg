using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace WinKegCore.CaliburnCustom
{
    public static class NavigationExtensions
    {
        public static bool NavigateToViewModel(this FrameAdapterEx navigationService, Type viewModelType, NavigationTransitionInfo transition,
           object parameter = null)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            if (viewType == null)
            {
                throw new InvalidOperationException(
                    string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            }

            return navigationService.Navigate(viewType, parameter, transition);
        }

        public static bool NavigateToViewModel<T>(this FrameAdapterEx navigationService, NavigationTransitionInfo transition, object parameter = null)
        {
            return navigationService.NavigateToViewModel(typeof(T), transition, parameter);
        }
    }
}
