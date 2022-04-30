var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger(c => c.RouteTemplate = "PhotographyApi/swagger/{documentname}/swagger.json");
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Blabla Swagger");
    c.RoutePrefix = "PhotographyApi/swagger";
});
//}
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
