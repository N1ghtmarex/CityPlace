using Api.StartupConfigurations.Models;
using Keycloak.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.StartupConfigurations
{
    public static class KeycloakAuthConfiguration
    {
        public const string AdminApiPolicy = "CityPlace.Admin.Policy";
        public const string AdminApiScheme = "CityPlace.Admin.Scheme";

        public static void AddKeycloakConfiguration(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IOptions<KeycloakConfigurationModel>>().Value;
            var scopesConfiguration = serviceProvider.GetRequiredService<IOptions<KeycloakScopeConfigurationModel>>().Value;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration.BaseUrl + $"/realms/{configuration.Realm}";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidAudiences = configuration.Audiences.Split(' ');
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminApiPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes();

                    var scopes = new[] { scopesConfiguration.AdminScopeName };
                    policy.RequireAssertion(context => CheckScopes(context, scopes));

                    policy.RequireAuthenticatedUser();
                });
            });
        }

        public static bool CheckScopes(AuthorizationHandlerContext context, string[] scopes)
        {
            var claim = context.User.FindFirst("scope");
            if (claim == null)
            {
                return false;
            }
            return claim.Value.Split(' ').Any(scope =>
                scopes.Contains(scope, StringComparer.Ordinal)
            );
        }
    }
}
