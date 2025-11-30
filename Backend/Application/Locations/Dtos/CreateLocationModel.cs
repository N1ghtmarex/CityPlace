using Domain.Enums;

namespace Application.Locations.Dtos
{
    public class CreateLocationModel
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Тип
        /// </summary>
        public required LocationType LocationType { get; init; }

        /// <summary>
        /// Широта (геолокация)
        /// </summary>
        public required double Latitude { get; set; }

        /// <summary>
        /// Долгота (геолокация)
        /// </summary>
        public required double Longitude { get; set; }
    }
}
