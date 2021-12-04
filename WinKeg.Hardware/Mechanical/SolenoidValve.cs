using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WinKeg.Hardware.Relays;

namespace WinKeg.Hardware.Mechanical
{
    public class SolenoidValve : IHardware
    {
        public static string DisplayName => "Solenoid Valve";
        public static string SetupString => "Relay:string;GPIO Pin:int";

        private System.Timers.Timer durationTimer;
        private IRelay relay;

        public SolenoidValve(string initializationData)
        {
            Type t = Type.GetType("WinKeg.Hardware.Relays." + initializationData.Split(';')[0]);
            string relayData = initializationData.Split(';')[1];

            relay = (IRelay)Activator.CreateInstance(t, relayData);

            durationTimer = new System.Timers.Timer
            {
                Interval = 60000 // 60 seconds to pour...
            };
        }

        public void OpenValve()
        {
            durationTimer = new System.Timers.Timer
            {
                Interval = 60000
            };

            durationTimer.Elapsed += TimesUp;
            durationTimer.Start();

            relay.OpenRelay();
        }

        public void CloseValve()
        {
            relay.CloseRelay();
            durationTimer?.Stop();
            durationTimer?.Dispose();
        }

        private void TimesUp(object? sender, ElapsedEventArgs e)
        {
            // We've been pouring too long... did someone forget?
            CloseValve();
        }
    }
}
