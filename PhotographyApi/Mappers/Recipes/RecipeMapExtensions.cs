using Business.Entities.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Mappers.Recipes;

public static class RecipeMapExtensions
{
    public static RecipeOverviewViewModel MapToOverview(this Recipe recipe) => new(recipe.Id, recipe.Name);
    public static RecipeViewModel Map(this Recipe recipe)
    {
        var singleIngredients = recipe.Ingredients.Where(i => i.Subgroup == null);
        var ingredientGroups = recipe.Ingredients
            .ExceptBy(singleIngredients.Select(i => i.Id), i => i.Id)
            .GroupBy(i => i.Subgroup)
            .Select(g => new IngredientGroup(g.Key!, [.. g]));

        return new(
            recipe.Id,
            recipe.RowVersion,
            recipe.Name,
            recipe.NumberOfPortions,
            [.. singleIngredients.Select(i => i.Map())],
            [.. ingredientGroups.Select(ig => ig.Map())],
            recipe.Preparation);
    }

    public static Recipe Map(this RecipeViewModel recipeViewModel) => new(
            recipeViewModel.Name,
            recipeViewModel.NumberOfPortions,
            [.. recipeViewModel.SingleIngredients.Select(i => i.Map()), .. recipeViewModel.IngredientGroups.SelectMany(ig => ig.Ingredients.Select(i => i.Map(ig.Name)))],
            recipeViewModel.Preparation)
    {
        Id = recipeViewModel.Id ?? 0,
        RowVersion = recipeViewModel.RowVersion ?? 0
    };

    private static IngredientViewModel Map(this Ingredient ingredient) => new(ingredient.Id, ingredient.RowVersion, ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Subgroup);
    private static Ingredient Map(this IngredientViewModel ingredientViewModel, string? subgroupName = null) => new(ingredientViewModel.Name, ingredientViewModel.Quantity, ingredientViewModel.Unit, subgroupName)
    {
        Id = ingredientViewModel.Id ?? 0,
        RowVersion = ingredientViewModel.RowVersion ?? 0
    };

    private static IngredientGroupViewModel Map(this IngredientGroup ingredientGroup) => new(ingredientGroup.Name, [.. ingredientGroup.Ingredients.Select(i => i.Map())]);
}
