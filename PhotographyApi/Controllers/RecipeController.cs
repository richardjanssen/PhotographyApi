using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PhotographyApi.Mappers;
using PhotographyApi.Mappers.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class RecipeController(IRecipeRepository recipeRepository, IDbContextFactory<RiesjDbContext> dbContextFactory) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<RecipeViewModel>> GetAll() => (await recipeRepository.GetRecipes()).Select(recipe => recipe.Map()).ToList();

    [HttpPost]
    [Authorize(Roles = "PhotographyApi_Admin")]
    public async Task<RecipeViewModel> Add(RecipeViewModel recipe) => (await recipeRepository.AddRecipe(recipe.Map())).Map();

    [HttpPost]
    [Authorize(Roles = "PhotographyApi_Admin")]
    public async Task UpdateConcurrent()
    {
        // TODO: This should be tested in a unit test
        using var db = dbContextFactory.CreateDbContext();
        var recipe = await db.Recipes.FirstOrDefaultAsync(r => r.Id == 1);

        // Simulate a concurrent update
        using (var concurrentDb = dbContextFactory.CreateDbContext())
        {
            var concurrentRecipe = await concurrentDb.Recipes.FirstOrDefaultAsync(r => r.Id == 1);
            if (concurrentRecipe != null)
            {
                concurrentRecipe.Name = "An updated recipe name";
            }
            await concurrentDb.SaveChangesAsync();
        }

        // Throws DbUpdateConcurrencyException
        if (recipe != null)
        {
            recipe.Name = "Hoi";
        }
        await db.SaveChangesAsync();
    }
}