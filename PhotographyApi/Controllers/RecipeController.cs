using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyApi.Mappers;
using PhotographyApi.Mappers.Recipes;
using PhotographyApi.ViewModels.Recipes;

namespace PhotographyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class RecipeController(IRecipeRepository recipeRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<RecipeViewModel>> GetAll() => (await recipeRepository.GetRecipes()).Select(recipe => recipe.Map()).ToList();

    [HttpPost]
    [Authorize(Roles = "PhotographyApi_Admin")]
    public async Task<RecipeViewModel> Add(RecipeViewModel recipe) => (await recipeRepository.AddRecipe(recipe.Map())).Map();
}