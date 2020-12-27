using System;
using System.Collections.Generic;
using System.Text;

namespace WinKeg.Hardware.Interfaces
{
    public interface IFlowMeter
    {
        int CurrentFlowPulses { get; set; }
        int PreviousFlowPulses { get; set; }

        void StartFlowRead();
        void StopFlowRead();
    }
}
