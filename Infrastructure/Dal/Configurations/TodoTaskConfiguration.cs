using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.ToTable(nameof(TodoTask));
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.OwnsOne(x => x.Status, Status =>
        {
            Status.Property(x => x.Value)
                .HasMaxLength(10);
        });
        
        builder.OwnsOne(x => x.Priority, Priority =>
        {
            Priority.Property(x => x.Value)
                .HasMaxLength(10);
        });
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}