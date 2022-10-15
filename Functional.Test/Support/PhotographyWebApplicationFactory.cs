using Common.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test.Helpers;

public class PhotographyWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly FakePhotographyDbContext _fakePhotographyDbContext;
    private readonly FakeDateTimeProvider _fakeDateTimeProvider;
    private readonly FakeWebHostEnvironment _fakeWebHostEnvironment;
    private readonly HttpClient _httpClient = null!;

    public PhotographyWebApplicationFactory(
        FakePhotographyDbContext fakePhotographyDbContext,
        FakeDateTimeProvider fakeDateTimeProvider,
        FakeWebHostEnvironment fakeWebHostEnvironment)
    {
        _fakePhotographyDbContext = fakePhotographyDbContext;
        _fakeDateTimeProvider = fakeDateTimeProvider;
        _fakeWebHostEnvironment = fakeWebHostEnvironment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(_ => _fakePhotographyDbContext.GetContext());
            services.AddTransient<IDateTimeProvider>(_ => _fakeDateTimeProvider);
            services.AddTransient<IWebHostEnvironment>(_ => _fakeWebHostEnvironment);
        });

        return base.CreateHost(builder);
    }

    public HttpClient GetClient() => _httpClient ?? CreateClient();
}
