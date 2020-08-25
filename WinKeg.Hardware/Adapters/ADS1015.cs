using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.Hardware.Interfaces;
using Windows.Devices.I2c;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Adapters
{
    public class ADS1015 : IADC
    {
        // The ADS1015 is an analog/digital converter from
        // Texas Instruments, which allows us to measure a
        // voltage in a 12-bit signed integer. Currently,
        // this implementation of the ADS1015 is limited to
        // a 1 volt gain (will read +- 1.024v) and multiplexes
        // the AIN0/AIN1 pins. Originally written to support
        // the SCT-013-020 current transformer. This could be
        // expanded at a future date to support other voltages
        public string DisplayName { get { return "ADS1015"; } }
        
        public enum I2cAddress
        {
            Ground = 0x48,
            VDD = 0x49,
            SDA = 0x4A,
            SCL = 0x4B
        }

        private I2cConnectionSettings connectionSettings;
        private DeviceInformationCollection devices;
        private I2cDevice device;
        private string I2cString;

        // +-0.4mV per tick @ 1v range (ADS1015 - 12-bits signed)
        // we may want to expand this later for devices with a
        // larger range (e.g. current transformer at 2V?)
        private double voltageConversion = 0.00048828125;

        public ADS1015(string busId, I2cAddress address)
        {
            connectionSettings = new I2cConnectionSettings((int)address)
            {
                BusSpeed = I2cBusSpeed.FastMode,
                SharingMode = I2cSharingMode.Shared
            };

            I2cString = I2cDevice.GetDeviceSelector("I2C" + busId);
            InitializeDevice();
        }

        // Startup the device 
        public async void InitializeDevice()
        {
            devices = await DeviceInformation.FindAllAsync(I2cString);
            device = await I2cDevice.FromIdAsync(devices[0].Id, connectionSettings);

            device.Write(new byte[] { 0x01, 0x06, 0xA3 }); // Write the config register:
                                                           // Mutiplex AIN0/AIN1
                                                           // Set gain to +-1.024v
                                                           // Continuous conversion mode
                                                           // 2400 samples per second
                                                           // Disable comparator
            device.Write(new byte[] { 0x02, 0x00, 0x00 }); // Set the low threshold
            device.Write(new byte[] { 0x03, 0xFF, 0xFF }); // Set the high threshold
        }

        public async Task<double> ReadDeviceAsync()
        {
            if(devices == null || device == null)
            {
                InitializeDevice();
            }

            int maxValue = 0;

            await Task.Run(() =>
            {
                // loop 1000 samples
                // and then grab the maximum value
                // returned from the ADS1015
                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        // Read the conversion register
                        var bytearray = new byte[2];
                        device.WriteRead(new byte[] { 0x0 }, bytearray);

                        // make sure we have the array in the correct order
                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(bytearray);
                        }

                        // Bit shift right - 4 places (12-bit ADC)
                        var value = BitConverter.ToInt16(bytearray, 0) >> 4;

                        // Convert to absolute value
                        value = Math.Abs(value);

                        // Check if we've reached a new maximum in our sample period
                        if (value > maxValue) { maxValue = value; }
                    }
                    catch
                    {
                        // guess the device wasn't ready yet
                        // try again :)
                    }
                }
            });

            return maxValue * voltageConversion;
        }
    }
}
