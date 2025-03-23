using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserReadRepository(DbContext context) : BaseReadRepository<User>(context)
{
    
}