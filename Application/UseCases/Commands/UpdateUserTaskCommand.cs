using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands;

public record UpdateUserTaskCommand(
    Guid TaskId,
    Guid UserId,
    string Title,
    string Description,
    Status Status,
    Priority Priority) : IRequest; 