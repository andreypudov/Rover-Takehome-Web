using NUnit.Framework;
using SearchRanking.Models;
using SearchRanking.ScoreService.Tests.TestData;

namespace SearchRanking.ScoreService.Tests;

[TestFixture]
public sealed class RatingsScoreProviderTest
{
    private readonly RatingsScoreProvider provider = new();

    [Test]
    public void ProfileScoreProvider_Compute_ShouldReturnCorrectAverages_WhenMultipleSitterReviewsExist()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Rating = 5, Sitter = "Alice" },
            CommonTestData.GeneralReview with { Rating = 3, Sitter = "Alice" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Bob" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Alice" },
            CommonTestData.GeneralReview with { Rating = 5, Sitter = "Bob" }
        };

        var expectedScores = new Dictionary<string, double>
        {
            { "Alice", (5 + 3 + 4) / 3.0 },
            { "Bob", (4 + 5) / 2.0 }
        };

        var actualScores = provider.Compute(reviews);
        
        Assert.AreEqual(expectedScores["Alice"], actualScores["Alice"]);
        Assert.AreEqual(expectedScores["Bob"], actualScores["Bob"]);
    }
    
    [Test]
    public void ProfileScoreProvider_Compute_ShouldReturnEmptyDictionary_WhenNoReviewsProvided()
    {
        var reviews = new List<Review>();
        
        var actualScores = provider.Compute(reviews);
        
        Assert.IsEmpty(actualScores);
    }
    
    [Test]
    public void ProfileScoreProvider_Compute_ShouldHandleSingleSitterReviewCorrectly()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Rating = 5, Sitter = "Alice" },
        };

        var actualScores = provider.Compute(reviews);
        
        Assert.That(actualScores, Has.Count.EqualTo(1));
        Assert.AreEqual(5.0, actualScores["Alice"]);
    }
    
    [Test]
    public void ProfileScoreProvider_Compute_ShouldHandleMultipleSittersWithIdenticalScores()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Alice", Text = "1" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Alice", Text = "2" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Bob", Text = "1" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Bob", Text = "2" }
        };

        var actualScores = provider.Compute(reviews);
        
        Assert.AreEqual(4.0, actualScores["Alice"]);
        Assert.AreEqual(4.0, actualScores["Bob"]);
    }
    
    [Test]
    public void ProfileScoreProvider_Compute_ShouldHandleDuplicateReviewsCorrectly()
    {
        var reviews = new List<Review>
        {
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Alice", Text = "1" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Alice", Text = "1" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Bob", Text = "1" },
            CommonTestData.GeneralReview with { Rating = 4, Sitter = "Bob", Text = "1" }
        };

        var actualScores = provider.Compute(reviews);
        
        Assert.AreEqual(4.0, actualScores["Alice"]);
        Assert.AreEqual(4.0, actualScores["Bob"]);
    }
}