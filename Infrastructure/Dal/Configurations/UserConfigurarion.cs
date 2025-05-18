using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class UserConfigurarion : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName(nameof(User.Username));

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName(nameof(User.Email));

        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.HasMany(x => x.UserTasks)
            .WithOne(T => T.User)
            .HasForeignKey(T => T.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}