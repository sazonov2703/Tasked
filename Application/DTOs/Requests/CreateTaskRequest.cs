using Domain.ValueObjects;

namespace Application.DTOs.Requests;

public record CreateTaskRequest(string Title, string Description, string Priority); 