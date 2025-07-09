using Domain.Abstractions;

namespace Domain.Entities
{
    /// <summary>
    /// Изображения локации
    /// </summary>
    public class LocationPictureBind : BaseEntity<Ulid>, IHasTrackDateAttribute, IHasArchiveTrack
    {
        /// <summary>
        /// Идентификатор локации
        /// </summary>
        public required Ulid LocationId { get; init; }

        /// <summary>
        /// Локация
        /// </summary>
        public Location? Location { get; init; }

        /// <summary>
        /// Идентификатор изображения
        /// </summary>
        public required Ulid PictureId { get; init; }

        /// <summary>
        /// Изображение
        /// </summary>
        public Picture? Picture { get; init; }

        /// <summary>
        /// Является ли обложкой локации
        /// </summary>
        public required bool IsAvatar { get; set; }

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
