using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.DB.Models;
using WinKeg.DB;
using WinKeg.Hardware.Interfaces;
using System.Threading;

namespace WinKeg.Service.Monitoring
{
    public static class PowerMonitor
    {
        public static async void MonitorPower()
        {
            IPowerMeter powerMeter = null;

            // Determine which power meter is configured for use
            string powerMeterClass;
            string powerMeterData;

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                powerMeterClass = unitOfWork.Hardware.GetThermometer().Type;
                powerMeterData = unitOfWork.Hardware.GetThermometer().Data;
                unitOfWork.Dispose();
            }

            if (null != powerMeterClass)
            {
                try
                {
                    Type t = Type.GetType("WinKeg.Hardware.Thermometers." + powerMeterClass);
                    powerMeter = (IPowerMeter)Activator.CreateInstance(t, powerMeterData);
                }
                catch
                {
                    // throw some error here that we don't recognize the type of power meter
                }
            }

            if(null != powerMeter)
            {
                while (true)
                {
                    await Task.Run(async () =>
                    {
                        double currentPower = await powerMeter.GetCurrentWattageAsync();
                        KegeratorEvent kegeratorEvent = new KegeratorEvent()
                        {
                            Type = "ReadPower",
                            CreatedOn = DateTime.Now,
                            Message = currentPower.ToString()
                        };

                        using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                        {
                            unitOfWork.KegeratorEvents.Add(kegeratorEvent);

                            Setting setting = unitOfWork.Settings.GetSettingByName("CurrentPower");
                            if (null != setting)
                            {
                                setting.Value = currentPower.ToString();
                                unitOfWork.Settings.Update(setting);
                            }
                            else
                            {
                                setting = new Setting()
                                {
                                    Name = "CurrentPower",
                                    Value = currentPower.ToString()
                                };
                                unitOfWork.Settings.Add(setting);
                            }

                            unitOfWork.Complete();
                            unitOfWork.Dispose();
                        }
                    });

                    Thread.Sleep(60000); // let's grab power every minute
                }
            }
            else
            {
                // there is a misconfiguration here - we should throw an error or something
            }
        }
    }
}
