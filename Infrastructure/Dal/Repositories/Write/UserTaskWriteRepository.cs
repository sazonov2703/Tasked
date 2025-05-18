using Domain.Entities;
using Application.Interfaces.Repositories.Write;
using Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Write;

public class UserTaskWriteRepository(TaskedDbContext context) : BaseWriteRepository<UserTask>(context), IUserTaskWriteRepository
{
    
}