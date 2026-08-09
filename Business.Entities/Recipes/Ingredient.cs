using Business.Entities.Models;

namespace Business.Entities.Recipes;

public class Ingredient(string name, string? quantity, string? unit, string? subgroup) : EntityBase
{
    public string Name { get; private set; } = name;
    public string? Quantity { get; private set; } = quantity;
    public string? Unit { get; private set; } = unit;
    public string? Subgroup { get; private set; } = subgroup;

    // Parameterless constructor for EF Core
    public Ingredient() : this(string.Empty, null, null, null) { }

    public void UpdateIngredient(string name, string? quantity, string? unit, string? subgroup)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Subgroup = subgroup;
    }
}
