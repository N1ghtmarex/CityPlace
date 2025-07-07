using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Место
    /// </summary>
    public class Location : BaseEntity<Guid>
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
    }
}
