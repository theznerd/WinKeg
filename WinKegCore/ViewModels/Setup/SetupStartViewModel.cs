using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;
using WinKegCore.CaliburnCustom;

namespace WinKegCore.ViewModels.Setup
{
    public class SetupStartViewModel : Screen
    {
        private readonly FrameAdapterEx _navigationService;

        public SetupStartViewModel(FrameAdapterEx navigationService)
        {
            _navigationService = navigationService;
        }

        public void Continue()
        {
            _navigationService.NavigateToViewModel<SetupKegeratorViewModel>(new DrillInNavigationTransitionInfo());
        }
    }
}
