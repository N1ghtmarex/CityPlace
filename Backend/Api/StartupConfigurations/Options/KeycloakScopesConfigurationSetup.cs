using Api.StartupConfigurations.Models;
using Microsoft.Extensions.Options;

namespace Api.StartupConfigurations.Options
{
    public class KeycloakScopesConfigurationSetup(IConfiguration configuration) : IConfigureOptions<KeycloakScopeConfigurationModel>
    {
        public void Configure(KeycloakScopeConfigurationModel options)
        {
            var scopesConfiguration = configuration.GetSection("KeycloakScopesConfiguration");
            scopesConfiguration.Bind(options);
        }
    }
}
