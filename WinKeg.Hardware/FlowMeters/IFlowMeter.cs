﻿using System;
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
        event EventHandler FlowPulsed;

        void OnFlowPulsed(EventArgs e);

        void StartFlowRead();
        void StopFlowRead();
    }
}
