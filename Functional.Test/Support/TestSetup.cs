using BoDi;
using TechTalk.SpecFlow;
using Test.Helpers;

namespace Functional.Test.Helpers;

[Binding]
public sealed class TestSetup
{
    private readonly IObjectContainer _objectContainer;

    public TestSetup(IObjectContainer objectContainer) => _objectContainer = objectContainer;

    [BeforeScenario(Order = 1)]
    public void BeforeScenario()
    {
        var httpResponseMessage = new HttpResponseMessage();
        var fakeDateTimeProvider = new FakeDateTimeProvider();
        var fakePhotographyDbContext = new FakePhotographyDbContext();

        // Register webApplicationFactory so SUT can be built
        var webApplicationFactory = new PhotographyWebApplicationFactory(fakePhotographyDbContext, fakeDateTimeProvider);
        _objectContainer.RegisterInstanceAs(httpResponseMessage, typeof(HttpResponseMessage));
        _objectContainer.RegisterInstanceAs(webApplicationFactory, typeof(PhotographyWebApplicationFactory));

        // register in _objectContainer so fakes can be found and edited in tests
        _objectContainer.RegisterInstanceAs(fakeDateTimeProvider, typeof(FakeDateTimeProvider));
        _objectContainer.RegisterInstanceAs(fakePhotographyDbContext, typeof(FakePhotographyDbContext));
    }
}