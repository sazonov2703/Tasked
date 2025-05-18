using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Events;
using MediatR;

namespace Application.UseCases.Commands;

public class DeleteUserTaskCommandHandler(
    IUserTaskReadRepository taskReadRepository,
    IUserTaskWriteRepository taskWriteRepository) 
    : IRequestHandler<DeleteUserTaskCommand>
{
    public async Task Handle(DeleteUserTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await taskReadRepository.GetByIdAsync(request.TaskId, cancellationToken);
            
            if (task == null)
            {
                throw new TaskNotFoundException(request.TaskId);
            }

            if (task.UserId != request.UserId)
            {
                throw new UnauthorizedTaskAccessException(request.UserId, request.TaskId);
            }

            task.AddDomainEvent(new UserTaskDeletedEvent(task.Id, task.UserId));
            await taskWriteRepository.DeleteAsync(task.Id, cancellationToken);
            await taskWriteRepository.SaveChangesAsync(cancellationToken);
        }
        catch (TaskNotFoundException)
        {
            throw;
        }
        catch (UnauthorizedTaskAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new TaskedException($"Failed to delete task {request.TaskId}. Error: {ex.Message}", ex);
        }
    }
} 