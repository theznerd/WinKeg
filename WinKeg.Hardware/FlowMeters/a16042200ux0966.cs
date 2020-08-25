using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Base;
using WinKeg.Hardware.Interfaces;

namespace WinKeg.Hardware.FlowMeters
{
    public class a16042200ux0966 : GPIOFlowMeterBase, IFlowMeter
    {
        // The uxcell a16042200ux0966 is a 1/4" hall effect
        // water flow sensor that can handle rates of 0.25 -
        // 3.0L per minute. All flow meters should be 
        // calibrated as part of the initial setup of the
        // kegerator... all the flow meter should care about
        // is counting pulses.
        public string DisplayName { get { return "Uxcell 1/4\" Hall Effect"; } }

        private GPIO gpio;
        public int CurrentFlowPulses { get; set; }
        public int PreviousFlowPulses { get; set; }

        public a16042200ux0966(int relayPin) : base(relayPin)
        {
            gpio = new GPIO(gpioPin);
            gpio.GpioPinPulsed += FlowSensorPulsed;
        }

        private void FlowSensorPulsed(object sender, EventArgs e)
        {
            CurrentFlowPulses++;
        }

        public void StartFlowRead()
        {
            CurrentFlowPulses = 0; // reset the counter to zero
            gpio.StartPulseRead();
        }

        public void StopFlowRead()
        {
            gpio.StopPulseRead();
            PreviousFlowPulses = CurrentFlowPulses;
        }
    }
}
