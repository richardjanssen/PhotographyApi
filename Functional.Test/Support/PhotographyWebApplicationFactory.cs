using Common.Common.Interfaces;
using Data.Repository;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Test.Helpers;

public class PhotographyWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly FakeDateTimeProvider _fakeDateTimeProvider;
    private readonly FakeWebHostEnvironment _fakeWebHostEnvironment;
    private readonly FakePhotographyManager _fakePhotographyManager;
    private readonly HttpClient _httpClient = null!;

    public PhotographyWebApplicationFactory(
        FakeDateTimeProvider fakeDateTimeProvider,
        FakeWebHostEnvironment fakeWebHostEnvironment,
        FakePhotographyManager fakePhotographyManager)
    {
        _fakeDateTimeProvider = fakeDateTimeProvider;
        _fakeWebHostEnvironment = fakeWebHostEnvironment;
        _fakePhotographyManager = fakePhotographyManager;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("AppSettings:JwtIssuer", "AFakeJwtIssuerForTestingPurposes"),
                new KeyValuePair<string, string>("AppSettings:JwtSecret", "AFakeJwtSecretForTestingPurposes")
            });
        });

        builder.ConfigureServices(services =>
        {
            services.AddTransient<IPhotographyManager>(_ => _fakePhotographyManager);
            services.AddTransient<IDateTimeProvider>(_ => _fakeDateTimeProvider);
            services.AddTransient<IWebHostEnvironment>(_ => _fakeWebHostEnvironment);
        });

        return base.CreateHost(builder);
    }

    public HttpClient GetClient() => _httpClient ?? CreateClient();
}
