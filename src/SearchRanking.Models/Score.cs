namespace SearchRanking.Models;

public record Score(
    string Email,
    string Name,
    double ProfileScore,
    double RatingsScore,
    double SearchScore) : IScore;