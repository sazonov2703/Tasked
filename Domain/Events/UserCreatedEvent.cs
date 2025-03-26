using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Events;

public class UserCreatedEvent : IDomainEvent
{
    public UserCreatedEvent(Guid userId, string username, string email, string passwordHash)
    {
        UserId = userId;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; protected set; }
}