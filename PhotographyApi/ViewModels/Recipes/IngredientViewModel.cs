namespace PhotographyApi.ViewModels.Recipes;

public record IngredientViewModel(long? Id, long? RowVersion, string Name, string Quantity, string? Unit, string? Subgroup);
