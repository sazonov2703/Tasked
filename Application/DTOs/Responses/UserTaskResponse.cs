using Domain.Entities;
using Domain.ValueObjects;

namespace Application.DTOs.Responses;

public record UserTaskResponse(Guid Id, string Title, string Description, Status Status, Priority Priority)
{
    public UserTaskResponse(UserTask task) : this(task.Id, task.Title, task.Description, task.Status, task.Priority) { }
} 