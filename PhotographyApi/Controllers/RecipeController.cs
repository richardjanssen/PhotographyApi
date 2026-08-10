using Common.Common;
using Data.Interfaces;
using Data.Repository.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotographyApi.Mappers;
using PhotographyApi.Mappers.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class RecipeController(IRecipeRepository recipeRepository, IDbContextFactory<RiesjDbContext> dbContextFactory) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<RecipeOverviewViewModel>> GetAll() => [.. (await recipeRepository.GetRecipes()).Select(recipe => recipe.MapToOverview())];

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_RecipeEdit)]
    public async Task<RecipeViewModel> Add(RecipeViewModel recipeViewModel)
    {
        var recipe = recipeViewModel.Map();
        return (await recipeRepository.SaveRecipe(recipe)).Map();
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Riesj_RecipeEdit)]
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
                concurrentRecipe.UpdateRecipeTest("An updated recipe name", concurrentRecipe.Ingredients, concurrentRecipe.Preparation);
            }
            await concurrentDb.SaveChangesAsync();
        }

        // Throws DbUpdateConcurrencyException
        if (recipe != null)
        {
            recipe.UpdateRecipeTest("Hoi", recipe.Ingredients, recipe.Preparation); ;
        }
        await db.SaveChangesAsync();
    }

    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Riesj_RecipeEdit)]
    public async Task<RecipeViewModel?> GetById(int id)
    {
        var recipe = await recipeRepository.GetById(id);
        return recipe?.Map();
    }
}