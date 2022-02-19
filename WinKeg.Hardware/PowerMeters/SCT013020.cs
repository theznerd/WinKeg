using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Converters;

namespace WinKeg.Hardware.PowerMeters
{
    public class SCT013020 : IHardware, IPowerMeter
    {
        // The SCT-013-020 is a split-core current transformer
        // that allows you to measure the amperage of a line
        // up to 20A, with an output of 1V @ 20A. We can use
        // this to do rough calculations of wattage used by
        // the kegerator. Needs an analog to digital converter
        // such as the ADS1015 to get a value.
        public string DisplayName => "SCT-013-020";
        public string SetupString => "AD Converter:string;AD Converter Data:string";

        private IADC analogDigitalConverter;

        // this should be modified in the future to allow
        // for different line voltages. It would actually
        // be better to measure actual line voltage, but
        // we're going for approximations here, not perfect
        // accuracy.
        private int LineVoltage = 125;

        public SCT013020(string initializationData)
        {
            try
            {
                Type t = Type.GetType("WinKeg.Hardware.Converters." + initializationData.Split(';')[0]);
                string adcData = initializationData.Split(';')[1] + ";" + initializationData.Split(';')[2]; // let's fix this later... this is ugly

                analogDigitalConverter = (IADC)Activator.CreateInstance(t, adcData);
            }
            catch
            {
                // throw some error here that we couldn't build the ADC
            }
        }

        public async Task<double?> GetCurrentWattageAsync()
        {
            double watts = 0;

            // get the readout from the ADC
            double? adcValue = analogDigitalConverter.ReadAnalogValueAsync().Result;

            if(null != adcValue)
            {
                // multiply by 20 (if read voltage is 1V, then we're at 20A)
                double amps = (double)adcValue * 20;

                // get the RMS value of amperage
                // probably more accurate than peak?
                double rms = amps / Math.Sqrt(2);

                // multiply by line voltage for watts
                // watts = volts * amps
                watts = Math.Round(rms * LineVoltage, 2); // again, approximations

                return watts;
            }
            else
            {
                return null; // null means we couldn't read from the ADC
            }
        }
    }
}
