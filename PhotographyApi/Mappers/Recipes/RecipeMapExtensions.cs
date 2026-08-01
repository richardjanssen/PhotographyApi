using Business.Entities.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Mappers.Recipes;

public static class RecipeMapExtensions
{
    public static RecipeViewModel Map(this Recipe recipe) => new(recipe.Name, [.. recipe.Ingredients.Select(i => i.Map())], recipe.Preparation);

    public static Recipe Map(this RecipeViewModel recipe) => new(recipe.Name, [.. recipe.Ingredients.Select(i => i.Map())], recipe.Preparation);

    private static IngredientViewModel Map(this Ingredient ingredient) => new(ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Subgroup);
    private static Ingredient Map(this IngredientViewModel ingredient) => new(ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Subgroup);
}
