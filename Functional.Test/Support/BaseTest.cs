using Functional.Test.Support.Mocks;

namespace Functional.Test.Support;

public class BaseTest
{
    private readonly PhotographyWebApplicationFactory _webApplicationFactory;
    public MockedDependencies MockedDependencies { get; }

    public BaseTest()
    {
        MockedDependencies = new MockedDependencies();
        _webApplicationFactory = new PhotographyWebApplicationFactory(MockedDependencies);
    }

    public HttpClient GetClient() => _webApplicationFactory.CreateClient();
}