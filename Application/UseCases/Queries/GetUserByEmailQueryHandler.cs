using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetUserByEmailQueryHandler(
    IUserReadRepository userReadRepository) 
    : IRequestHandler<GetUserByEmailQuery, User?>
{
    public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var users = await userReadRepository.FindAsync(
            user => user.Email == request.Email,
            cancellationToken
        );
        
        return users.FirstOrDefault();
    }
} 