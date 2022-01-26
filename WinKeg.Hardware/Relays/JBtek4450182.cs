using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Base;

namespace WinKeg.Hardware.Relays
{
    public class JBtek4450182 : IHardware, IRelay
    {
        // The JBtek 4450182, is a four-channel, 5V DC relay
        // that can be used to switch up to 10A at 250V, more
        // than sufficient for most chest freezer compressors
        // and the semi-direct activated EPDM solenoid valve
        // used to pour beer in the original incarnation of
        // WinKeg
        public static string DisplayName => "JBtek 4-Channel 5V DC Relay";
        public static string SetupString => "GPIO Pin:int";

        private GPIO gpio;
        private int closedGpioState = 1; // determines whether the pin needs to be
                                         // driven high or low to close the relay
                                         // 1 = High, 0 = Low

        public JBtek4450182(string initializationData)
        {
            int.TryParse(initializationData, out int gpioPin);

            gpio = new GPIO(gpioPin);

            // set the default state of the pin
            // the pins on this relay need to be
            // driven HIGH to close the relay
            switch(closedGpioState)
            {
                case 1:
                    gpio.SetPinHigh();
                    break;
                case 0:
                    gpio.SetPinLow();
                    break;
                default:
                    gpio.SetPinLow();
                    break;
            }
        }

        public void CloseRelay()
        {
            gpio.SetPinHigh();
        }

        public void OpenRelay()
        {
            gpio.SetPinLow();
        }
    }
}
