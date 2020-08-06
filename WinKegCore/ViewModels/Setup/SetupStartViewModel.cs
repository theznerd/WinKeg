using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKegCore.ViewModels.Setup
{
    public class SetupStartViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public SetupStartViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Continue()
        {
            _navigationService.NavigateToViewModel<SetupKegeratorViewModel>();
        }
    }
}
