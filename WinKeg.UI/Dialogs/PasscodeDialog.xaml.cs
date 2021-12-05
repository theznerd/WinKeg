using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinKeg.UI.Dialogs
{
    public enum PasscodeResult
    {
        SignInOK,
        SignInFail,
        PasscodeSet,
        Cancel,
        Nothing
    }

    public sealed partial class PasscodeDialog : ContentDialog
    {
        public PasscodeResult Result { get; private set; }
        private bool isForAdmin { get; set; }
        private bool setPasscode { get; set; }
        private int userId { get; set; }

        public PasscodeDialog(bool forAdmin)
        {
            this.InitializeComponent();
            this.Opened += PasscodeAdmin_Opened;
            this.Closing += PasscodeAdmin_Closing;
            this.isForAdmin = forAdmin;
            this.setPasscode = false;
        }

        public PasscodeDialog(int userId)
        {
            this.InitializeComponent();
            this.Opened += PasscodeAdmin_Opened;
            this.Closing += PasscodeAdmin_Closing;
            this.setPasscode = true;
            this.userId = userId;
        }

        /*
        public PasscodeDialog(User u)
        {
            InitializeComponent();
            Opened += PasscodeAdmin_Opened;
            Closing += PasscodeAdmin_Closing;
            user = u;
            this.setPasscode = true;
        }
        */

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
            this.Result = PasscodeResult.Cancel;
            this.Hide();
        }

        private void Primary_Click(object sender, RoutedEventArgs e)
        {
            this.Result = PasscodeResult.SignInFail;

            // THIS IS TEMPORARY CODE
            // REMOVE BEFORE DEPLOYMENT
            if(Passcode.Password == "0000")
                this.Result = PasscodeResult.SignInOK;
            // ABOVE IS TEMPORARY CODE
            // REMOVE BEFORE DEPLOYMENT

            /*
            if (setPasscode && user == null)
            {
                WinKeg.Data.Middleware.PasscodeMiddleware.SetPasscode(Passcode.Password, userId);
                this.Result = PasscodeResult.PasscodeSet;
                this.Hide();
                return;
            }
            else if(setPasscode && user != null)
            {
                string[] returnData = WinKeg.Data.Middleware.PasscodeMiddleware.SetPasscode(Passcode.Password);
                user.EncryptedPasscode = returnData[0];
                user.PCSalt = returnData[1];
                this.Result = PasscodeResult.PasscodeSet;
                this.Hide();
                return;
            }
            

            if (WinKeg.Data.Middleware.PasscodeMiddleware.ValidateAdmin(Passcode.Password))
            {
                this.Result = PasscodeResult.SignInOK;
            }
            else
            {
                this.Result = PasscodeResult.SignInFail;
            }
            */
            this.Hide();
            return;
        }

        private void PinClick(object sender, RoutedEventArgs e)
        {
            string pin = ((Button)sender).Tag.ToString();
            if (pin == "Clear")
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
