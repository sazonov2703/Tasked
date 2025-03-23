using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal;

public class TaskedDbContext : DbContext
{
    public DbSet<UserTask> UserTasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("", (options) =>
        {
            options.CommandTimeout(10);
        });
    }
}