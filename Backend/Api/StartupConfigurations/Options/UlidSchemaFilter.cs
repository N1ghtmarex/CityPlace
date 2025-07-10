using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Api.StartupConfigurations.Options
{
    [ExcludeFromCodeCoverage]
    public class UlidSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Ulid))
            {
                schema.Type = "string";
                schema.Format = "string";
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString("01FZJ5K5Z0K8QH3X9N5G0RT0D5");
                schema.Properties.Clear();
                schema.Reference = null;
            }
        }
    }
}
