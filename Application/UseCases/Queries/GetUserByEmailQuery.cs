using Domain.Entities;
using MediatR;

namespace Application.UseCases.Queries;

public record GetUserByEmailQuery(string Email) : IRequest<User?>; 