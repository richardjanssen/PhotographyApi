namespace PhotographyApi.Startup;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseRiesjSwagger(this IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            application.UseSwagger(c => c.RouteTemplate = "api/swagger/{documentname}/swagger.json");
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1 Swagger");
                c.RoutePrefix = "api/swagger";
            });
        }

        return application;
    }
}
