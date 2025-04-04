namespace SearchRanking.Models;

public record Review(int Rating,
    string SitterImage,
    DateTimeOffset EndDate,
    string Text,
    string OwnerImage,
    string Dogs,
    string Sitter,
    string Owner,
    DateTimeOffset StartDate,
    string SitterPhoneNumber,
    string SitterEmail,
    string OwnerPhoneNumber,
    string OwnerEmail,
    int ResponseTimeMinutes) : IReview, IOwner, ISitter, IPet;