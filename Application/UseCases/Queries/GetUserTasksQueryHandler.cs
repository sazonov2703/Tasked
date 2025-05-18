using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetUserTasksQueryHandler(
    IUserTaskReadRepository taskReadRepository) 
    : IRequestHandler<GetUserTasksQuery, IEnumerable<UserTask>>
{
    public async Task<IEnumerable<UserTask>> Handle(GetUserTasksQuery request, CancellationToken cancellationToken)
    {
        return await taskReadRepository.FindAsync(
            task => task.UserId == request.UserId,
            cancellationToken
        );
    }
} 