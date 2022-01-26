using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Base;

namespace WinKeg.Hardware.Relays
{
    public class RELAYR5V4C : IHardware, IRelay
    {
        // The RELAY-R5V-4C, is a four-channel, 5V DC relay
        // that can be used to switch up to 10A at 250V, more
        // than sufficient for most chest freezer compressors
        // and the semi-direct activated EPDM solenoid valve
        // used to pour beer in the original incarnation of
        // WinKeg
        public static string DisplayName => "DZS Elec 5V 4 Channel Relay";
        public static string SetupString => "GPIO Pin:int";

        private GPIO gpio;

        public RELAYR5V4C(string initializationData)
        {
            int.TryParse(initializationData, out int gpioPin);

            // Technically the relay can be activated high
            // or low, but we're going to force low because
            // it's just silly to open a relay on low voltage
            gpio = new GPIO(gpioPin);
            gpio.SetPinLow();
        }

        public void CloseRelay()
        {
            gpio.SetPinLow();
        }

        public void OpenRelay()
        {
            gpio.SetPinHigh();
        }
    }
}
