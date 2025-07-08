using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("location");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));

            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("jsonb");

            builder.Property(x => x.PictureId)
                .IsRequired(false)
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.IsArchive).IsRequired();
        }
    }
}
