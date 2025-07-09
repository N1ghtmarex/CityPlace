using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations
{
    internal class LocationPictureBindConfiguration : IEntityTypeConfiguration<LocationPictureBind>
    {
        public void Configure(EntityTypeBuilder<LocationPictureBind> builder)
        {
            builder.ToTable("location_picture_bind");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));

            builder.Property(x => x.LocationId)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));
            builder.HasOne(x => x.Location)
                .WithMany(x => x.LocationPictures)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.PictureId)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));
            builder.HasOne(x => x.Picture)
                .WithMany()
                .HasForeignKey(x => x.PictureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IsAvatar).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.IsArchive).IsRequired();

            builder.HasIndex(x => new { x.PictureId, x.LocationId }).IsUnique();
        }
    }
}
