using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Thermometers
{
    // much of this code is stolen from:
    // https://github.com/gloveboxes/Windows-IoT-Core-Driver-Library/blob/master/Glovebox.IoT.Devices/Sensors/BMP280.cs
    // There is definitely something wrong in the code/formulas here
    // The .NET IoT Devices code works fine. Use that instead for now.

    public class BMP280 : IHardware, IThermometer
    {
        // The BMP280 is a barometric pressure, temperature,
        // and altitude sensor released by Bosch. It's purpose 
        // in the WinKeg project is to monitor temperature inside 
        // the kegerator walls. It is non submursible and has
        // a configurable I2C address of 0x76 or 0x77 which means
        // it would need to be multiplexed to support more than
        // one, but for our purposes, a single temperature
        // reading will be sufficient.

        public static string DisplayName => "Bosch® BMP280";
        public static string SetupString => "I²C Bus:int;";

        private I2cBus bus;
        private I2cDevice device;

        // define calibration data for temperature (this is more than I need right now
        private ushort dig_T1;
        private short dig_T2;
        private short dig_T3;

        protected enum Register : byte
        {
            REGISTER_DIG_T1 = 0x88,
            REGISTER_DIG_T2 = 0x8A,
            REGISTER_DIG_T3 = 0x8C,
            REGISTER_CONTROL = 0xF4,
            REGISTER_TEMPDATA = 0xFA
        };

        const byte TempPressure16xOverSampling = 0xB7; // 16x ovesampling for Temperature and Pressure
        const byte Temp1xOverSamplingPressureDisableNormalMode = 0x23; // Disable pressure measurement, 1x oversampling, and set normal mode
        public BMP280(string initializationData)
        {
            int busId;
            int.TryParse(initializationData.Split(';')[0], out busId);

            bus = I2cBus.Create(busId);
            device = bus.CreateDevice(0x76); // this might actually be 0x77 depending on config
            InitializeDevice();
        }

        private void InitializeDevice()
        {
            // Read calibration data from EEPROM
            dig_T1 = ReadU16_LE(Register.REGISTER_DIG_T1);
            dig_T2 = Read16_LE(Register.REGISTER_DIG_T2);
            dig_T3 = Read16_LE(Register.REGISTER_DIG_T3);

            // set oversampling to 16x
            // Write8(Register.REGISTER_CONTROL, TempPressure16xOverSampling);

            // set the control register
            Write8(Register.REGISTER_CONTROL, Temp1xOverSamplingPressureDisableNormalMode);
        }

        private void Write8(Register register, byte value)
        {
            device.Write(new byte[] { (byte)register, value });
        }

        private ushort ReadU16(Register register)
        {
            byte[] result = new byte[2];
            device.WriteRead(new byte[] { (byte)register }, result);
            return (ushort)(result[0] << 8 | result[1]);
        }

        private short Read16(Register register)
        {
            byte[] result = new byte[2];
            device.WriteRead(new byte[] {(byte)register }, result);
            return (short)(result[0] << 8 | result[1]);
        }

        private ushort ReadU16_LE(Register register)
        {
            ushort temp = ReadU16(register);
            return (ushort)(temp >> 8 | temp << 8);
        }

        private short Read16_LE(Register register)
        {
            short temp = Read16(register);
            return (short)(temp >> 8 | temp << 8);
        }

        private int Read24(Register register)
        {
            byte[] result = new byte[3];
            device.WriteRead(new byte[] {(byte)register }, result);
            return result[0] << 16 | result[1] << 8 | result[2];
        }

        protected int t_fine;
        public async Task<double?> ReadFineTemperatureAsync()
        {
            try
            {
                // The following formulas come from the
                // datasheet for the BMP280
                int var1, var2;
                int adc_T = Read24(Register.REGISTER_TEMPDATA);

                adc_T >>= 4;

                var1 = (((adc_T >> 3) - (dig_T1 << 1)) * dig_T2) >> 11;
                var2 = (((((adc_T >> 4) - dig_T1) * ((adc_T >> 4) - dig_T1)) >> 12) * dig_T3) >> 14;

                t_fine = var1 + var2;

                double T = (t_fine * 5 + 128) >> 8;
                return Math.Round(T / 100D, 2);
            }
            catch
            {
                return null; // couldn't read from the device
            }
        }

        public async Task<double?> ReadTemperatureAsync()
        {
            try
            {
                // The following formulas come from the
                // datasheet for the BMP280
                double var1, var2, T;

                int adc_T = Read24(Register.REGISTER_TEMPDATA);
                adc_T >>= 4;

                var1 = (((double)adc_T) / 16384.0 - ((double)dig_T1) / 1024.0) * ((double)dig_T2);
                var2 = ((((double)adc_T) / 131072.0 - ((double)dig_T1) / 8192.0) * (((double)adc_T) / 131072.0 - ((double)dig_T1) / 8192.0)) * ((double)dig_T3);
                t_fine = (int)(var1 + var2);
                T = (var1 + var2) / 5120.0;
                return T;
            }
            catch
            {
                return null; // couldn't read from the device
            }
        }
    }
}
