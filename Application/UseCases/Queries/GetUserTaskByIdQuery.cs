using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetUserTaskByIdQuery(Guid Id) : IRequest<UserTask>;
