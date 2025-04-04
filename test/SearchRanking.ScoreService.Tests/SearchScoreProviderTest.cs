using NUnit.Framework;
using SearchRanking.Models;
using SearchRanking.ScoreService.Tests.TestData;

namespace SearchRanking.ScoreService.Tests;

[TestFixture]
public sealed class SearchScoreProviderTest
{
    private readonly SearchScoreProvider provider = new();

    /* 13 distinct characters produces profile core equals 2.5 */
    private const string SitterName25 = "abcdefghijklm";
    private const string SitterName25_2 = "nopqrstuvwxyz";

    /* 26 distinct characters produces profile core equals 3.78 */
    private const string SitterName38 = "abcdefghijklmnopqrs";

    /* 26 distinct characters produces profile core equals 5.0 */
    private const string SitterName50 = "abcdefghijklmnopqrstuvwxyz";

    [Test]
    public void SearchScoreProvider_Compute_ShouldReturnProfileScore_WhenSitterHasNoStays()
    {
        const string sitterName = "Alice";

        var actualScore = provider.Compute([]);

        Assert.False(actualScore.ContainsKey(sitterName));
    }

    [Test]
    public void SearchScoreProvider_ComputeWithRatingsScore_ShouldReturnProfileScore_WhenSitterHasNoStays()
    {
        const string sitterName = "Alice";

        var actualScore = provider.Compute([], new Dictionary<string, double>());

        Assert.False(actualScore.ContainsKey(sitterName));
    }

    [TestCase(1, 2.75)]
    [TestCase(2, 3.0)]
    [TestCase(3, 3.25)]
    [TestCase(4, 3.5)]
    [TestCase(5, 3.75)]
    [TestCase(6, 4.0)]
    [TestCase(7, 4.25)]
    [TestCase(8, 4.5)]
    [TestCase(9, 4.75)]
    [TestCase(10,5.0)]
    [TestCase(11, 5.0)]
    [TestCase(12, 5.0)]
    public void SearchScoreProvider_Compute_ShouldReturnProfileScore_WhenSitterHasGivenStays(int numberOfStays, double expectedScore)
    {
        var actualScore = provider.Compute(
            Enumerable.Repeat(CommonTestData.GeneralReview with { Rating = 5, Sitter = SitterName25 }, numberOfStays)
        );

        Assert.True(actualScore.ContainsKey(SitterName25));
        Assert.That(actualScore[SitterName25], Is.EqualTo(expectedScore).Within(0.01));
    }

    [TestCase(1, 2.75)]
    [TestCase(2, 3.0)]
    [TestCase(3, 3.25)]
    [TestCase(4, 3.5)]
    [TestCase(5, 3.75)]
    [TestCase(6, 4.0)]
    [TestCase(7, 4.25)]
    [TestCase(8, 4.5)]
    [TestCase(9, 4.75)]
    [TestCase(10,5.0)]
    [TestCase(11, 5.0)]
    [TestCase(12, 5.0)]
    public void SearchScoreProvider_ComputeWithRatingsScore_ShouldReturnProfileScore_WhenSitterHasGivenStays(int numberOfStays, double expectedScore)
    {
        var ratingsScore = new Dictionary<string, double>()
        {
            { SitterName25, 5 }
        };
        var actualScore = provider.Compute(
            Enumerable.Repeat(CommonTestData.GeneralReview with { Rating = 5, Sitter = SitterName25 }, numberOfStays),
            ratingsScore
        );

        Assert.True(actualScore.ContainsKey(SitterName25));
        Assert.That(actualScore[SitterName25], Is.EqualTo(expectedScore).Within(0.01));
    }

    [Test]
    public void SearchScoreProvider_Compute_ShouldSortBySearchScoreDescending()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Sitter = SitterName25 },
            CommonTestData.GeneralReview with { Sitter = SitterName38 },
            CommonTestData.GeneralReview with { Sitter = SitterName50 },
        };

        var result = provider.Compute(reviews);

        var sorted = result.OrderByDescending(kv => kv.Value).ToList();

        Assert.That(sorted[0].Key, Is.EqualTo(SitterName50));
        Assert.That(sorted[1].Key, Is.EqualTo(SitterName38));
        Assert.That(sorted[2].Key, Is.EqualTo(SitterName25));
    }

    [Test]
    public void SearchScoreProvider_Compute_ShouldUseAlphabeticalSortingAsTieBreaker()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Sitter = SitterName25 },
            CommonTestData.GeneralReview with { Sitter = SitterName25_2 },
            CommonTestData.GeneralReview with { Sitter = SitterName38 },
        };

        var result = provider.Compute(reviews);

        var sorted = result.OrderByDescending(kv => kv.Value).ToList();

        Assert.That(sorted[0].Key, Is.EqualTo(SitterName38));
        Assert.That(sorted[1].Key, Is.EqualTo(SitterName25));
        Assert.That(sorted[2].Key, Is.EqualTo(SitterName25_2));
    }

    [Test]
    public void SearchScoreProvider_Compute_ShouldHandleAllEqualScoresUsingAlphabeticalSorting()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Sitter = SitterName25 },
            CommonTestData.GeneralReview with { Sitter = SitterName25_2 },
        };

        var result = provider.Compute(reviews);

        var sorted = result.OrderByDescending(kv => kv.Value).ToList();

        Assert.That(sorted[0].Key, Is.EqualTo(SitterName25));
        Assert.That(sorted[1].Key, Is.EqualTo(SitterName25_2));
    }
}