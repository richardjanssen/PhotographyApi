namespace PhotographyApi.ViewModels.Recipes;

public record RecipeViewModel(
    long? Id,
    long? RowVersion,
    string Name,
    int? NumberOfPortions,
    IngredientViewModel[] SingleIngredients,
    IngredientGroupViewModel[] IngredientGroups,
    string? Preparation);
