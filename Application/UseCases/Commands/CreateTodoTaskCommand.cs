using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateTodoTaskCommand(string Title, string Description, Status Status, Priority Priority, Guid UserId) : IRequest<Guid>;