using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public required UserRole Role { get; set; }

        /// <summary>
        /// Список избранного пользователя
        /// </summary>
        public ICollection<UserFavorite>? UserFavorites { get; set; }
    }
}
