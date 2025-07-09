using Domain.Abstractions;

namespace Domain.Entities
{
    /// <summary>
    /// Фото
    /// </summary>
    public class Picture : BaseEntity<Ulid>, IHasTrackDateAttribute, IHasArchiveTrack
    {
        /// <summary>
        /// Путь до файла
        /// </summary>
        public required string Path {  get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Ulid UserId {  get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User? User { get; set; }

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
