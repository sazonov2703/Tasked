using Domain.Entities;
using Infrastructure.Dal.Repositories.Read;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Write;

public class UserTaskWriteRepository(DbContext context) : BaseReadRepository<UserTask>(context)
{
    
}