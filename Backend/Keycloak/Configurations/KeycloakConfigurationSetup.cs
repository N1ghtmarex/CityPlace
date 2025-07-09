using Keycloak.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Keycloak.Configurations
{
    public class KeycloakConfigurationSetup(IConfiguration configuration) : IConfigureOptions<KeycloakConfigurationModel>
    {
        public void Configure(KeycloakConfigurationModel options)
        {
            var keycloakConfiguration = configuration.GetSection("KeycloakConfiguration") ??
                throw new ApplicationException("Конфигурация для Keycloak не задана!");

            keycloakConfiguration.Bind(options);
        }
    }
}
