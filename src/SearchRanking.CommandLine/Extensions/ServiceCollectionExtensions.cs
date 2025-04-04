using SearchRanking.CommandLine.Services;
using SearchRanking.CsvModels;
using SearchRanking.CsvRepository;

namespace SearchRanking.CommandLine.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services
            .AddScoped<ISearchRankingRepository, SearchRankingRepository>()
            .AddHostedService<SearchRankingService>();

    public static IServiceCollection AddCsv(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CsvSettings>(configuration);
}