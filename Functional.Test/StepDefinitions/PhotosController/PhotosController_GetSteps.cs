using Data.Repository.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using PhotographyApi.ViewModels;
using TechTalk.SpecFlow;
using Test.Helpers;

namespace Functional.Test.StepDefinitions;

[Binding]
public sealed class PhotosController_GetSteps
{
    private readonly PhotographyWebApplicationFactory _webApplicationFactory;
    private readonly FakePhotographyDbContext _fakePhotographyDbContext;
    private HttpResponseMessage _response = null!;

    private readonly DateTime _someDateTime = TestConstants.SomeDateTime;
    private readonly DateTime _someLaterDateTime = TestConstants.SomeDateTime.AddSeconds(1);

    public PhotosController_GetSteps(
        PhotographyWebApplicationFactory webApplicationFactory,
        FakePhotographyDbContext fakePhotographyDbContext)
    {
        _webApplicationFactory = webApplicationFactory;
        _fakePhotographyDbContext = fakePhotographyDbContext;
    }

    [Given("a number of photos in the database")]
    public void GivenANumberOfPhotosInTheDatabase()
    {
        _fakePhotographyDbContext.Seed(new List<Photo> {
            new Photo { Id = 1, Date = _someDateTime },
            new Photo { Id = 2, Date = _someLaterDateTime }
        });
    }

    [When("a request is received to retrieve these photos")]
    public async Task WhenARequestIsReceivedToRetrieveThesePhotos()
    {
        var client = _webApplicationFactory.GetClient();
        var requestUri = $"/api/v1/Photos/Get";
        _response = await client.GetAsync(requestUri);
    }

    [Then("the photos are returned")]
    public async Task ThenThePhotosAreReturned()
    {
        using var _ = new AssertionScope();

        await _response.Should().BeAValidResponse();
        var result = await _response.ParseTo<IReadOnlyCollection<PhotoViewModel>>();

        var photo = result.Should().SatisfyRespectively(
            first =>
            {
                first.Date.Should().Be(_someLaterDateTime);
                first.Id.Should().Be(2);
            },
            second =>
            {
                second.Date.Should().Be(_someDateTime);
                second.Id.Should().Be(1);
            });
    }
}
