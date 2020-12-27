using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinKeg.DB;
using WinKeg.DB.Models;
using WinKeg.Hardware.Interfaces;
using WinKeg.Hardware.Mechanical;

namespace WinKeg.Service.Controllers
{
    public static class CompressorController
    {
        public static async void MaintainTemperature()
        {
            Compressor compressor = null;
            IRelay relay = null;

            // Determine which relay is configured for use
            string relayClass;
            string relayData;
            bool compressorOn;

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                relayClass = unitOfWork.Hardware.GetCompressor().Type;
                relayData = unitOfWork.Hardware.GetCompressor().Data;
                bool.TryParse(unitOfWork.Settings.GetSettingByName("CompressorPower").Value, out compressorOn);
                unitOfWork.Dispose();
            }

            if (null != relayClass)
            {
                try
                {
                    Type t = Type.GetType("WinKeg.Hardware.Relays." + relayClass);
                    relay = (IRelay)Activator.CreateInstance(t, relayData);
                }
                catch
                {
                    // throw some error here that we don't recognize the type of thermometer
                }
            }

            if (null != relay)
            {
                compressor = new Compressor(relay);
            }
            else
            {
                // hmm... something went wrong... throw an error?
            }

            if (null != compressor)
            {
                while (true)
                {
                    await Task.Run(() =>
                    {
                        Setting currentTemperatureSetting = null;
                        Setting desiredTemperatureSetting = null;

                        using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                        {
                            currentTemperatureSetting = unitOfWork.Settings.GetSettingByName("CurrentTemperature");
                            desiredTemperatureSetting = unitOfWork.Settings.GetSettingByName("SetTemperature");
                        };

                        double currentTemperature;
                        double desiredTemperature;
                        double.TryParse(currentTemperatureSetting.Value, out currentTemperature);
                        double.TryParse(desiredTemperatureSetting.Value, out desiredTemperature);

                        if (compressorOn) // if the power for the compressor is on...
                        {
                            if (currentTemperature > desiredTemperature)
                            {
                                compressor.StartCompressor();
                            }
                            else if (currentTemperature > desiredTemperature - 1) { } // lets get to 1 degree below the desired temp before stopping
                            else
                            {
                                // We've reached our desired temperature!
                                compressor.StopCompressor();
                            }
                        }
                        else
                        {
                            compressor.StopCompressor();
                        }
                    });

                    Thread.Sleep(15000);
                }
            }
            else
            {
                // hmm... something went wrong... throw an error?
            }
        }
    }
}
