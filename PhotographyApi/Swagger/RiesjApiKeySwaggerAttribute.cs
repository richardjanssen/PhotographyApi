using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhotographyApi.Swagger;

public class RiesjApiKeySwaggerAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "RiesjApiKey",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}