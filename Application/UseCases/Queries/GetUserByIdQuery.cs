using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<User>;
