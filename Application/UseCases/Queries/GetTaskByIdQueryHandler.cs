using Application.Interfaces.Repository.Read;
using MediatR;

namespace Application.UseCases.Queries;

public class GetTaskByIdQueryHandler(ITaskReadRepository taskReadRepository)
    : IRequestHandler<GetTaskByIdQuery, Task>
{
    public async Task<Task> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await taskReadRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}