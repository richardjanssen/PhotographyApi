using Business.Entities.Models;

namespace Business.Entities.Recipes;
public class Recipe(string name, string ingredients, string preparation) : EntityBase
{
    public string Name { get; set; } = name;
    public string Ingredients { get; set; } = ingredients;
    public string Preparation { get; set; } = preparation;
}
