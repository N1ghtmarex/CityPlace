using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));

            builder.Property(x => x.ExternalUserId)
                .IsRequired()
                .HasDefaultValue(Guid.Empty);
            builder.Property(x => x.Username).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Role).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.IsArchive).IsRequired();
        }
    }
}
