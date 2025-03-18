using MediatR;

namespace Application.UseCases.Commands;

public record CreateUserCommand(string username) : IRequest<Guid>;