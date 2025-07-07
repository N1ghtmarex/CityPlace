using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Место
    /// </summary>
    public class Location : BaseEntity<Ulid>, IHasTrackDateAttribute, IHasArchiveTrack
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public required LocationType Type { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public required Address Address { get; set; }

        /// <summary>
        /// Идентификатор картинки (аватар)
        /// </summary>
        public Ulid? PictureId {  get; set; }

        /// <summary>
        /// Картинка (аватар)
        /// </summary>
        public Picture? Picture {  get; set; }

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
