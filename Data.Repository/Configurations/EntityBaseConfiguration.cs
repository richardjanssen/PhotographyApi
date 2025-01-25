using Business.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.RowVersion).IsConcurrencyToken();
        builder.Property(x => x.DateModifiedUtc).HasConversion(new UtcDateTimeConverter());
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}
