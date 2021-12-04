using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.Hardware;
using WinKeg.Hardware.Mechanical;

namespace WinKeg.Service.Services
{
    public class CompressorService
    {
        internal Compressor compressor;

        public CompressorService()
        {
            Data.Models.Hardware hw;
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                hw = unitOfWork.Hardware.GetCompressor();
                unitOfWork.Dispose();
            }

            compressor = (Compressor)HardwareFactory.GetHardwareClass(hw.Class, hw.Data);
        }

        internal void ExecuteCompressorLogic()
        {
            // check if the power is on or off
            Setting compressorPower;
            Setting setTemperature;
            Setting curTemperature;

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                compressorPower = unitOfWork.Settings.GetSettingByName("CompressorPower");
                setTemperature = unitOfWork.Settings.GetSettingByName("SetTemperature");
                curTemperature = unitOfWork.Settings.GetSettingByName("CurrentTemperature");
                unitOfWork.Dispose();
            }

            bool compressorOn;
            bool.TryParse(compressorPower.Value, out compressorOn);

            double currentTemperature;
            double desiredTemperature;
            double.TryParse(curTemperature.Value, out currentTemperature);
            double.TryParse(setTemperature.Value, out desiredTemperature);

            if(compressorOn)
            {
                if (currentTemperature > desiredTemperature)
                {
                    compressor.StartCompressor();
                }
                else if (currentTemperature < desiredTemperature - 1) { }
                else
                {
                    compressor.StopCompressor();
                }
            }
            else
            {
                compressor.StopCompressor();
            }
        }
    }
}