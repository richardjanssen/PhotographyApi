using Business.Entities.Recipes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class IngredientConfiguration : EntityBaseConfiguration<Ingredient>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Ingredient> builder)
    {
        builder.Property(r => r.Name).HasMaxLength(256);
        builder.Property(r => r.Quantity).HasMaxLength(10);
        builder.Property(r => r.Unit).HasMaxLength(64);
        builder.Property(r => r.Subgroup).HasMaxLength(64);

    }
}
