using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Events;

public class UserUpdatedEvent : IDomainEvent
{
    public UserUpdatedEvent(Guid userId, string newUsername, string newEmail, string newPasswordHash,
        string oldUsername, string oldEmail, string oldPasswordHash)
    {
        UserId = userId;
        NewUsername = newUsername;
        NewEmail = newEmail;
        NewPasswordHash = newPasswordHash;
        OldUsername = oldUsername;
        OldEmail = oldEmail;
        OldPasswordHash = oldPasswordHash;
        UpdatenAt = DateTime.Now;
    }

    private Guid UserId { get; set; }
    private string NewUsername { get; set; }
    private string NewEmail { get; set; }
    private string NewPasswordHash { get; set; }
    private string OldUsername { get; set; }
    private string OldEmail { get; set; }
    private string OldPasswordHash { get; set; }
    DateTime UpdatenAt { get; set; }
}