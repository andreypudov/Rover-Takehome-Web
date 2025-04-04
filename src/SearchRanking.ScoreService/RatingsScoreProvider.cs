using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public sealed class RatingsScoreProvider : IAggregateScoreProvider
{
    public IDictionary<string, double> Compute(IEnumerable<Review> reviews) =>
        reviews
            .Distinct()
            .GroupBy(r => r.Sitter)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp
                    .Average(r => r.Rating));
}