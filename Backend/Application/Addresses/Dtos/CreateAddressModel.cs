namespace Application.Addresses.Dtos
{
    /// <summary>
    /// Модель добавления адреса
    /// </summary>
    public class CreateAddressModel
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public required string Region { get; init; }

        /// <summary>
        /// Муниципальный район/округ
        /// </summary>
        public required string District { get; init; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public required string Settlement { get; init; }

        /// <summary>
        /// Элемент улично-дорожной сети
        /// </summary>
        public required string PlanningStructure { get; init; }

        /// <summary>
        /// Здание (строение), сооружение
        /// </summary>
        public required string House { get; init; }

        /// <summary>
        /// Помещение
        /// </summary>
        public required string Appartment { get; init; }
    }
}
