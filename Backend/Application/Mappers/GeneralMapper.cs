using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class GeneralMapper
{
    [NamedMapping("GenerateId")]
    public static Ulid GenerateId()
    {
        return Ulid.NewUlid();
    }

    [NamedMapping("SetCreatedAt")]
    public static DateTimeOffset SetCreatedAt()
    {
        return DateTime.UtcNow;
    }
}
