using Domain.Abstractions;

namespace Domain.Entities
{
    /// <summary>
    /// Список избранного пользователя
    /// </summary>
    public class UserFavorite : BaseEntity<Ulid>, IHasTrackDateAttribute
    {
        /// <summary>
        /// Идентификатор локации
        /// </summary>
        public required Ulid LocationId { get; set; }

        /// <summary>
        /// Локация
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Ulid UserId {  get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User? User {  get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
