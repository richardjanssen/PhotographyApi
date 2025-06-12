using Common.Common.Interfaces;
using Functional.Test.Support.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Functional.Test.Support;

public class PhotographyWebApplicationFactory(MockedDependencies mockedDependencies) : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Should this be used to replace app settings by an appsettings.IntegrationTesting.json file?
        //builder.UseEnvironment("IntegrationTesting");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("AppSettings:JwtIssuer", "AFakeJwtIssuerForTestingPurposes"),
                new KeyValuePair<string, string?>("AppSettings:JwtSecret", "AFakeJwtSecretForTestingPurposes")
            });
        });

        builder.ConfigureServices(services =>
        {
            services.AddTransient<IDateTimeProvider>(_ => new FakeDateTimeProvider());

            foreach ((var interfaceType, var serviceMock) in mockedDependencies.GetMockedDependencies())
            {
                var serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == interfaceType);
                if (serviceDescriptor != null)
                {
                    services.Remove(serviceDescriptor);
                }

                services.AddSingleton(interfaceType, serviceMock);
            }
        });

        return base.CreateHost(builder);
    }
}
