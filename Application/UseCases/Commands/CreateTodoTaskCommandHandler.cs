using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateTodoTaskCommandHandleк(
    IUserReadRepository userReadRepository,
    ITodoTaskWriteRepository todoTaskWriteRepository) : IRequestHandler<CreateTodoTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var user = userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NullReferenceException();
        }
        
        var userTask = new TodoTask(request.Title, request.Description, request.Status, request.Priority, request.UserId);
        
        await todoTaskWriteRepository.AddAsync(userTask, cancellationToken);
        
        return userTask.Id;
    }
}