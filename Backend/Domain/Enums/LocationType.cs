using System.ComponentModel;

namespace Domain.Enums
{
    /// <summary>
    /// Перечисление типов общественных мест
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// Не указан
        /// </summary>
        [Description("Не указан")]
        None = 0,

        /// <summary>
        /// Заведения общественного питания - кафе, рестораны, столовые
        /// </summary>
        [Description("Питание")]
        Food = 1,

        /// <summary>
        /// Услуги и сервисные центры
        /// </summary>
        [Description("Услуга")]
        Service = 2,

        /// <summary>
        /// Развлечение
        /// </summary>
        [Description("Развлечение")]
        Entertainment = 3,

        /// <summary>
        /// Памятники и скульптуры
        /// </summary>
        [Description("Памятники и скульптуры")]
        Monuments = 4,

        /// <summary>
        /// Спорт
        /// </summary>
        [Description("Спорт")]
        Sport = 5
    }
}
