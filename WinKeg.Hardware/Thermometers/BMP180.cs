using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Thermometers
{
    public class BMP180 : IHardware, IThermometer
    {
        // The BMP180 is a barometric pressure, temperature,
        // and altitude sensor released by Bosch. It's purpose 
        // in the WinKeg project is to monitor temperature inside 
        // the kegerator walls. It is non submursible and has
        // since been replaced by the BMP280. It has a static
        // I2C address of 0x77 which means it would need to be
        // multiplexed to support more than one, but for our
        // purposes, a single temperature reading will be sufficient.
        public static string DisplayName => "Bosch® BMP180";
        public static string SetupString => "I²C Bus:int;";

        private I2cConnectionSettings connectionSettings;
        private I2cBus bus;
        private I2cDevice device;

        // define calibration data for temperature
        private short ac1;
        private short ac2;
        private short ac3;
        private ushort ac4;
        private ushort ac5;
        private ushort ac6;
        private short b1;
        private short b2;
        private short mb;
        private short mc;
        private short md;

        public BMP180(string initializationData)
        {
            int busId;
            int.TryParse(initializationData.Split(';')[0], out busId);

            bus = I2cBus.Create(busId);
            device = bus.CreateDevice(0x77);
            // connectionSettings = new I2cConnectionSettings(busId, 0x77);
            // device = I2cDevice.Create(connectionSettings);
            InitializeDevice();
        }

        private void InitializeDevice()
        {
            // Read calibration data from EEPROM
            ac1 = BMP180ReadShort(0xAA);
            ac2 = BMP180ReadShort(0xAC);
            ac3 = BMP180ReadShort(0xAE);
            ac4 = BMP180ReadUShort(0xB0);
            ac5 = BMP180ReadUShort(0xB2);
            ac6 = BMP180ReadUShort(0xB4);
            b1 = BMP180ReadShort(0xB6);
            b2 = BMP180ReadShort(0xB8);
            mb = BMP180ReadShort(0xBA);
            mc = BMP180ReadShort(0xBC);
            md = BMP180ReadShort(0xBE);
        }

        private short BMP180ReadShort(byte address)
        {
            byte MSB;
            byte LSB;

            device.Write(new byte[] { address });

            // Read two bytes from BMP180
            byte[] bytearray = new byte[2];
            device.Read(bytearray);

            // Return the value
            MSB = bytearray[0];
            LSB = bytearray[1];
            return (short)(MSB << 8 | LSB);
        }

        private ushort BMP180ReadUShort(byte address)
        {
            byte MSB;
            byte LSB;

            device.Write(new byte[] { address });

            // Read two bytes from BMP180
            byte[] bytearray = new byte[2];
            device.Read(bytearray);

            // Return the value
            MSB = bytearray[0];
            LSB = bytearray[1];
            return (ushort)(MSB << 8 | LSB);
        }

        private async Task<long> BMP180ReadUT()
        {
            byte[] ba = new byte[] { 0xF4, 0x2E }; // this is the register to tell
                                                   // the BMP180 to gather temp
            device.Write(ba);

            await Task.Delay(20); // wait 20ms for the data to be in the register

            return BMP180ReadUShort(0xF6); // 0xF6 is the register where the data is stored
        }

        public async Task<double?> ReadTemperatureAsync()
        {
            try
            {
                // The following formulas come from the
                // datasheet for the BMP180
                long x1;
                long x2;
                long b5;

                if (null == device)
                {
                    InitializeDevice();
                }
                long ut = await BMP180ReadUT();

                x1 = (((long)ut - (long)ac6) * (long)ac5) >> 15;
                x2 = ((long)mc << 11) / (x1 + md);
                b5 = x1 + x2;

                double longValue = (((b5 + 8) >> 4));
                return longValue / 10;
            }
            catch
            {
                return null; // couldn't read from the device
            }
        }
    }
}
