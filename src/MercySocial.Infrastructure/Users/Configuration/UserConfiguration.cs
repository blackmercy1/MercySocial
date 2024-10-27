using MercySocial.Domain.UserAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercySocial.Infrastructure.Users.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
    }

    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");
        
        builder
            .HasKey(u => u.Id);
        
        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
        
        builder
            .Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.ProfileImageUrl)
            .HasMaxLength(255);

        builder
            .Property(u => u.DateOfBirth)
            .IsRequired();

        builder
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder
            .Property(u => u.LastLogin);

        builder
            .Property(u => u.Bio)
            .HasMaxLength(500);

        builder
            .Property(u => u.IsActive)
            .IsRequired();
    }
}