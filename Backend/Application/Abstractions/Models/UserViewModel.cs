using Domain.Enums;

namespace Application.Abstractions.Models
{
    /// <summary>
    /// Модель профиля пользователя
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public required Ulid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public required UserRole Role { get; set; }

        /// <summary>
        /// Дата создания аккаунта
        /// </summary>
        public required DateTimeOffset CreatedAt { get; set; }
    }
}
