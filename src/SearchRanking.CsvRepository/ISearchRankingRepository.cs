using SearchRanking.CsvModels;

namespace SearchRanking.CsvRepository;

public interface ISearchRankingRepository
{
    Task<IReadOnlyList<ReviewCsv>> ReadAsync(CancellationToken cancellationToken);

    Task WriteAsync(IReadOnlyList<ScoreCsv> scores, CancellationToken cancellationToken);
}