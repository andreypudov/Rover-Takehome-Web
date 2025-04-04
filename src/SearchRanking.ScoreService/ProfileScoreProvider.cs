using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public sealed class ProfileScoreProvider : IScoreProvider
{
    private readonly FractionScoreProvider fractionScoreProvider = new();

    private const int FractionScoreMultiplier = 5;

    public double Compute(Review review)
    {
        var fractionScore = fractionScoreProvider.Compute(review);

        return fractionScore * FractionScoreMultiplier;
    }
}