namespace Keycloak.Models
{
    /// <summary>
    /// Модель конфигурации Keycloak
    /// </summary>
    public class KeycloakConfigurationModel
    {
        /// <summary>
        /// Адрес сервиса идентификации
        /// </summary>
        public required string BaseUrl { get; set; }

        /// <summary>
        /// ClientId (Backend)
        /// </summary>
        public required string BackendClientId { get; set; }

        /// <summary>
        /// ClientSecret (credentials) (Backend)
        /// </summary>
        public required string BackendClientSecret { get; set; }

        /// <summary>
        /// ClientId (name)
        /// </summary>
        public required string ClientId { get; set; }

        /// <summary>
        /// ClientSecret (credentials)
        /// </summary>
        public required string ClientSecret { get; set; }

        /// <summary>
        /// Конфигурация внешнего клиента
        /// </summary>
        public required ExternalClientConfigurationModel ExternalClientConfiguration { get; set; }

        /// <summary>
        /// Audiences
        /// </summary>
        public required string Audiences { get; set; }

        /// <summary>
        /// Realm
        /// </summary>
        public required string Realm { get; set; }

        /// <summary>
        /// Время истечения срока действия токена (в секундах)
        /// </summary>
        public int ExpirationTimeBySeconds { get; set; }
    }
}
