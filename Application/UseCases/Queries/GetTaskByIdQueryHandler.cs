using Application.Interfaces.Repositories.Read;
using MediatR;

namespace Application.UseCases.Queries;

public class GetTaskByIdQueryHandler(IUserTaskReadRepository userTaskReadRepository)
    : IRequestHandler<GetTaskByIdQuery, Task>
{
    public async Task<Task> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return userTaskReadRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}