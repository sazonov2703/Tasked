using MediatR;

namespace Application.UseCases.Commands;

public record ChangeTaskStatusCommand(Guid TaskId, string NewStatus) : IRequest<Result>;