using Domain.ValueObjects;

namespace Application.DTOs.Requests;

public record UpdateTaskRequest(string Title, string Description, Status Status, Priority Priority); 