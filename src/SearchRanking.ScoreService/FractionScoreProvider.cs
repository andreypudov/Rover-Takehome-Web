using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public sealed class FractionScoreProvider : IScoreProvider
{
    private readonly DistinctLettersScoreProvider distinctLettersScoreProvider = new();

    private const int LettersInEnglishAlphabetCount = 26;

    public double Compute(Review review)
    {
        var distinctLettersScore = distinctLettersScoreProvider.Compute(review);

        return distinctLettersScore / LettersInEnglishAlphabetCount;
    }
}