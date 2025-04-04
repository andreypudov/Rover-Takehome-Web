using NUnit.Framework;
using SearchRanking.Models;
using SearchRanking.ScoreService.Tests.TestData;

namespace SearchRanking.ScoreService.Tests;

[TestFixture]
public sealed class FractionScoreProviderTest
{
    private readonly FractionScoreProvider provider = new();

    [TestCaseSource(typeof(FractionScoreProviderTestData), nameof(FractionScoreProviderTestData.Basic))]
    [TestCaseSource(typeof(FractionScoreProviderTestData), nameof(FractionScoreProviderTestData.Edge))]
    [TestCaseSource(typeof(FractionScoreProviderTestData), nameof(FractionScoreProviderTestData.MixedCases))]
    [TestCaseSource(typeof(FractionScoreProviderTestData), nameof(FractionScoreProviderTestData.Performance))]
    public void FractionScoreProvider_Compute_ShouldReturnCorrectScore(Review review, double expectedScore) =>
        Assert.That(provider.Compute(review), Is.EqualTo(expectedScore).Within(0.00001));
}