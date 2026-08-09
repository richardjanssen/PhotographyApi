using Business.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).HasMaxLength(128);
        builder.Property(u => u.PasswordHash).HasMaxLength(256);
        builder.Property(u => u.PasswordSalt).HasMaxLength(256);

        builder.HasMany(rt => rt.RefreshTokens)
    .WithOne()
    .HasForeignKey(rt => rt.UserId)
    .IsRequired(true)  // Required
    .OnDelete(DeleteBehavior.Cascade);
    }
}
