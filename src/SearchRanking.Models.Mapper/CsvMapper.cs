using System.Globalization;
using SearchRanking.CsvModels;

namespace SearchRanking.Models.Mapper;

public static class CsvMapper
{
    private const string DateFormat = "yyyy-MM-dd";

    public static Review Map(ReviewCsv review) => new(
        review.Rating,
        review.SitterImage,
        DateTimeOffset.ParseExact(
            review.EndDate,
            DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal
        ),
        review.Text,
        review.OwnerImage,
        review.Dogs,
        review.Sitter,
        review.Owner,
        DateTimeOffset.ParseExact(
            review.StartDate,
            DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal
        ),
        review.SitterPhoneNumber,
        review.SitterEmail,
        review.OwnerPhoneNumber,
        review.OwnerEmail,
        review.ResponseTimeMinutes
    );

    public static ScoreCsv Map(Score score) => new()
    {
        Email = score.Email,
        Name = score.Name,
        ProfileScore = score.ProfileScore,
        RatingsScore = score.RatingsScore,
        SearchScore = score.SearchScore
    };
}