using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.Hardware;
using WinKeg.Hardware.Thermometers;

namespace WinKeg.Service.Services
{
    public class TemperatureService
    {
        internal IThermometer thermometer;

        public TemperatureService()
        {
            Data.Models.Hardware hw;
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                hw = unitOfWork.Hardware.GetThermometer();
                unitOfWork.Dispose();
            }

            thermometer = (IThermometer)HardwareFactory.GetHardwareClass(hw.Class, hw.Data);
        }

        internal async void MonitorTemperatureAsync()
        {
            double? currentTemp = await thermometer.ReadTemperatureAsync();
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
                if(setting != null)
                {
                    setting.Value = currentTemp.ToString();
                    unitOfWork.Settings.Edit(setting);
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
        }
    }
}
