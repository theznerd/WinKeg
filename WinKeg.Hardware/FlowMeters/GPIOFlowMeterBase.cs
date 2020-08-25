using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.FlowMeters
{
    public abstract class GPIOFlowMeterBase
    {
        protected int gpioPin;
        protected GPIOFlowMeterBase(int relayPin)
        {
            gpioPin = relayPin;
        }
    }
}
