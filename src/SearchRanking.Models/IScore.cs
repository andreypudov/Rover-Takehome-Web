namespace SearchRanking.Models;

public interface IScore
{
    string Email { get; }

    string Name { get; }

    double ProfileScore { get; }

    double RatingsScore { get; }

    double SearchScore { get; }
}