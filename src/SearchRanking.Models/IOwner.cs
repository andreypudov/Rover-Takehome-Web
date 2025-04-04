namespace SearchRanking.Models;

public interface IOwner
{
    string OwnerImage { get; }

    string Owner { get; }

    string OwnerPhoneNumber { get; }

    string OwnerEmail { get; }
}