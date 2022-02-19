using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Converters
{
    public class ADS1015 : IHardware, IADC
    {
        // The ADS1015 is an analog/digital converter from
        // Texas Instruments, which allows us to measure a
        // voltage in a 12-bit signed integer. Currently,
        // this implementation of the ADS1015 is limited to
        // AC 1.414volt (will read +- 2.048v) and multiplexes
        // the AIN0/AIN1 pins. Originally written to support
        // the SCT-013-020 current transformer. This could be
        // expanded at a future date to support other voltages
        public static string DisplayName => "ADS1015";
        public static string SetupString => "I²C Bus:int;Address:int;";

        private I2cConnectionSettings connectionsettings;
        private I2cDevice device;

        // 4.096v full scale, and 4096 step resolution
        // equals 0.001v per step.
        private double voltageConversion = 0.001;

        public ADS1015(string initializationData)
        {
            int busId;
            int.TryParse(initializationData.Split(';')[0], out busId);

            int address;
            int.TryParse(initializationData.Split(';')[1], out address);

            connectionsettings = new I2cConnectionSettings(busId, address);
            device = I2cDevice.Create(connectionsettings);

            InitializeDevice();
        }

        private void InitializeDevice()
        {
            device.Write(new byte[] { 0x01, 0x02, 0xA3 }); // Write the config register:
                                                           // Multiplex AIN0/AIN1 (differential)
                                                           // Set gain to +-2.048v
                                                           // Continuous conversion mode
                                                           // 2400 samples per second
                                                           // Disable comparator

            // device.Write(new byte[] { 0x01, 0x2A, 0xA3 }); // Write the config register:
            //                                                // Multiplex AIN0/GND
            //                                                // Set gain to +-2.048v
            //                                                // Continuous conversion mode
            //                                                // 2400 samples per second
            //                                                // Disable comparator
            device.Write(new byte[] { 0x02, 0x00, 0x00 }); // Set the comparator low threshold (not strictly necessary)
            device.Write(new byte[] { 0x03, 0xFF, 0xFF }); // Set the comparator high threshold (not strictly necessary)
        }

        public async Task<double?> ReadAnalogValueAsync()
        {
            if(device == null)
            {
                return null; // if we return null we know
                             // there is an issue with the
                             // device
            }

            int maxValue = 0;
            int max = 0;
            int min = 0;

            await Task.Run(() =>
            {
                // loop 1000 samples
                // and then grab the maximum
                // and minimum values returned
                // from ADS1015
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

                        // Convert to absolute value (A/C is positive or negative)
                        var absvalue = Math.Abs(value);

                        if (value > max)
                            max = value;

                        if (value < min)
                            min = value;

                        // Check if we've reached a new maximum in our sample period
                        if (absvalue > maxValue)
                            maxValue = absvalue;
                    }
                    catch
                    {
                        // device wasn't ready...
                        // oh well... try again!
                    }
                }
            });

            Console.Write(max.ToString() + " " + min.ToString());
            var measuredVoltage = (max - min) * voltageConversion;
            //var measuredVoltage = ((double)maxValue * voltageConversion);
            return measuredVoltage;
        }
    }
}
