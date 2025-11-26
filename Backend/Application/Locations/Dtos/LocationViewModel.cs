using Application.Addresses.Dtos;
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
        public required string Type { get; init; }

        /// <summary>
        /// Модель адреса
        /// </summary>
        public required AddressViewModel Address { get; init; }
        public List<PictureViewModel>? Pictures { get; init; }
    }
}
