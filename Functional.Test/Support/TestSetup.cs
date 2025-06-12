using BoDi;
using Functional.Test.Support.Mocks;
using TechTalk.SpecFlow;

namespace Functional.Test.Support;

[Binding]
public sealed class TestSetup(IObjectContainer objectContainer)
{
    [BeforeScenario(Order = 1)]
    public void BeforeScenario()
    {
        var httpResponseMessage = new HttpResponseMessage();
        var fakeDateTimeProvider = new FakeDateTimeProvider();

        // PhotographyWebApplicationFactory and MockedDependencies are created in BaseTest,
        // and are therefore not required in the object container.
        objectContainer.RegisterInstanceAs(httpResponseMessage, typeof(HttpResponseMessage));
        objectContainer.RegisterInstanceAs(fakeDateTimeProvider, typeof(FakeDateTimeProvider));

        
    }
}