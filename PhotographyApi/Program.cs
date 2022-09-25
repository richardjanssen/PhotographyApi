using Infrastructure.Ioc;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = Directory.GetCurrentDirectory()
    }); ;
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    // Add services to the container.
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("PhotographyClient", builder =>
        {
            builder
                .WithOrigins("https://localhost:4200")
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader();
        });
    });
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddPhotographyDatabase(builder.Configuration);
    builder.Services.AddDataBindings();
    builder.Services.AddBusinessBindings();
    builder.Services.AddCommonBindings();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c => c.RouteTemplate = "api/swagger/{documentname}/swagger.json");
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "v1 Swagger");
            c.RoutePrefix = "api/swagger";
        });
    }

    //app.UseHttpsRedirection();

    //app.UsePathBase("/api");
    app.UseStaticFiles();
    app.UseCors("PhotographyClient");
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}


#pragma warning disable CA1050 
// Required for integration tests
public partial class Program { }
#pragma warning restore CA1050