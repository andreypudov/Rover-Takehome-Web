using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Options;
using SearchRanking.CsvModels;
using SearchRanking.CsvRepository.Converters;
using SearchRanking.CsvRepository.Validators;

namespace SearchRanking.CsvRepository;

public sealed class SearchRankingRepository(
    IOptions<CsvSettings> csvOptions,
    ILogger<SearchRankingRepository> logger) : ISearchRankingRepository
{
    private readonly IValidator reviewFileValidator = new ReviewFileValidator();
    private readonly IValidator scoreFileValidator = new ScoreFileValidator();
    
    public async Task<IReadOnlyList<ReviewCsv>> ReadAsync(CancellationToken cancellationToken)
    {
        if (reviewFileValidator.IsValid(csvOptions.Value.Reviews, logger) == false)
        {
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
        if (scoreFileValidator.IsValid(csvOptions.Value.Scores, logger) == false)
        {
            return;
        }

        await using var writer = new StreamWriter(csvOptions.Value.Scores);
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<ScoreCsvMap>();

        await csv.WriteRecordsAsync(scores, cancellationToken);
    }
}