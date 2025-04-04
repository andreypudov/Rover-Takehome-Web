using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public sealed class DistinctLettersScoreProvider : IScoreProvider
{
    public double Compute(Review review) =>
        string.IsNullOrEmpty(review.Sitter)
            ? 0
            : review.Sitter
                .ToLower()
                .Where(char.IsLetter)
                .Distinct()
                .Count();
}