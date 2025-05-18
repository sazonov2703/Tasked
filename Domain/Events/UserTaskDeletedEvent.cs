using Domain.Interfaces;

namespace Domain.Events;

/// <summary>
/// Событие удаления задачи пользователя.
/// </summary>
public class UserTaskDeletedEvent : IDomainEvent
{
    public UserTaskDeletedEvent(Guid userTaskId, Guid userId)
    {
        Id = userTaskId;
        UserId = userId;
        OccurredAt = DateTime.UtcNow;
    }

    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime OccurredAt { get; init; }
} 