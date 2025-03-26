using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Events;

public class TodoTaskCreatedEvent : IDomainEvent
{
    public TodoTaskCreatedEvent(Guid todoTaskId, string title, string description, Status status, Priority priority)
    {
        Id = todoTaskId;
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        CreatedAt = DateTime.Now;
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; private set; }
}