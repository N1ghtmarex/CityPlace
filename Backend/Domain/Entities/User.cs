using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity<Ulid>, IHasTrackDateAttribute, IHasArchiveTrack
    {
        /// <summary>
        /// Идентификатор пользователя внешней системы
        /// </summary>
        public required Guid ExternalUserId { get; set; }
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

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Статус архивности
        /// </summary>
        public bool IsArchive { get; set; }
    }
}
