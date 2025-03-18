using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateTaskCommandHandler(
    IUserReadRepository userReadRepository,
    IUserTaskWriteRepository userTaskWriteRepository) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NullReferenceException();
        }
        
        var userTask = new UserTask(request.Name, request.Description, request.UserId);
        
        await userTaskWriteRepository.AddAsync(userTask, cancellationToken);
        
        return userTask.Id;
    }
}