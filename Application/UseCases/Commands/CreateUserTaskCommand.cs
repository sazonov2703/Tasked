using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateUserTaskCommand(string Title, string Description, Status Status, Priority Priority, Guid UserId) : IRequest<Guid>;