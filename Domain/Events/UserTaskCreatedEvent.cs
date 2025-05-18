using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Events;

/// <summary>
/// Событие создания задачи пользователя.
/// </summary>
public class UserTaskCreatedEvent : IDomainEvent
{
    public UserTaskCreatedEvent(Guid userTaskId, string title, string description, Status status, Priority priority)
    {
        Id = userTaskId;
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        OccurredAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public Status Status { get; init; }
    public Priority Priority { get; init; }
    public DateTime OccurredAt { get; init; }
}