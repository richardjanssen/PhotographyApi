using Business.Entities.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Mappers.Recipes;

public static class RecipeMapExtensions
{
    public static RecipeViewModel Map(this Recipe recipe) => new(recipe.Name, recipe.Ingredients, recipe.Preparation);

    public static Recipe Map(this RecipeViewModel recipe) => new(recipe.Name, recipe.Ingredients, recipe.Preparation);
}
