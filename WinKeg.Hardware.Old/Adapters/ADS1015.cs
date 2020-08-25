using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.Hardware.Interfaces;
using System.Device.I2c;

namespace WinKeg.Hardware.Adapters
{
    public class ADS1015
    {
        public enum I2cAddress
        {
            Ground = 0x48,
            VDD = 0x49,
            SDA = 0x4A,
            SCL = 0x4B
        }

        private I2cConnectionSettings connectionSettings;
        private I2cDevice device;

        // 0.4mV per tick (ADS1015 - 12-bits)
        private double voltageConversion = 0.00048828125;

        public ADS1015(int busId, I2cAddress address)
        {
            connectionSettings = new I2cConnectionSettings(busId, (int)address);
            
        }
    }
}
