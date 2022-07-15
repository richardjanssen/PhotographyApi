using Common.Common;
using Common.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class CommonBindings
{
    public static IServiceCollection AddCommonBindings(this IServiceCollection services) => services
        .AddScoped<IDateTimeProvider, DateTimeProvider>();
}
