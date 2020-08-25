using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using WinKeg.Hardware.Interfaces;
using WinKeg.Hardware.Relays;

namespace WinKeg.Hardware.Mechanical
{
    public class SolenoidValve
    {
        private Timer durationTimer;
        private IRelay relay;

        public SolenoidValve(IRelay solenoidRelay)
        {
            relay = solenoidRelay;
        }

        public void OpenValve()
        {
            durationTimer = new Timer
            {
                Interval = 60000 // 60 seconds to pour dude
            };

            durationTimer.Elapsed += TimesUp;
            durationTimer.Start();

            relay.OpenRelay();
        }

        public void CloseValve()
        {
            relay.CloseRelay();
            durationTimer.Stop();
            durationTimer.Dispose();
        }

        private void TimesUp(object sender, ElapsedEventArgs e)
        {
            // We've been pouring too long... did someone forget?
            CloseValve();
        }
    }
}
