using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetUserByIdQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await userReadRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}