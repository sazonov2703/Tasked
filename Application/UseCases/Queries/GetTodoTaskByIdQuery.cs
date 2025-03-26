using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetTodoTaskByIdQuery(Guid Id) : IRequest<TodoTask>;
