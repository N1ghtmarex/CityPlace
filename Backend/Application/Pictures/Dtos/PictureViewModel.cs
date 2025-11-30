namespace Application.Pictures.Dtos
{
    public class PictureViewModel
    {
        public required Ulid Id { get; init; }
        public required string Path { get; init; }
        public required Ulid UserId { get; init; }
        public required Boolean IsAvatar { get; init; }
        public required DateTimeOffset CreatedAt { get; init; }
    }
}
