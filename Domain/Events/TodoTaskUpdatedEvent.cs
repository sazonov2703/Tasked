using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Events;

public class TodoTaskUpdatedEvent : IDomainEvent
{
    public TodoTaskUpdatedEvent(Guid todoTaskId, string newTitle, string newDescription, Status newStatus, Priority newPriority,
        string oldTitle, string oldDescription, Status oldStatus, Priority oldPriority)
    {
        TodoTaskId = todoTaskId;
        NewTitle = newTitle;
        NewDescription = newDescription;
        NewStatus = newStatus;
        NewPriority = newPriority;
        OldTitle = oldTitle;
        OldDescription = oldDescription;
        OldStatus = oldStatus;
        OldPriority = oldPriority;
        UpdatedAt = DateTime.Now;
    }

    private Guid TodoTaskId { get; set; }
    string NewTitle { get; set; }
    string NewDescription { get; set; }
    Status NewStatus { get; set; }
    Priority NewPriority { get; set; }
    string OldTitle { get; set; }
    string OldDescription { get; set; }
    Status OldStatus { get; set; }
    Priority OldPriority { get; set; }
    DateTime UpdatedAt { get; set; }
}