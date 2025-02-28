using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.IsEmailConfirmed)
            .IsRequired();
        
        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.IsActive)
            .IsRequired();
    }
}