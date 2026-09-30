using Business.Entities.Models;

namespace Business.Entities.Recipes;

public class Ingredient(string name, string? quantity, string? unit, string? subgroup, bool addToGroceries) : EntityBase
{
    public string Name { get; private set; } = name;
    public string? Quantity { get; private set; } = quantity;
    public string? Unit { get; private set; } = unit;
    public string? Subgroup { get; private set; } = subgroup;
    public bool AddToGroceries { get; private set; } = addToGroceries;

    // Parameterless constructor for EF Core
    public Ingredient() : this(string.Empty, null, null, null, true) { }

    public void UpdateIngredient(string name, string? quantity, string? unit, string? subgroup, bool addToGroceries)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Subgroup = subgroup;
        AddToGroceries = addToGroceries;
    }
}
