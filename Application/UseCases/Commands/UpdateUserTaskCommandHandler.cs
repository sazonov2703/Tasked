using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using MediatR;

namespace Application.UseCases.Commands;

public class UpdateUserTaskCommandHandler(
    IUserTaskReadRepository taskReadRepository,
    IUserTaskWriteRepository taskWriteRepository) 
    : IRequestHandler<UpdateUserTaskCommand>
{
    public async Task Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
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

            task.Update(
                request.Title,
                request.Description,
                request.Status,
                request.Priority
            );

            await taskWriteRepository.UpdateAsync(task, cancellationToken);
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
            throw new TaskedException($"Failed to update task {request.TaskId}. Error: {ex.Message}", ex);
        }
    }
} 