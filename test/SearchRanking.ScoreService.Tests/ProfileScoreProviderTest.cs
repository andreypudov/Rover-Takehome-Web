using NUnit.Framework;
using SearchRanking.Models;
using SearchRanking.ScoreService.Tests.TestData;

namespace SearchRanking.ScoreService.Tests;

[TestFixture]
public sealed class ProfileScoreProviderTest
{
    private readonly ProfileScoreProvider provider = new();

    [TestCaseSource(typeof(ProfileScoreProviderTestData), nameof(ProfileScoreProviderTestData.Basic))]
    [TestCaseSource(typeof(ProfileScoreProviderTestData), nameof(ProfileScoreProviderTestData.Edge))]
    [TestCaseSource(typeof(ProfileScoreProviderTestData), nameof(ProfileScoreProviderTestData.MixedCases))]
    [TestCaseSource(typeof(ProfileScoreProviderTestData), nameof(ProfileScoreProviderTestData.Performance))]
    public void ProfileScoreProvider_Compute_ShouldReturnCorrectScore(Review review, double expectedScore) =>
        Assert.That(provider.Compute(review), Is.EqualTo(expectedScore).Within(0.00001));
}