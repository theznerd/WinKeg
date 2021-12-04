using WinKeg.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WinKeg Background Service";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<WinKegBackgroundService>();
    })
    .Build();

await host.RunAsync();
