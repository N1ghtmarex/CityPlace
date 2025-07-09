using System.ComponentModel;

namespace Domain.Enums
{
    /// <summary>
    /// Перечисление "Роль"
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        [Description("Пользователь")]
        User = 0,

        /// <summary>
        /// Администратор
        /// </summary>
        [Description("Администратор")]
        Admin = 1
    }
}
