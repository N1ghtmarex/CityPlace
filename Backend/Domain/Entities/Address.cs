using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address : BaseEntity<Ulid>
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public required string Region { get; init; }

        /// <summary>
        /// Идентификатор субъекта РФ в системе FIAS
        /// </summary>
        public required string RegionFiasId { get; init; }

        /// <summary>
        /// Муниципальный район/округ
        /// </summary>
        public required string District { get; init; }

        /// <summary>
        /// Идентификатор муниципального района/округа в системе FIAS
        /// </summary>
        public required string DistrictFiasId { get; init; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public required string Settlement { get; init; }

        /// <summary>
        /// Идентификатор населенного пункта в системе FIAS
        /// </summary>
        public required string SettlementFiasId { get; init; }

        /// <summary>
        /// Элемент улично-дорожной сети
        /// </summary>
        public required string PlanningStructure { get; init; }

        /// <summary>
        /// Идентификатор элемента улично-дорожной сети в системе FIAS
        /// </summary>
        public required string PlanningStructureFiasId { get; init; }

        /// <summary>
        /// Здание (строение), сооружение
        /// </summary>
        public required string House { get; init; }

        /// <summary>
        /// Идентификатор здания (строения), сооружения в системе FIAS
        /// </summary>
        public required string HouseFiasId { get; init; }

        /// <summary>
        /// Помещение
        /// </summary>
        public required string Appartment { get; init; }

        /// <summary>
        /// Идентификатор помещения в системе FIAS
        /// </summary>
        public required string AppartmentFiasId { get; init; }
    }
}
