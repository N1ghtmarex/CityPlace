namespace Application.Locations.Dtos
{
    /// <summary>
    /// Модель списка типов локаций
    /// </summary>
    public class LocationTypesViewModel
    {
        /// <summary>
        /// Числовое значение
        /// </summary>
        public required int Value { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public required string Description { get; set; }
    }
}
