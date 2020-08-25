using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Hardware.Interfaces;
using WinKeg.DB;
using WinKeg.DB.Models;

namespace WinKeg.Hardware.Mechanical
{
    public class Compressor
    {
        public bool CompressorRunning { get; set; }
        private IRelay relay;

        public Compressor(IRelay compressorRelay)
        {
            relay = compressorRelay;
        }

        public void GetCompressorState()
        {
            Setting compressorState;
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                compressorState = unitOfWork.Settings.GetSettingByName("CompressorRunning");
                unitOfWork.Dispose();
            }
            CompressorRunning = compressorState.Value == "Running";
        }

        public void SetCompressorState(bool state)
        {
            CompressorRunning = state;
            Setting compressorState;
            using(var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                compressorState = unitOfWork.Settings.GetSettingByName("CompressorRunning");
                switch(state)
                {
                    case true:
                        compressorState.Value = "Running";
                        break;
                    case false:
                        compressorState.Value = "Off";
                        break;
                }
                if(null != compressorState)
                {
                    unitOfWork.Settings.Update(compressorState);
                }
                else
                {
                    unitOfWork.Settings.Add(compressorState);
                }
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public void StartCompressor()
        {
            GetCompressorState();
            if(!CompressorRunning)
            {
                KegeratorEvent lastCompressorEvent;
                using(var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    lastCompressorEvent = unitOfWork.KegeratorEvents.GetLatestEventByType(new[] { "CompressorStart", "PowerOn" });
                    unitOfWork.Dispose();
                }

                // check to make sure the last time the compressor was started
                // was more than 15 minutes ago, to avoid cycling the compressor
                // too often
                if(null != lastCompressorEvent && lastCompressorEvent.CreatedOn <= DateTime.Now.AddMinutes(-15))
                {
                    relay.OpenRelay();
                    
                    KegeratorEvent newEvent = new KegeratorEvent()
                    {
                        Type = "CompressorStart",
                        CreatedOn = DateTime.Now
                    };
                    using (var unitOfWork = new UnitOfWork(new WinKegContext()))
                    {
                        unitOfWork.KegeratorEvents.Add(newEvent);
                        unitOfWork.Complete();
                        unitOfWork.Dispose();
                    }
                }
                else
                {
                    StopCompressor();
                }
            }
        }

        public void StopCompressor()
        {
            GetCompressorState();
            if(CompressorRunning)
            {
                relay.CloseRelay();
                SetCompressorState(false);
            }

            KegeratorEvent newEvent = new KegeratorEvent()
            {
                Type = "CompressorStop",
                CreatedOn = DateTime.Now
            };
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                unitOfWork.KegeratorEvents.Add(newEvent);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }
    }
}
