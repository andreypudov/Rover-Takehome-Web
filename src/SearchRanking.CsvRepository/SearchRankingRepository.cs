using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Options;
using SearchRanking.CsvModels;
using SearchRanking.CsvRepository.Converters;

namespace SearchRanking.CsvRepository;

public sealed class SearchRankingRepository(
    IOptions<CsvSettings> csvOptions,
    ILogger<SearchRankingRepository> logger) : ISearchRankingRepository
{
    public async Task<IReadOnlyList<ReviewCsv>> ReadAsync(CancellationToken cancellationToken)
    {
        if (File.Exists(csvOptions.Value.Reviews) == false)
        {
            logger.LogError("CSV file doesn't exist: {Path}", csvOptions.Value.Reviews);
            return [];
        }

        using var reader = new StreamReader(csvOptions.Value.Reviews);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var reviews = csv.GetRecordsAsync<ReviewCsv>(cancellationToken);
        var result = new List<ReviewCsv>();

        await foreach (var review in reviews)
        {
            result.Add(review);
        }

        return result.AsReadOnly();
    }

    public async Task WriteAsync(IReadOnlyList<ScoreCsv> scores, CancellationToken cancellationToken)
    {
        if (File.Exists(csvOptions.Value.Scores))
        {
            logger.LogError("CSV is already exists: {Path}", csvOptions.Value.Scores);
        }

        await using var writer = new StreamWriter(csvOptions.Value.Scores);
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<ScoreCsvMap>();

        await csv.WriteRecordsAsync(scores, cancellationToken);
    }
}