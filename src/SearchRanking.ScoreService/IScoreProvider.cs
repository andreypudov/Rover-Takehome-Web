using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public interface IScoreProvider
{
   double Compute(Review review);
}