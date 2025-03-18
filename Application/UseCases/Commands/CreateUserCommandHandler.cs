using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateUserCommandHandler(
    IUserWriteRepository userWriteRepository) 
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.username);
        
        await userWriteRepository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}