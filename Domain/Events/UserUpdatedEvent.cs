using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Events;

/// <summary>
/// Событие обновления пользователя.
/// </summary>
public class UserUpdatedEvent : IDomainEvent
{
    public UserUpdatedEvent(
        Guid userId,
        string newUsername,
        string newEmail,
        string newPasswordHash,
        string oldUsername,
        string oldEmail,
        string oldPasswordHash)
    {
        UserId = userId;
        NewUsername = newUsername;
        NewEmail = newEmail;
        NewPasswordHash = newPasswordHash;
        OldUsername = oldUsername;
        OldEmail = oldEmail;
        OldPasswordHash = oldPasswordHash;
        OccurredAt = DateTime.UtcNow;
    }

    public Guid UserId { get; init; }
    public string NewUsername { get; init; }
    public string NewEmail { get; init; }
    public string NewPasswordHash { get; init; }
    public string OldUsername { get; init; }
    public string OldEmail { get; init; }
    public string OldPasswordHash { get; init; }
    public DateTime OccurredAt { get; init; }
}