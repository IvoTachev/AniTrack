
namespace AniTrack.Data.Configuration
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Common.EntityConstants.Anime;
    public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> entity)
        {
            entity
                .HasKey(a => a.Id);

            entity
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            entity
                .Property(a => a.Episodes)
                .IsRequired();

            entity
                .Property(a => a.AirDate)
                .IsRequired();

            entity
                .Property(a => a.EndDate)
                .IsRequired(false);

            entity
                .Property(a => a.Synopsis)
                .IsRequired()
                .HasMaxLength(SynopsisMaxLength);

            entity
                .Property(a => a.ImageUrl)
                .IsRequired()
                .HasMaxLength(ImageUrlMaxLength);

            entity
                .Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
