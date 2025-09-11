namespace Application.Users.Dtos
{
    /// <summary>
    /// Модель создания пользователя
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Username { get; set; }
        
        /// <summary>
        /// Электронная почта
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
