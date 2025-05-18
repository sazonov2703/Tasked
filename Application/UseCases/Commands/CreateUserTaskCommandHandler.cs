using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateUserTaskCommandHandler(
    IUserReadRepository userReadRepository,
    IUserTaskWriteRepository userTaskWriteRepository) 
    : IRequestHandler<CreateUserTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }
            
            var userTask = new UserTask(request.Title, request.Description, request.Status, request.Priority, request.UserId);
            
            await userTaskWriteRepository.AddAsync(userTask, cancellationToken);
            await userTaskWriteRepository.SaveChangesAsync(cancellationToken);
            
            return userTask.Id;
        }
        catch (UserNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new TaskedException($"Failed to create task. Error: {ex.Message}", ex);
        }
    }
}