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
        // PhotographyWebApplicationFactory and MockedDependencies are created in BaseTest.cs,
        // and are therefore not required in the object container.
        objectContainer.RegisterInstanceAs(new FakeDateTimeProvider(), typeof(FakeDateTimeProvider));

        
    }
}