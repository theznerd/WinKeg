using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.Hardware;
using WinKeg.Hardware.PowerMeters;

namespace WinKeg.Service.Services
{
    public class PowerService
    {
        internal IPowerMeter powerMeter;

        public PowerService()
        {
            Data.Models.Hardware hw;
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                hw = unitOfWork.Hardware.GetCompressor();
                unitOfWork.Dispose();
            }

            powerMeter = (IPowerMeter)HardwareFactory.GetHardwareClass(hw.Class, hw.Data);
        }

        public async void MonitorPowerAsync()
        {
            double? currentPower = await powerMeter.GetCurrentWattageAsync();
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
                if(setting != null)
                {
                    setting.Value = currentPower.ToString();
                    unitOfWork.Settings.Edit(setting);
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
        }
    }
}
