using Common.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test.Helpers;

public class PhotographyWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly FakePhotographyDbContext _fakePhotographyDbContext;
    private readonly FakeDateTimeProvider _fakeDateTimeProvider;

    private readonly HttpClient _httpClient = null!;

    public PhotographyWebApplicationFactory(
        FakePhotographyDbContext fakePhotographyDbContext,
        FakeDateTimeProvider fakeDateTimeProvider)
    {
        _fakePhotographyDbContext = fakePhotographyDbContext;
        _fakeDateTimeProvider = fakeDateTimeProvider;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(_ => _fakePhotographyDbContext.GetContext());
            services.AddTransient<IDateTimeProvider>(_ => _fakeDateTimeProvider);
            
        });

        return base.CreateHost(builder);
    }

    public HttpClient GetClient() => _httpClient ?? CreateClient();
}
