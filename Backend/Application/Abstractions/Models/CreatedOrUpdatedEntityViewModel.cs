namespace Application.Abstractions.Models
{
    public class CreatedOrUpdatedEntityViewModel<T>(T id)
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; } = id;
    }

    public class CreatedOrUpdatedEntityViewModel(Ulid id) : CreatedOrUpdatedEntityViewModel<Ulid>(id);
}
