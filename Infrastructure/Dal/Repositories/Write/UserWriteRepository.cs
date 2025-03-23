using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Write;

public class UserWriteRepository(DbContext context) : BaseWriteRepository<User>(context)
{
    
}