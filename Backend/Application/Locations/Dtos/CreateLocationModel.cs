using Application.Addresses.Dtos;
using Domain.Entities;
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
        /// Адрес
        /// </summary>
        public required AddressViewModel Address { get; init; }
    }
}
