using Common.Common;
using Infrastructure.Ioc;
using NLog;
using NLog.Web;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using PhotographyApi.Startup;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

//ILogger? logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("Program");
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
    builder.Host.UseNLog();

    builder.Services
        //.AddRiesjTelemetry(environment.ApplicationName)
        .ConfigureRiesjForwardedHttpHeader()
        .AddRiesjCors(configuration, corsPolicyName)
        .AddRiesjAuthentication(configuration).AddRiesjControllers()
        .AddSwaggerGen(c => c.OperationFilter<RiesjApiKeySwaggerAttribute>())
        .Configure<AppSettings>(configuration.GetSection("AppSettings"))
        .AddMemoryCache()
        .AddRiesjApiBindings();

    var app = builder.Build();
    //logger = app.Services.GetRequiredService<ILogger<Program>>();

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
    //logger?.LogError(exception, "Stopped program because of exception");
    throw;
}

// Required for integration tests
public partial class Program { }