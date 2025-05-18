using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Events;

/// <summary>
/// Событие обновления задачи пользователя.
/// </summary>
public class UserTaskUpdatedEvent : IDomainEvent
{
    public UserTaskUpdatedEvent(
        Guid userTaskId, 
        string newTitle, 
        string newDescription, 
        Status newStatus, 
        Priority newPriority,
        string oldTitle, 
        string oldDescription, 
        Status oldStatus, 
        Priority oldPriority)
    {
        UserTaskId = userTaskId;
        NewTitle = newTitle;
        NewDescription = newDescription;
        NewStatus = newStatus;
        NewPriority = newPriority;
        OldTitle = oldTitle;
        OldDescription = oldDescription;
        OldStatus = oldStatus;
        OldPriority = oldPriority;
        OccurredAt = DateTime.UtcNow;
    }

    public Guid UserTaskId { get; init; }
    public string NewTitle { get; init; }
    public string NewDescription { get; init; }
    public Status NewStatus { get; init; }
    public Priority NewPriority { get; init; }
    public string OldTitle { get; init; }
    public string OldDescription { get; init; }
    public Status OldStatus { get; init; }
    public Priority OldPriority { get; init; }
    public DateTime OccurredAt { get; init; }
}