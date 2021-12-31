using WinKeg.Data;
using WinKeg.Data.Models;
using WinKeg.Service.Services;

namespace WinKeg.Service
{
    public class WinKegBackgroundService : BackgroundService
    {
        private readonly CompressorService _compressorService;
        private readonly PowerService _powerService;
        private readonly TemperatureService _temperatureService;

        public WinKegBackgroundService(CompressorService compressorService, PowerService powerService, TemperatureService temperatureService)
            => (_compressorService, _powerService, _temperatureService) = (compressorService, powerService, temperatureService);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int compressorTimeout = 3;
            int compressorInterval = 3;
            int powerTimeout = 12;
            int powerInterval = 12;

            KegeratorEvent kegeratorEvent = new KegeratorEvent()
            {
                Type = "PowerOn",
                CreatedOn = DateTime.Now,
                Message = "Kegerator powered on..."
            };

            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                unitOfWork.KegeratorEvents.Add(kegeratorEvent);
                unitOfWork.Complete();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                if(compressorTimeout >= compressorInterval)
                {
                    _compressorService.ExecuteCompressorLogic();
                    compressorTimeout = 0;
                }
                if(powerTimeout >= powerInterval)
                {
                    _powerService.MonitorPowerAsync();
                    powerTimeout = 0;
                }
                _temperatureService.MonitorTemperatureAsync();

                compressorTimeout++;
                powerTimeout++;

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}