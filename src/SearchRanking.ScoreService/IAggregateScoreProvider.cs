using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public interface IAggregateScoreProvider
{
    IDictionary<string, double> Compute(IEnumerable<Review> reviews);
}