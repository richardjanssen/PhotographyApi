using Infrastructure.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();

#pragma warning disable CA1050 // Declare types in namespaces
// Required for integration tests
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces