using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Result>
{
    private readonly IRepository<UserTask> _taskRepository;

    public ChangeTaskStatusCommandHandler(IRepository<UserTask> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
                return Result.Failure($"Task with ID {request.TaskId} not found");

            var newStatus = Status.FromString(request.NewStatus);
            task.ChangeStatus(newStatus);
            
            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();
            
            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to change task status: {ex.Message}");
        }
    }
} 