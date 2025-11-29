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

    [NamedMapping("GenerateStringId")]
    public static string GenerateStringId()
    {
        return Ulid.NewUlid().ToString();
    }

    [NamedMapping("SetCreatedAt")]
    public static DateTimeOffset SetCreatedAt()
    {
        return DateTime.UtcNow;
    }
}
