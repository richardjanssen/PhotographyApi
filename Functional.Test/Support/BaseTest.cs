using Functional.Test.Support.Mocks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Functional.Test.Support;

public class BaseTest
{
    private readonly PhotographyWebApplicationFactory _webApplicationFactory;
    public MockedDependencies MockedDependencies { get; }

    public BaseTest()
    {
        MockedDependencies = new MockedDependencies();

        var dbContext = FakeDbContextManager.GetDbContext();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        //dbContext.SaveChangesAsync().Wait();

        _webApplicationFactory = new PhotographyWebApplicationFactory(MockedDependencies);
    }

    public HttpClient GetClient() => _webApplicationFactory.CreateClient();
}