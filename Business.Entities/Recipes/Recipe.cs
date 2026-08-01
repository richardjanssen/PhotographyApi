using Business.Entities.Models;

namespace Business.Entities.Recipes;
public class Recipe(string name, List<Ingredient> ingredients, string preparation) : EntityBase
{
    public string Name { get; private set; } = name;
    public List<Ingredient> Ingredients { get; private set; } = ingredients;
    public string Preparation { get; private set; } = preparation;

    // Parameterless constructor for EF Core
    public Recipe() : this(string.Empty, [], string.Empty) { }

    public void UpdateRecipe(string name, List<Ingredient> ingredients, string preparation)
    {
        Name = name;
        Ingredients = ingredients;
        Preparation = preparation;
    }
}
