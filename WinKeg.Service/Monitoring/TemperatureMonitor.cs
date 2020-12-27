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
    public static class TemperatureMonitor
    {
        public static async void MonitorTemperature()
        {
            IThermometer thermometer = null;

            // Determine which thermometer is configured for use
            string thermometerClass;
            string thermometerData;

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                thermometerClass = unitOfWork.Hardware.GetThermometer().Type;
                thermometerData = unitOfWork.Hardware.GetThermometer().Data;
                unitOfWork.Dispose();
            }

            if(null != thermometerClass)
            {
                try
                {
                    Type t = Type.GetType("WinKeg.Hardware.Thermometers." + thermometerClass);
                    thermometer = (IThermometer)Activator.CreateInstance(t, thermometerData);
                }
                catch
                {
                    // throw some error here that we don't recognize the type of thermometer
                }
            }

            if(null != thermometer)
            {
                while (true)
                {
                    await Task.Run(async () =>
                    {
                        double currentTemp = await thermometer.ReadTemperatureAsync();
                        KegeratorEvent kegeratorEvent = new KegeratorEvent()
                        {
                            Type = "ReadTemperature",
                            CreatedOn = DateTime.Now,
                            Message = currentTemp.ToString()
                        };

                        using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                        {
                            unitOfWork.KegeratorEvents.Add(kegeratorEvent);

                            Setting setting = unitOfWork.Settings.GetSettingByName("CurrentTemperature");
                            if (null != setting)
                            {
                                setting.Value = currentTemp.ToString();
                                unitOfWork.Settings.Update(setting);
                            }
                            else
                            {
                                setting = new Setting()
                                {
                                    Name = "CurrentTemperature",
                                    Value = currentTemp.ToString()
                                };
                                unitOfWork.Settings.Add(setting);
                            }

                            unitOfWork.Complete();
                            unitOfWork.Dispose();
                        }
                    });

                    Thread.Sleep(5000); // let's grab temperature every 5 seconds
                }
            }
            else
            {
                // there is a misconfiguration here - we should throw an error or something
            }
        }
    }
}
