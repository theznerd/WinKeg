using System;
using System.Collections.Generic;
using System.Text;

namespace WinKeg.Hardware.Interfaces
{
    public interface IRelay
    {
        void OpenRelay();
        void CloseRelay();
    }
}
