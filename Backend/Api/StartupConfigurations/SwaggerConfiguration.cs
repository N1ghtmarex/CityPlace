using Api.StartupConfigurations.Models;
using Keycloak.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Api.StartupConfigurations
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IOptions<KeycloakConfigurationModel>>().Value;
            var scopesConfiguration = serviceProvider.GetRequiredService<IOptions<KeycloakScopeConfigurationModel>>().Value;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("admin", new OpenApiInfo { Title = "CityPlace.Admin.API" });

                Directory
                    .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                    .ToList()
                    .ForEach(xmlFile =>
                    {
                        var doc = XDocument.Load(xmlFile);
                        options.IncludeXmlComments(() => new XPathDocument(doc.CreateReader()), includeControllerXmlComments: true);
                    });

                options.AddSecurityDefinition(KeycloakAuthConfiguration.AdminApiScheme,
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(configuration.BaseUrl + $"/realms/{configuration.Realm}/protocol/openid-connect/auth"),
                                TokenUrl = new Uri(configuration.BaseUrl + $"/realms/{configuration.Realm}/protocol/openid-connect/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { scopesConfiguration.AdminScopeName, "CityPlace.Admin.API" },
                                    { "openid", "Identifier" },
                                    { "roles", "Roles User" },
                                    { "profile", "Profile identity user" },
                                    { "email", "User email" }
                                }
                            }
                        },
                        Scheme = "Bearer"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = KeycloakAuthConfiguration.AdminApiScheme
                            }
                        },
                        new[] { KeycloakAuthConfiguration.AdminApiScheme }
                    }
                });
            });
        }

        public static void ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/admin/swagger.json", "CityPlace.Admin.API");
                options.OAuthAppName("Swagger Client");
                options.OAuthUsePkce();
            });
        }
    }
}
