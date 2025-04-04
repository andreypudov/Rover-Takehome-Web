using SearchRanking.CsvRepository;
using SearchRanking.Models;
using SearchRanking.Models.Mapper;
using SearchRanking.ScoreService;

namespace SearchRanking.CommandLine.Services;

public class SearchRankingService(
    ISearchRankingRepository searchRankingRepository,
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<SearchRankingService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("SearchRankingService is starting.");

        var reviews = await ReadReviewsAsync(cancellationToken);

        /* ensure correct sorting */
        var scores = ComputeScores(reviews)
            .OrderByDescending(s => s.SearchScore)
            .ThenBy(s => s.Name)
            .ToList();
        await WriteScoresAsync(scores, cancellationToken);

        logger.LogInformation("SearchRankingService is done.");
        hostApplicationLifetime.StopApplication();
    }

    private static IReadOnlyList<Score> ComputeScores(IReadOnlyList<Review> reviews)
    {
        var profileScoreProvider = new ProfileScoreProvider();
        var ratingScoreProvider = new RatingsScoreProvider();
        var searchScoreProvider = new SearchScoreProvider();

        var reviewsBySitters = reviews.GroupBy(r => r.Sitter);

        return [.. (from reviewsForSitter in reviewsBySitters
            let firstReview = reviewsForSitter.First()
            let profileScore = profileScoreProvider.Compute(firstReview)
            let ratingScore = ratingScoreProvider.Compute(reviewsForSitter)
            let searchScore = searchScoreProvider.Compute(reviewsForSitter, ratingScore)

            select new Score(
                Email: firstReview.SitterEmail,
                Name: firstReview.Sitter,
                ProfileScore: profileScore,
                RatingsScore: ratingScore.Values.First(),
                SearchScore: searchScore.Values.First()))];
    }

    private async Task<IReadOnlyList<Review>> ReadReviewsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("SearchRankingService is reading reviews.");
        try
        {
            var reviews = await searchRankingRepository.ReadAsync(cancellationToken);
            return [.. reviews.Select(CsvMapper.Map)];
        }
        catch (Exception e)
        {
            logger.LogError("Could not read reviews: {error}", e);
            return [];
        }
    }

    private async Task WriteScoresAsync(IReadOnlyList<Score> scores, CancellationToken cancellationToken)
    {
        logger.LogInformation("SearchRankingService is writing scores.");
        try
        {
            await searchRankingRepository.WriteAsync([.. scores.Select(CsvMapper.Map)], cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError("Could not write scores: {error}", e);
        }
    }
}