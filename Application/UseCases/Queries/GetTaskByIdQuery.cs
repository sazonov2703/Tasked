using MediatR;

namespace Application.UseCases.Queries;

public record GetTaskByIdQuery(Guid Id) : IRequest<Task>;
