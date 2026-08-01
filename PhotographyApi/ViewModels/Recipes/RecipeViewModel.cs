namespace PhotographyApi.ViewModels.Recipes;

public record RecipeViewModel(string Name, IngredientViewModel[] Ingredients, string Preparation);
