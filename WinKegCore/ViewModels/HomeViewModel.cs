using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace WinKegCore.ViewModels
{
    public class HomeViewModel
    {
        // Menu Handling
        public async void OpenMenu()
        {
            Dialogs.PasscodeDialog adminPasscode = new Dialogs.PasscodeDialog(true);
            await adminPasscode.ShowAsync();

            var result = adminPasscode.Result;
            if(result == Dialogs.PasscodeResult.SignInOK)
            {
                // navigate to menu
            }
        }
    }
}
