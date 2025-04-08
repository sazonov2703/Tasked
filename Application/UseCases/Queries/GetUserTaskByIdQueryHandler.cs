using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetUserTaskByIdQueryHandler(
    ITodoTaskReadRepository todoTaskReadRepository)
    : IRequestHandler<GetUserTaskByIdQuery, UserTask>
{
    public async Task<UserTask> Handle(GetUserTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await todoTaskReadRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}