using Business.Entities.Recipes;

namespace Data.Interfaces;

public interface IRecipeRepository
{
    Task<IReadOnlyCollection<Recipe>> GetRecipes();
    Task<Recipe> SaveRecipe(Recipe recipe);
    Task<Recipe?> GetById(int id);
}