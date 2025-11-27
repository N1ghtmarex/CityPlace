using Core.Http.Features.HttpClients;
using System.Text.Json.Serialization;

namespace Keycloak.Models.KeycloakApiModels
{
    public class CreateKeyckloakUserModel : IHttpRequest
    {
        /// <summary>
        /// Логин пользователя (необязательный параметр)
        /// </summary>
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        /// <summary>
        /// Статус пользователя (необязательный параметр)
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Подтверждена ли почта (необязательный параметр)
        /// </summary>
        [JsonPropertyName("emailVerified")]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Имя (необязательный параметр)
        /// </summary>
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия (необязательный параметр)
        /// </summary>
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        /// <summary>
        /// Почта (необязательный параметр)
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Учетные данные пользователя (необязательный параметр)
        /// </summary>
        [JsonPropertyName("credentials")]
        public List<Credentials>? Credentials { get; set; }
    }

    public class Credentials
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("value")]
        public string? Value { get; set; }
        [JsonPropertyName("temporary")]
        public bool Temporary { get; set; }
    }
}
