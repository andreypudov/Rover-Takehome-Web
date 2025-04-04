using SearchRanking.CommandLine.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddCore();
        services.AddCsv(configuration);
    }).Build();

await host.RunAsync();