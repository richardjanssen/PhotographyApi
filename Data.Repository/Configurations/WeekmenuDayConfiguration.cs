using Business.Entities.Weekmenu;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class WeekmenuDayConfiguration : EntityBaseConfiguration<WeekmenuDay>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WeekmenuDay> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(256);
    }
}
