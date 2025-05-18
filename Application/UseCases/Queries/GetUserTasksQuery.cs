using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetUserTasksQuery(Guid UserId) : IRequest<IEnumerable<UserTask>>; 