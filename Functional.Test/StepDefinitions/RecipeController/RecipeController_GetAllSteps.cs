using Business.Entities.Recipes;
using FluentAssertions;
using FluentAssertions.Execution;
using Functional.Test.Support;
using Functional.Test.Support.Extensions;
using PhotographyApi.ViewModels.Recipes;
using TechTalk.SpecFlow;

namespace Functional.Test.StepDefinitions;

[Binding]
public sealed class RecipeController_GetAllSteps() : BaseTest
{
    private HttpResponseMessage _response = null!;
    private readonly Recipe _firstRecipe = new("First recipe", "Some ingredients", "Some preparation");
    private readonly Recipe _secondRecipe = new("Second recipe", "Some other ingredients", "Some other preparation");

    [Given("there are recipes")]
    public async Task GivenANumberOfPhotosInTheDatabase()
    {
        var dbContext = Support.Mocks.FakeDbContextManager.GetDbContext();
        dbContext.Recipes.AddRange(_firstRecipe, _secondRecipe);
        await dbContext.SaveChangesAsync();
    }

    [When("a request is received to retrieve these recipes")]
    public async Task WhenARequestIsReceivedToRetrieveThesePhotos()
    {
        var requestUri = $"/api/v1/Recipe/GetAll";
        _response = await GetClient().GetAsync(requestUri);
    }

    [Then("the recipes are returned")]
    public async Task ThenThePhotosAreReturned()
    {
        using var _ = new AssertionScope();

        await _response.Should().BeAValidResponse();
        var result = await _response.ParseTo<IReadOnlyCollection<RecipeViewModel>>();

        var photo = result.Should().SatisfyRespectively(
            first =>
            {
                first.Name.Should().Be(_firstRecipe.Name);
                first.Ingredients.Should().Be(_firstRecipe.Ingredients);
                first.Preparation.Should().Be(_firstRecipe.Preparation);
            },
            second =>
            {
                second.Name.Should().Be(_secondRecipe.Name);
                second.Ingredients.Should().Be(_secondRecipe.Ingredients);
                second.Preparation.Should().Be(_secondRecipe.Preparation);
            });
    }
}
