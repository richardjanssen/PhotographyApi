using Common.Common;
using Data.Repository.Database;
using Infrastructure.Ioc;
using NLog;
using NLog.Web;
using PhotographyApi.Startup;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var corsPolicyName = "PhotographyClient";

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = Directory.GetCurrentDirectory()
    });
    var configuration = builder.Configuration;
    var environment = builder.Environment;

    builder.Logging.ClearProviders();
    builder.Logging.AddConfiguration(configuration.GetSection("Logging"));
    builder.Logging.AddEntityFramework<RiesjDbContext>();
    builder.Host.UseNLog();

    builder.Services
        .ConfigureRiesjForwardedHttpHeader()
        .AddRiesjCors(configuration, corsPolicyName)
        .AddRiesjAuthentication(configuration)
        .AddAuthorization()
        .AddRiesjControllers()
        .AddSwaggerGen(c => c.OperationFilter<RiesjApiKeySwaggerAttribute>())
        .Configure<AppSettings>(configuration.GetSection("AppSettings"))
        .AddMemoryCache()
        .AddRiesjApiBindings();

    var app = builder.Build();

    app
        .UseForwardedHeaders()
        .UseRiesjSwagger(environment)
        .UsePathBase("/api")
        .UseStaticFiles()
        .UseCors(corsPolicyName)
        .UseAuthentication()
        .UseAuthorization();

    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}

// Required for integration tests
public partial class Program { }