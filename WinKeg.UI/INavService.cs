using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.UI
{
    public interface INavService
    {
        void Navigate(Type sourcePage);
        void Navigate(Type sourcePage, object parameter);

        void NavigateWithAnimation(Type sourcePage, Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo navigationTransitionInfo);
        void NavigateWithAnimation(Type sourcePage, object parameter, Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo navigationTransitionInfo);
        void GoBack();
    }
}
