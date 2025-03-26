using Domain.Entities;
using Infrastructure.Dal.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal;

public class TaskedDbContext : DbContext
{
    public DbSet<TodoTask> UserTasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfigurarion());
        modelBuilder.ApplyConfiguration(new TodoTaskConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("", (options) =>
        {
            options.CommandTimeout(10);
        });
    }
}