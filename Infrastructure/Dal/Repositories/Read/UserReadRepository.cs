using Domain.Entities;
using Application.Interfaces.Repositories.Read;
using Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserReadRepository(TaskedDbContext context) : BaseReadRepository<User>(context), IUserReadRepository
{
    
}