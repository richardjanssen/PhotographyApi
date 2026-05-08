using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PhotographyApi.Startup;

public class RiesjApiKeySwaggerAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "RiesjApiKey",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String
            }
        });
    }
}