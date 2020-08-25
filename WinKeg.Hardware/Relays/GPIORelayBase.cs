using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Relays
{
    public abstract class GPIORelayBase
    {
        protected int gpioPin;
        protected GPIORelayBase(int relayPin)
        {
            gpioPin = relayPin;
        }
    }
}
