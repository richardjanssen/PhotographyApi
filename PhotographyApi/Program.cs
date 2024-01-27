using Common.Common;
using Infrastructure.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using PhotographyApi.Swagger;
using System.Text;
using System.Text.Json.Serialization;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = Directory.GetCurrentDirectory()
    });
    var configuration = builder.Configuration;

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("PhotographyClient", builder =>
        {
            builder
                .WithOrigins(configuration["AppSettings:CorsOrigins"]!.Split(','))
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader();
        });
    });
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
    builder.Services.AddControllers(options => options.OutputFormatters.RemoveType<StringOutputFormatter>()).AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => c.OperationFilter<RiesjApiKeySwaggerAttribute>());
    builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    builder.Services.AddDataBindings();
    builder.Services.AddBusinessBindings();
    builder.Services.AddCommonBindings();

    var app = builder.Build();

    app.UseForwardedHeaders();

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

    app.UsePathBase("/api");
    app.UseStaticFiles();
    app.UseCors("PhotographyClient");
    app.UseAuthentication();
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


// Required for integration tests
public partial class Program { }