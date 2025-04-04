using SearchRanking.Models;

namespace SearchRanking.ScoreService;

public sealed class SearchScoreProvider : IAggregateScoreProvider
{
    private readonly ProfileScoreProvider profileScoreProvider = new();
    private readonly RatingsScoreProvider ratingsScoreProvider = new();

    public IDictionary<string, double> Compute(IEnumerable<Review> reviews)
    {
        var reviewList = reviews.ToList();
        var ratingsScores = ratingsScoreProvider.Compute(reviewList);

        return Compute(reviewList, ratingsScores);
    }

    public IDictionary<string, double> Compute(IEnumerable<Review> reviews, IDictionary<string, double> ratingsScores)
    {
        var sitterReviews = reviews.GroupBy(r => r.Sitter)
            .ToDictionary(g => g.Key, g => g.ToList());;
        var searchScores = new Dictionary<string, double>();

        foreach (var (sitter, sitterReviewsList) in sitterReviews)
        {
            var stays = sitterReviewsList.Count;

            var profileScore = profileScoreProvider.Compute(sitterReviewsList.First());
            var ratingsScore = ratingsScores.TryGetValue(sitter, out var value) ? value : 0.0;

            double searchScore;

            if (stays >= 10)
            {
                searchScore = ratingsScore;
            }
            else
            {
                var profileWeight = (10 - stays) / 10.0;
                var ratingsWeight = stays / 10.0;

                searchScore = (profileWeight * profileScore) + (ratingsWeight * ratingsScore);
            }

            searchScores[sitter] = searchScore;
        }

        return searchScores;
    }
}