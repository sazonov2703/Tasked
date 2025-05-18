using MediatR;

namespace Application.UseCases.Commands;

public record DeleteUserTaskCommand(Guid TaskId, Guid UserId) : IRequest; 