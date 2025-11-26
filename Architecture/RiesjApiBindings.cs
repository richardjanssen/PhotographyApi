using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;
public static class RiesjApiBindings
{
    public static IServiceCollection AddRiesjApiBindings(this IServiceCollection services) => services
        .AddDataBindings()
        .AddBusinessBindings()
        .AddCommonBindings();
}