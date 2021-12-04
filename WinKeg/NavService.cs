using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace WinKeg
{
    public class NavService : INavService
    {
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }

        void INavService.NavigateWithAnimation(Type sourcePage, NavigationTransitionInfo navigationTransitionInfo)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, null, navigationTransitionInfo);
        }

        void INavService.NavigateWithAnimation(Type sourcePage, object parameter, NavigationTransitionInfo navigationTransitionInfo)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter, navigationTransitionInfo);
        }
    }
}
