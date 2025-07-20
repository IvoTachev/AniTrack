

namespace AniTrack.Data.Configuration
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class AnimeGenreConfiguration : IEntityTypeConfiguration<AnimeGenre>
    {
        public void Configure(EntityTypeBuilder<AnimeGenre> entity)
        {
            entity
                .HasKey(ag => new { ag.AnimeId, ag.GenreId });
            entity
                .HasOne(ag => ag.Anime)
                .WithMany(a => a.AnimeGenres)
                .HasForeignKey(ag => ag.AnimeId);
            entity
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AnimesGenre)
                .HasForeignKey(ag => ag.GenreId);
            entity
                .Property(ag => ag.IsDeleted)
                .HasDefaultValue(false);
            entity
               .HasQueryFilter(cm => cm.IsDeleted == false &&
                                               cm.Anime.IsDeleted == false &&
                                               cm.Genre.IsDeleted == false);
        }
    }
}
