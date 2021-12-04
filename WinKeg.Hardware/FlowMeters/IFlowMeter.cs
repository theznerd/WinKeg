using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.FlowMeters
{
    public interface IFlowMeter
    {
        int CurrentFlowPulses { get; set; }
        int PreviousFlowPulses { get; set; }

        void StartFlowRead();
        void StopFlowRead();
    }
}
