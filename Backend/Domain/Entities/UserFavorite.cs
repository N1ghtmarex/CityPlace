namespace Domain.Entities
{
    /// <summary>
    /// Список избранного пользователя
    /// </summary>
    public class UserFavorite : BaseEntity<Guid>
    {
        /// <summary>
        /// Идентификатор локации
        /// </summary>
        public required Guid LocationId { get; set; }

        /// <summary>
        /// Локация
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Guid UserId {  get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User? User {  get; set; }
    }
}
