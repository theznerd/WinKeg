using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Base;
using WinKeg.Hardware.Interfaces;

namespace WinKeg.Hardware.PowerMeters
{
    public class SCT013020 : HardwareBase<string>, IPowerMeter
    {
        // The SCT-013-020 is a split-core current transformer
        // that allows you to measure the amperage of a line
        // up to 20A, with an output of 1V @ 20A. We can use
        // this to do rough calculations of wattage used by
        // the kegerator. Needs an analog to digital converter
        // such as the ADS1015 to get a value.
        public static string DisplayName { get { return "SCT-013-020"; } }
        public static string SetupString { get { return "AD Converter:IADC;"; } }
        
        // Which ADC should we be reading from?
        private IADC analogDigitalConverter;

        // this should be modified in the future to allow
        // for different line voltages. It would actually
        // be better to measure actual line voltage, but
        // we're going for approximations here, not perfect
        // accuracy.
        private int LineVoltage = 120;


        public SCT013020 (string data) : base(data)
        {
            try
            {
                Type t = Type.GetType("WinKeg.Hardware.Adapters." + data.Split(',')[0]);
                string adcData = string.Concat(data.Split(',')[1], data.Split(',')[2]);

                analogDigitalConverter = (IADC)Activator.CreateInstance(t, adcData); // add data to parameters
            }
            catch
            {
                // throw some error here that we don't recognize the type of ADC
            }
        }

        public async Task<double> GetCurrentWattageAsync()
        {
            double watts = 0;

            // get the voltage readout from the ADC
            double adcVoltage = await analogDigitalConverter.ReadDeviceAsync();

            // multiply by 20 (if read voltage is 1V, then we're at 20A)
            double amps = adcVoltage * 20;

            // get the RMS value of amperage
            // probably more accurate than peak?
            double rms = amps / Math.Sqrt(2);

            // multiply by line voltage for watts
            // watts = volts * amps
            watts = Math.Round(rms * LineVoltage, 2); // again, approximations

            return watts;
        }
    }
}
