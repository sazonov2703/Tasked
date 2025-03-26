using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public class GetTodoTaskByIdQueryHandler(
    ITodoTaskReadRepository todoTaskReadRepository)
    : IRequestHandler<GetTodoTaskByIdQuery, TodoTask>
{
    public async Task<TodoTask> Handle(GetTodoTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await todoTaskReadRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}