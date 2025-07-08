using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfigurations
{
    internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("picture");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));

            builder.Property(x => x.Path).IsRequired();
            builder.HasIndex(x => x.Path).IsUnique();

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => Ulid.Parse(x));
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.IsArchive).IsRequired();
        }
    }
}
