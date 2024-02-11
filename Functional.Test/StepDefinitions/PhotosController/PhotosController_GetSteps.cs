using Data.Repository;
using FluentAssertions;
using FluentAssertions.Execution;
using PhotographyApi.ViewModels.Photos;
using TechTalk.SpecFlow;
using Test.Helpers;

namespace Functional.Test.StepDefinitions;

[Binding]
public sealed class PhotosController_GetSteps
{
    private readonly PhotographyWebApplicationFactory _webApplicationFactory;
    private readonly FakePhotographyManager _fakePhotgraphyManager;

    private HttpResponseMessage _response = null!;

    private readonly int _homepageAlbumId = 1;
    private readonly string _someAlbumFileName = "album_1.json";
    private readonly DateTime _someDateTime = TestConstants.SomeDateTime;
    private readonly DateTime _someLaterDateTime = TestConstants.SomeDateTime.AddSeconds(1);

    public PhotosController_GetSteps(
        PhotographyWebApplicationFactory webApplicationFactory,
        FakePhotographyManager fakePhotgraphyManager)
    {
        _webApplicationFactory = webApplicationFactory;
        _fakePhotgraphyManager = fakePhotgraphyManager;
    }

    [Given("there are photos in the homepage album")]
    public async Task GivenANumberOfPhotosInTheDatabase()
    {
        await _fakePhotgraphyManager.WriteAlbums([new() { Id = _homepageAlbumId, Title = "Homepage", FileName = _someAlbumFileName }]);
        await _fakePhotgraphyManager.WriteAlbumDetails(
            _someAlbumFileName,
            new() { Photos = [new() { Id = 1, Date = _someDateTime }, new() { Id = 2, Date = _someLaterDateTime }] });
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
