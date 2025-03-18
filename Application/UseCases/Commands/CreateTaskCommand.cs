using MediatR;

namespace Application.UseCases.Commands;

public record CreateTaskCommand(string Name, string Description, Guid UserId) : IRequest<Guid>;