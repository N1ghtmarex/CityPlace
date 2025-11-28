using Application.Pictures.Dtos;
using Domain.Enums;

namespace Application.Locations.Dtos
{
    /// <summary>
    /// Модель локации
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public required Ulid Id { get; init; }

        /// <summary>
        /// Наименование
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public required string? Description { get; init; }

        /// <summary>
        /// Тип
        /// </summary>
        public required LocationType Type { get; init; }

        /// <summary>
        /// Широта (геолокация)
        /// </summary>
        public required double Latitude { get; set; }

        /// <summary>
        /// Долгота (геолокация)
        /// </summary>
        public required double Longitude { get; set; }

        /// <summary>
        /// Список изображений
        /// </summary>
        public List<PictureViewModel>? Pictures { get; init; }
    }
}
