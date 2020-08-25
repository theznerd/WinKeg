using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;
using WinKeg.DB;
using WinKeg.DB.Interfaces;
using WinKeg.DB.Models;
using WinKegCore.CaliburnCustom;

namespace WinKegCore.ViewModels.Setup
{
    public class SetupKegeratorViewModel : PropertyChangedBase
    {
        private readonly FrameAdapterEx _navigationService;

        public SetupKegeratorViewModel(FrameAdapterEx navigationService)
        {
            _navigationService = navigationService;
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                kegerator = unitOfWork.Kegerator.Get(1);
            }
            if(null == kegerator)
            {
                kegerator = new Kegerator();
            }
            else
            {
                alreadySet = true;
            }
        }

        private Kegerator kegerator;
        public Kegerator Kegerator
        {
            get { return kegerator; }
            set
            {
                kegerator = value;
                NotifyOfPropertyChange(() => KegeratorName);
                NotifyOfPropertyChange(() => KegeratorOwner);
                NotifyOfPropertyChange(() => KegeratorLocation);
            }
        }

        private bool alreadySet;

        public string KegeratorName
        {
            get { return kegerator.Name; }
            set
            {
                kegerator.Name = value;
                NotifyOfPropertyChange(() => KegeratorName);
            }
        }

        public string KegeratorOwner
        {
            get { return kegerator.Owner; }
            set
            {
                kegerator.Owner = value;
                NotifyOfPropertyChange(() => KegeratorOwner);
            }
        }

        public string KegeratorLocation
        {
            get { return kegerator.Location; }
            set
            {
                kegerator.Location = value;
                NotifyOfPropertyChange(() => KegeratorLocation);
            }
        }

        public void Continue()
        {
            // save database info
            if (!alreadySet)
            {
                using(var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    unitOfWork.Kegerator.Add(kegerator);
                    unitOfWork.Complete();
                }
            }
            else
            {
                using(var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    unitOfWork.Kegerator.Update(kegerator);
                    unitOfWork.Complete();
                }
            }
            _navigationService.NavigateToViewModel<SetupUserViewModel>(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
