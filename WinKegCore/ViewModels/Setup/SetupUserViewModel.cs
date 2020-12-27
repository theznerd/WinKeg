using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Media.Animation;
using WinKeg.DB;
using WinKeg.DB.Controllers;
using WinKegCore.CaliburnCustom;
using WinKegCore.Dialogs;
using User = WinKeg.DB.Models.User;

namespace WinKegCore.ViewModels.Setup
{
    public class SetupUserViewModel : Screen
    {
        private readonly FrameAdapterEx _navigationService;

        public SetupUserViewModel(FrameAdapterEx navigationService)
        {
            _navigationService = navigationService;
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                user = unitOfWork.Users.Get(1);
            }
            if (null == user)
            {
                user = new User()
                {
                    IsAdministrator = true
                };
            }
            else
            {
                alreadySet = true;
                passcodeSet = true;
            }
        }

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        private bool alreadySet = false;
        private bool passcodeSet = false;

        public string UserName
        {
            get { return user.Name; }
            set
            {
                User.Name = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public async void SetPasscode()
        {
            if (!alreadySet)
            {
                using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    unitOfWork.Users.Add(user);
                    unitOfWork.Complete();
                }
                alreadySet = true;
            }

            Dialogs.PasscodeDialog adminPasscode = new Dialogs.PasscodeDialog(1);
            await adminPasscode.ShowAsync();
            if(adminPasscode.Result == PasscodeResult.PasscodeSet)
            {
                // load newly created hash/salt into user object
                using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    user = unitOfWork.Users.Get(1);
                }
                passcodeSet = true;
            }
        }

        public void Continue()
        {
            // save database info
            if (!passcodeSet)
            {
                PasscodeController.SetPasscode("00000", 1); // set default passcode to five 0s if not set
            }
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                unitOfWork.Users.Update(user);
                unitOfWork.Complete();
            }
            _navigationService.NavigateToViewModel<SetupHardwareViewModel>(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        public void Back()
        {
            _navigationService.NavigateToViewModel<SetupKegeratorViewModel>(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
