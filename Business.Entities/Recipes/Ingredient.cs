using Business.Entities.Models;

namespace Business.Entities.Recipes;
public class Ingredient(string name, string quantity, string? unit, string? subgroup) : EntityBase
{
    public string Name { get; } = name;
    public string Quantity { get; } = quantity;
    public string? Unit { get; } = unit;
    public string? Subgroup { get; } = subgroup;

    // Parameterless constructor for EF Core
    public Ingredient() : this(string.Empty, string.Empty, null, null) { }
}
