using Business.Entities.Groceries;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class GroceryListProductConfiguration : EntityBaseConfiguration<GroceryListProduct>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GroceryListProduct> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(256);
    }
}
