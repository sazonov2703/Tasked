using MediatR;

namespace Application.UseCases.Commands;

public record CreateUserCommand(string Username, string Email, string PasswordHash) : IRequest<Guid>;