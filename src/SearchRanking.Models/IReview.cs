namespace SearchRanking.Models;

public interface IReview
{
    int Rating { get; }

    DateTimeOffset EndDate { get; }

    string Text { get; }

    DateTimeOffset StartDate { get; }

    int ResponseTimeMinutes { get; }
}