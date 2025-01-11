namespace PhotographyApi.ViewModels.Recipes;

public class RecipeViewModel(string name, string ingredients, string preparation)
{
    public string Name = name;
    public string Ingredients = ingredients;
    public string Preparation = preparation;
}
