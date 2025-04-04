using SearchRanking.Models;

namespace SearchRanking.ScoreService.Tests.TestData;

public static class CommonTestData
{
    public static readonly Review GeneralReview = new(
        5,
        "https://example.com/sitter.png",
        DateTimeOffset.Parse("2013-04-08"),
        "Lorem ipsum.",
        "https://example.com/owner.png",
        "Pinot Grigio",
        "Lauren B.",
        "Shelli K.",
        DateTimeOffset.Parse("2013-02-26"),
        "+00000000000",
        "sitter@example.com",
        "+00000000000",
        "owner@example.com",
        2);
}