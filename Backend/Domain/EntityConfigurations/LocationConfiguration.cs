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
            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(100);

            builder.Property(x => x.Type).IsRequired();
            builder.HasIndex(x => x.Type).IsUnique(false);

            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
            builder.HasIndex(x => new { x.Latitude, x.Longitude }).IsUnique(false);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.HasIndex(x => x.CreatedAt).IsUnique(false);

            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.HasIndex(x => x.UpdatedAt).IsUnique(false);

            builder.Property(x => x.IsArchive).IsRequired();
            builder.HasIndex(x => x.IsArchive).IsUnique(false);
        }
    }
}
