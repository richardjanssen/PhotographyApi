using Business.Entities.Recipes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Configurations;

public class RecipeConfiguration : EntityBaseConfiguration<Recipe>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Recipe> builder)
    {
        builder.Property(r => r.Name).HasMaxLength(256);
    }
}
