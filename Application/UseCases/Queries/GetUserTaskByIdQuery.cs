using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetUserTaskByIdQuery(Guid Id, Guid UserId) : IRequest<UserTask?>;
