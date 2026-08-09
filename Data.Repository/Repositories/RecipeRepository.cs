using Business.Entities.Recipes;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories;

public class RecipeRepository(IDbContextFactory<RiesjDbContext> dbContextFactory) : IRecipeRepository
{
    private readonly IDbContextFactory<RiesjDbContext> _dbContextFactory = dbContextFactory;

    public async Task<IReadOnlyCollection<Recipe>> GetRecipes()
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return [.. dbContext.Recipes];
    }

    public async Task<Recipe> SaveRecipe(Recipe recipe)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();
        if (recipe.Id == 0)
        {
            await dbContext.Recipes.AddAsync(recipe);

        }
        else
        {
            var dbRecipe = await dbContext.Recipes.Include(r => r.Ingredients).SingleAsync(r => r.Id == recipe.Id);
            dbRecipe.UpdateRecipe(recipe.Name, recipe.NumberOfPortions, recipe.Preparation);

            for (var i = 0; i < recipe.Ingredients.Count; i++)
            {
                var ingredient = recipe.Ingredients[i];
                if (ingredient.Id == 0)
                {
                    await dbContext.Ingredients.AddAsync(ingredient);
                }
                else
                {
                    var dbIngredient = dbRecipe.Ingredients.Single(ig => ig.Id == ingredient.Id);
                    dbIngredient.UpdateIngredient(ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Subgroup);
                }
            }
        }

        await dbContext.SaveChangesAsync();

        return recipe;
    }

    public async Task<Recipe?> GetById(int id)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return dbContext.Recipes.Include(r => r.Ingredients).FirstOrDefault(r => r.Id == id);
    }
}
