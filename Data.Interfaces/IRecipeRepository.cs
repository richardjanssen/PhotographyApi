using Business.Entities.Recipes;

namespace Data.Interfaces;

public interface IRecipeRepository
{
    Task<IReadOnlyCollection<Recipe>> GetRecipes();
}