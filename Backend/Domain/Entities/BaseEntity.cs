namespace Domain.Entities
{
    /// <summary>
    /// Базовая сущность, от которой налследуются последующие сущности.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T> where T: notnull
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public required T Id { get; set; }
    }
}
