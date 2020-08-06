using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinKeg.DB;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinKegCore.Dialogs
{
    public enum PasscodeResult
    {
        SignInOK,
        SignInFail,
        SignInCancel,
        Nothing
    }

    public sealed partial class PasscodeDialog : ContentDialog
    {
        public PasscodeResult Result { get; private set; }
        private bool isForAdmin { get; set; }

        public PasscodeDialog(bool forAdmin)
        {
            this.InitializeComponent();
            this.Opened += PasscodeAdmin_Opened;
            this.Closing += PasscodeAdmin_Closing;
            isForAdmin = forAdmin;
        }

        private void PasscodeAdmin_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // test
        }

        private void PasscodeAdmin_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            this.Result = PasscodeResult.Nothing;
        }

        private void Secondary_Click(object sender, RoutedEventArgs e)
        {
            this.Result = PasscodeResult.SignInCancel;
            this.Hide();
        }

        private void Primary_Click(object sender, RoutedEventArgs e)
        {
            if(WinKeg.DB.Controllers.PasscodeController.ValidateAdmin(Passcode.Password))
            {
                this.Result = PasscodeResult.SignInOK;
            }
            else
            {
                this.Result = PasscodeResult.SignInFail;
            }
            this.Hide();
        }

        private void PinClick(object sender, RoutedEventArgs e)
        {
            string pin = ((Button)sender).Tag.ToString();
            if(pin == "Clear")
            {
                Passcode.Password = "";
            }
            else
            {
                Passcode.Password += pin;
            }
        }
    }
}
