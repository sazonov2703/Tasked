using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetUserTaskByIdQueryHandler(
    IUserTaskReadRepository userTaskReadRepository)
    : IRequestHandler<GetUserTaskByIdQuery, UserTask?>
{
    public async Task<UserTask?> Handle(GetUserTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await userTaskReadRepository.GetByIdAsync(request.Id, cancellationToken);
        return task?.UserId == request.UserId ? task : null;
    }
}