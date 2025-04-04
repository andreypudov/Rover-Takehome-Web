using CsvHelper.Configuration.Attributes;

namespace SearchRanking.CsvModels;

public sealed class ScoreCsv
{
    [Name("email")]
    public string Email { get; set; } = string.Empty;

    [Name("name")]
    public string Name { get; set; } = string.Empty;

    [Name("profile_score")]
    public double ProfileScore { get; set; } = 0.0;

    [Name("ratings_score")]
    public double RatingsScore { get; set; } = 0.0;

    [Name("search_score")]
    public double SearchScore { get; set; } = 0.0;
}