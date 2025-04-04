using NUnit.Framework;
using SearchRanking.Models;
using SearchRanking.ScoreService.Tests.TestData;

namespace SearchRanking.ScoreService.Tests;

[TestFixture]
public sealed class DistinctLettersScoreProviderTest
{
    private readonly DistinctLettersScoreProvider provider = new();

    [TestCaseSource(typeof(DistinctLettersScoreProviderTestData), nameof(DistinctLettersScoreProviderTestData.Basic))]
    [TestCaseSource(typeof(DistinctLettersScoreProviderTestData), nameof(DistinctLettersScoreProviderTestData.Edge))]
    [TestCaseSource(typeof(DistinctLettersScoreProviderTestData), nameof(DistinctLettersScoreProviderTestData.MixedCases))]
    [TestCaseSource(typeof(DistinctLettersScoreProviderTestData), nameof(DistinctLettersScoreProviderTestData.Performance))]
    public void DistinctLettersScoreProvider_Compute_ShouldReturnCorrectScore(Review review, double expectedScore) =>
        Assert.That(provider.Compute(review), Is.EqualTo(expectedScore).Within(0.00001));
}