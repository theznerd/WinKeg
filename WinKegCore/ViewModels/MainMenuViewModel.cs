using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;
using WinKegCore.CaliburnCustom;

namespace WinKegCore.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        private readonly FrameAdapterEx _navigationService;

        public MainMenuViewModel(FrameAdapterEx navigationService)
        {
            _navigationService = navigationService;
        }

        public void CloseButton()
        {
            _navigationService.GoBack();
        }
    }
}
