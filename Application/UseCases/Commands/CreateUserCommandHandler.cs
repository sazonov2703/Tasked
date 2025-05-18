using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateUserCommandHandler(
    IUserWriteRepository userWriteRepository,
    IUserReadRepository userReadRepository) 
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if user with this email already exists
            var existingUsers = await userReadRepository.FindAsync(
                u => u.Email == request.Email,
                cancellationToken
            );

            if (existingUsers.Any())
            {
                throw new DuplicateEmailException(request.Email);
            }

            var user = new User(request.Username, request.Email, request.PasswordHash);
            
            await userWriteRepository.AddAsync(user, cancellationToken);
            await userWriteRepository.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
        catch (DuplicateEmailException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new TaskedException($"Failed to create user. Error: {ex.Message}", ex);
        }
    }
}