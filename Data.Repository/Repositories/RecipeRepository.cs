using Business.Entities.Recipes;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Repositories;
public class RecipeRepository(IDbContextFactory<RiesjDbContext> dbContextFactory) : IRecipeRepository
{
    private readonly IDbContextFactory<RiesjDbContext> _dbContextFactory = dbContextFactory;

    public async Task<IReadOnlyCollection<Recipe>> GetRecipes() {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return dbContext.Recipes.ToList();
    }
}
