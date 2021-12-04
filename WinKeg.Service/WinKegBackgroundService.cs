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