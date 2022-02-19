using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinKeg.Hardware.Thermometers
{
    public class BMP280IoT : IHardware, IThermometer
    {
        // The BMP280 is a barometric pressure, temperature,
        // and altitude sensor released by Bosch. It's purpose 
        // in the WinKeg project is to monitor temperature inside 
        // the kegerator walls. It is non submursible and has
        // a configurable I2C address of 0x76 or 0x77 which means
        // it would need to be multiplexed to support more than
        // one, but for our purposes, a single temperature
        // reading will be sufficient.
        public static string DisplayName => "Bosch® BMP280 (.NET IoT)";
        public static string SetupString => "I²C Bus:int;";

        private I2cBus bus;
        private I2cDevice device;
        private Iot.Device.Bmxx80.Bmp280 bmp280;

        public BMP280IoT(string initializationData)
        {
            int busId;
            int.TryParse(initializationData.Split(';')[0], out busId);

            bus = I2cBus.Create(busId);
            device = bus.CreateDevice(0x76); // this might actually be 0x77 depending on config
            bmp280 = new Iot.Device.Bmxx80.Bmp280(device);
            bmp280.PressureSampling = Iot.Device.Bmxx80.Sampling.Skipped;
            bmp280.TemperatureSampling = Iot.Device.Bmxx80.Sampling.UltraLowPower;
            bmp280.FilterMode = Iot.Device.Bmxx80.FilteringMode.Bmx280FilteringMode.Off;
            bmp280.SetPowerMode(Iot.Device.Bmxx80.PowerMode.Bmx280PowerMode.Normal);
        }

        public async Task<double?> ReadTemperatureAsync()
        {
            UnitsNet.Temperature temp;
            bool success = bmp280.TryReadTemperature(out temp);
            if (success)
            { 
                return temp.DegreesCelsius; 
            }
            else
            {
                return null;
            }
        }
    }
}
