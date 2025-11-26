using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using System.Text;
using System.Text.Json.Serialization;

namespace PhotographyApi.Startup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRiesjTelemetry(this IServiceCollection services, string applicationName)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService(applicationName))
            .WithLogging(logging => logging
                .AddOtlpExporter());

        return services;
    }

    public static IServiceCollection ConfigureRiesjForwardedHttpHeader(this IServiceCollection services) =>
        services.Configure<ForwardedHeadersOptions>(options => { options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto; });

    public static IServiceCollection AddRiesjCors(this IServiceCollection services, ConfigurationManager configuration, string policyName) =>
        services.AddCors(opt =>
        {
            opt.AddPolicy(policyName, builder =>
            {
                builder
                    .WithOrigins(configuration["AppSettings:CorsOrigins"]!.Split(','))
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            });
        });

    public static IServiceCollection AddRiesjAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["AppSettings:JwtIssuer"],
                    ValidAudience = configuration["AppSettings:JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:JwtSecret"]!))
                };
            });

        return services;
    }

    public static IServiceCollection AddRiesjControllers(this IServiceCollection services)
    {
        services.AddControllers(options => options.OutputFormatters.RemoveType<StringOutputFormatter>()).AddJsonOptions(opts =>
        {
            var enumConverter = new JsonStringEnumConverter();
            opts.JsonSerializerOptions.Converters.Add(enumConverter);
        });

        return services;
    }
}
