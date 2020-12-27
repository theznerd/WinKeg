using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;
using WinKeg.DB;
using WinKeg.DB.Controllers;
using WinKeg.DB.Models;
using WinKegCore.CaliburnCustom;
using WinKegCore.Dialogs;
using WinKeg.Hardware.Interfaces;
using WinKeg.Hardware.Base;

namespace WinKegCore.ViewModels.Setup
{
    public class SetupHardwareViewModel : Screen
    {
        private readonly FrameAdapterEx _navigationService;

        public SetupHardwareViewModel(FrameAdapterEx navigationService)
        {
            _navigationService = navigationService;


            Type itherm = typeof(IThermometer);
            ThermometerClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => itherm.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            Type ipower = typeof(IPowerMeter);
            PowerMeterClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => ipower.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            Type iflow = typeof(IFlowMeter);
            FlowMeterClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => iflow.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            Type irelay = typeof(IRelay);
            RelayClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => irelay.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();
        }

        private List<Type> RelayClasses;
        public Dictionary<string, string> Relay
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (Type t in RelayClasses)
                {
                    r.Add(t.Name, (string)t.GetProperty("DisplayName").GetValue(null));
                }
                return r;
            }
        }
        private KeyValuePair<string, string> _SelectedRelay;
        public KeyValuePair<string, string> SelectedRelay
        {
            get
            {
                return _SelectedRelay;
            }
            set
            {
                _SelectedRelay = value;
                NotifyOfPropertyChange(() => SelectedRelay);
            }

        }

        private List<Type> FlowMeterClasses;
        public Dictionary<string, string> FlowMeter
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (Type t in FlowMeterClasses)
                {
                    r.Add(t.Name, (string)t.GetProperty("DisplayName").GetValue(null));
                }
                return r;
            }
        }
        private KeyValuePair<string, string> _SelectedFlowMeter;
        public KeyValuePair<string, string> SelectedFlowMeter
        {
            get
            {
                return _SelectedFlowMeter;
            }
            set
            {
                _SelectedFlowMeter = value;
                NotifyOfPropertyChange(() => SelectedFlowMeter);
            }

        }

        private List<Type> ThermometerClasses;
        public Dictionary<string, string> Thermometer
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (Type t in ThermometerClasses)
                {
                    r.Add(t.Name, (string)t.GetProperty("DisplayName").GetValue(null));
                }
                return r;
            }
        }
        private KeyValuePair<string, string> _SelectedThermometer;
        public KeyValuePair<string, string> SelectedThermometer
        {
            get
            {
                return _SelectedThermometer;
            }
            set
            {
                _SelectedThermometer = value;
                NotifyOfPropertyChange(() => SelectedThermometer);
            }

        }

        private List<Type> PowerMeterClasses;
        public Dictionary<string, string> PowerMeter
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (Type t in PowerMeterClasses)
                {
                    r.Add(t.Name, (string)t.GetProperty("DisplayName").GetValue(null));
                }
                return r;
            }
        }

        private KeyValuePair<string, string> _SelectedPowerMeter;
        public KeyValuePair<string, string> SelectedPowerMeter
        {
            get
            {
                return _SelectedPowerMeter;
            }
            set
            {
                _SelectedPowerMeter = value;
                NotifyOfPropertyChange(() => SelectedPowerMeter);
            }
            
        }

        private double _NumTaps = 2;
        public double NumTaps
        {
            get { return _NumTaps; }
            set
            {
                _NumTaps = value;
                NotifyOfPropertyChange(() => NumTaps);
            }
        }

        public void Continue()
        {
            // Create a list for the Kegs
            List<Keg> kegs = new List<Keg>();
            int NTaps = Convert.ToInt32(NumTaps);

            for (int i = 0; i < NTaps; i++)
            {
                Keg k = new Keg();
                kegs.Add(k);
            }

            List<Hardware> hardwareToAdd = new List<Hardware>();

            // Add the Kegerator Hardware
            foreach(Keg k in kegs)
            {
                // Flow Meter
                Hardware fm = new Hardware()
                {
                    Class = SelectedFlowMeter.Key,
                    Type = "FlowMeter",
                    Keg = k
                };

                // Relay
                Hardware r = new Hardware()
                {
                    Class = SelectedRelay.Key,
                    Type = "Relay",
                    Keg = k
                };

                hardwareToAdd.Add(fm);
                hardwareToAdd.Add(r);
            }

            // Create the Compressor Relay
            Hardware compressorRelay = new Hardware()
            {
                Type = "Compressor",
                Class = SelectedRelay.Key
            };
            hardwareToAdd.Add(compressorRelay);

            // Create the Power Meter
            Hardware powerMeter = new Hardware()
            {
                Type = "PowerMeter",
                Class = SelectedPowerMeter.Key
            };
            hardwareToAdd.Add(powerMeter);

            // Create the Thermometer
            Hardware thermometer = new Hardware()
            {
                Type = "Thermometer",
                Class = SelectedThermometer.Key
            };
            hardwareToAdd.Add(thermometer);

            // Write the Hardware to the Database
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                unitOfWork.Hardware.AddRange(hardwareToAdd);
                unitOfWork.Complete();
            }

            _navigationService.NavigateToViewModel<SetupUserViewModel>(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
