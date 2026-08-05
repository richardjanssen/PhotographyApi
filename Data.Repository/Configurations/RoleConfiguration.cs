using Business.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class RoleConfiguration : EntityBaseConfiguration<Role>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Name).HasMaxLength(128);
    }
}
