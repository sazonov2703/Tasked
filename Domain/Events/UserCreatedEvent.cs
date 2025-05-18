using Domain.Interfaces;

namespace Domain.Events;

/// <summary>
/// Событие создания пользователя.
/// </summary>
public class UserCreatedEvent : IDomainEvent
{
    public UserCreatedEvent(Guid userId, string username, string email, string passwordHash)
    {
        UserId = userId;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        OccurredAt = DateTime.UtcNow;
    }
    
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public DateTime OccurredAt { get; init; }
}