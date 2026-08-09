using Business.Entities.Models;

namespace Business.Entities.Recipes;

public class Recipe(string name, int? numberOfPortions, List<Ingredient> ingredients, string? preparation) : EntityBase
{
    public string Name { get; private set; } = name;
    public int? NumberOfPortions { get; private set; } = numberOfPortions;
    public List<Ingredient> Ingredients { get; private set; } = ingredients;
    public string? Preparation { get; private set; } = preparation;

    // Parameterless constructor for EF Core
    public Recipe() : this(string.Empty, null, [], null) { }

    public void UpdateRecipe(string name, List<Ingredient> ingredients, string? preparation)
    {
        Name = name;
        Ingredients = ingredients;
        Preparation = preparation;
    }
}
