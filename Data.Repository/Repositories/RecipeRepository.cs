using Business.Entities.Recipes;
using Common.Common.Interfaces;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories;
public class RecipeRepository(IDbContextFactory<RiesjDbContext> dbContextFactory, IDateTimeProvider dateTimeProvider) : IRecipeRepository
{
    private readonly IDbContextFactory<RiesjDbContext> _dbContextFactory = dbContextFactory;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<IReadOnlyCollection<Recipe>> GetRecipes() {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return dbContext.Recipes.ToList();
    }

    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        recipe.DateModifiedUtc = _dateTimeProvider.UtcNow;
        await dbContext.Recipes.AddAsync(recipe);
        await dbContext.SaveChangesAsync();

        return recipe;
    }
}
