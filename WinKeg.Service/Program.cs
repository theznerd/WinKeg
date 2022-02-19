using WinKeg.Service;
using WinKeg.Service.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WinKeg Background Service";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<WinKegBackgroundService>();
        services.AddSingleton<TemperatureService>();
        services.AddSingleton<PowerService>();
        //services.AddSingleton<CompressorService>();
    })
    .Build();

await host.RunAsync();