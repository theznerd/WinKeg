using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg
{
    public interface INavService
    {
        void Navigate(Type sourcePage);
        void Navigate(Type sourcePage, object parameter);

        void NavigateWithAnimation(Type sourcePage, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo navigationTransitionInfo);
        void NavigateWithAnimation(Type sourcePage, object parameter, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo navigationTransitionInfo);
        void GoBack();
    }
}
