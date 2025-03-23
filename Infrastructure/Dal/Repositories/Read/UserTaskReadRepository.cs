using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserTaskReadRepository(DbContext context) : BaseReadRepository<UserTask>(context)
{
    
}