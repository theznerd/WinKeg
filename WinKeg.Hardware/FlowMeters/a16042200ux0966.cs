using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Base;

namespace WinKeg.Hardware.FlowMeters
{
    public class a16042200ux0966 : IHardware, IFlowMeter
    {
        // The uxcell a16042200ux0966 is a 1/4" hall effect
        // water flow sensor that can handle rates of 0.25 -
        // 3.0L per minute. All flow meters should be 
        // calibrated as part of the initial setup of the
        // kegerator... all the flow meter should care about
        // is counting pulses.
        public static string DisplayName => "Uxcell 1/4\" Hall Effect";
        public static string SetupString => "GPIO Pin:int";

        private GPIO gpio;
        public int CurrentFlowPulses { get; set; }
        public int PreviousFlowPulses { get; set; }

        public a16042200ux0966(string initializationData)
        {
            int gpioPin;
            int.TryParse(initializationData.Split(';')[0], out gpioPin);

            gpio = new GPIO(gpioPin);
            gpio.GpioPinPulsed += FlowSensorPulsed;
        }

        private void FlowSensorPulsed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            CurrentFlowPulses++;
        }

        public void StartFlowRead()
        {
            CurrentFlowPulses = 0;
            gpio.StartPulseRead();
        }

        public void StopFlowRead()
        {
            gpio.StopPulseRead();
            PreviousFlowPulses = CurrentFlowPulses;
        }
    }
}
