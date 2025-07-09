using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations
{
    internal class UserFavoritesConfiguration : IEntityTypeConfiguration<UserFavorite>
    {
        public void Configure(EntityTypeBuilder<UserFavorite> builder)
        {
            builder.ToTable("user_favorite");
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
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserFavorites)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.LocationId, x.UserId }).IsUnique();

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
        }
    }
}
