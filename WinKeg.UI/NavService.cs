using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.UI
{
    public class NavService : INavService
    {
        private Frame _frame = App.rootFrame;

        public void Navigate(Type sourcePage)
        {
            // var frame = (Frame)Window.Current.Content;
            _frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            // var frame = (Frame)Window.Current.Content;
            _frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            // var frame = (Frame)Window.Current.Content;
            _frame.GoBack();
        }

        void INavService.NavigateWithAnimation(Type sourcePage, NavigationTransitionInfo navigationTransitionInfo)
        {
            // var frame = (Frame)Window.Current.Content;
            _frame.Navigate(sourcePage, null, navigationTransitionInfo);
        }

        void INavService.NavigateWithAnimation(Type sourcePage, object parameter, NavigationTransitionInfo navigationTransitionInfo)
        {
            // var frame = (Frame)Window.Current.Content;
            _frame.Navigate(sourcePage, parameter, navigationTransitionInfo);
        }
    }
}
