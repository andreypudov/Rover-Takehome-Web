using CsvHelper.Configuration.Attributes;

namespace SearchRanking.CsvModels;

public sealed class ScoreCsv
{
    [Name("email")]
    public string Email { get; init; } = string.Empty;

    [Name("name")]
    public string Name { get; init; } = string.Empty;

    [Name("profile_score")]
    public double ProfileScore { get; init; } = 0.0;

    [Name("ratings_score")]
    public double RatingsScore { get; init; } = 0.0;

    [Name("search_score")]
    public double SearchScore { get; init; } = 0.0;
}