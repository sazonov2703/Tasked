using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Events;

public class UserTaskStatusChangedEvent : IDomainEvent
{
    public Guid TaskId { get; }
    public Status NewStatus { get; }
    public Status OldStatus { get; }
    public DateTime OccurredOn { get; }

    public UserTaskStatusChangedEvent(Guid taskId, Status newStatus, Status oldStatus)
    {
        TaskId = taskId;
        NewStatus = newStatus;
        OldStatus = oldStatus;
        OccurredOn = DateTime.UtcNow;
    }
} 