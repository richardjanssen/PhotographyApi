using Business.Entities.Groceries;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class GroceryListRecurringProductConfiguration : EntityBaseConfiguration<GroceryListRecurringProduct>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GroceryListRecurringProduct> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(256);
    }
}
