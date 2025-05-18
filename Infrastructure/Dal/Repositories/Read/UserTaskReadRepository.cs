using Domain.Entities;
using Application.Interfaces.Repositories.Read;
using Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserTaskReadRepository(TaskedDbContext context) : BaseReadRepository<UserTask>(context), IUserTaskReadRepository
{
    
}