using CsvHelper.Configuration.Attributes;

namespace SearchRanking.CsvModels;

public sealed class ReviewCsv
{
    [Name("rating")] public int Rating { get; set; } = 0;

    [Name("sitter_image")]
    public string SitterImage { get; set; } = string.Empty;

    [Name("end_date")]
    public string EndDate { get; set; } = string.Empty;

    [Name("text")]
    public string Text { get; set; } = string.Empty;

    [Name("owner_image")]
    public string OwnerImage { get; set; } = string.Empty;

    [Name("dogs")]
    public string Dogs { get; set; } = string.Empty;

    [Name("sitter")]
    public string Sitter { get; set; } = string.Empty;

    [Name("owner")]
    public string Owner { get; set; } = string.Empty;

    [Name("start_date")]
    public string StartDate { get; set; } = string.Empty;

    [Name("sitter_phone_number")]
    public string SitterPhoneNumber { get; set; } = string.Empty;

    [Name("sitter_email")]
    public string SitterEmail { get; set; } = string.Empty;

    [Name("owner_phone_number")]
    public string OwnerPhoneNumber { get; set; } = string.Empty;

    [Name("owner_email")]
    public string OwnerEmail { get; set; } = string.Empty;

    [Name("response_time_minutes")]
    public int ResponseTimeMinutes { get; set; } = 0;
}