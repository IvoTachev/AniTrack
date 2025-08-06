namespace AniTrack.Data.Configuration
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class AnimeGenreConfiguration : IEntityTypeConfiguration<AnimeGenre>
    {
        public void Configure(EntityTypeBuilder<AnimeGenre> entity)
        {
            // Setting up the primary key and relationships
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
            // Apply global query filter to exclude soft-deleted records
            entity
               .HasQueryFilter(cm => cm.IsDeleted == false &&
                                               cm.Anime.IsDeleted == false &&
                                               cm.Genre.IsDeleted == false);
            // Seed initial data
            entity.HasData(this.SeedAnimeGenre());
        }
        public List<AnimeGenre> SeedAnimeGenre()
        {
            var animeGenres = new List<AnimeGenre>
        {
            // Sousou no Frieren: Adventure, Drama, Fantasy
            new AnimeGenre { AnimeId = 1, GenreId = 2, IsDeleted = false },  // Adventure
            new AnimeGenre { AnimeId = 1, GenreId = 7, IsDeleted = false },  // Drama
            new AnimeGenre { AnimeId = 1, GenreId = 8, IsDeleted = false },  // Fantasy

            // Fullmetal Alchemist: Brotherhood: Action, Adventure, Drama, Fantasy
            new AnimeGenre { AnimeId = 2, GenreId = 1, IsDeleted = false },  // Action
            new AnimeGenre { AnimeId = 2, GenreId = 2, IsDeleted = false },  // Adventure
            new AnimeGenre { AnimeId = 2, GenreId = 7, IsDeleted = false },  // Drama
            new AnimeGenre { AnimeId = 2, GenreId = 8, IsDeleted = false },  // Fantasy

            // Steins;Gate: Sci-Fi, Suspense, Drama
            new AnimeGenre { AnimeId = 3, GenreId = 14, IsDeleted = false }, // Sci-Fi
            new AnimeGenre { AnimeId = 3, GenreId = 18, IsDeleted = false }, // Suspense
            new AnimeGenre { AnimeId = 3, GenreId = 7, IsDeleted = false },  // Drama

            // Hunter x Hunter (2011): Action, Adventure, Fantasy
            new AnimeGenre { AnimeId = 4, GenreId = 1, IsDeleted = false },  // Action
            new AnimeGenre { AnimeId = 4, GenreId = 2, IsDeleted = false },  // Adventure
            new AnimeGenre { AnimeId = 4, GenreId = 8, IsDeleted = false },  // Fantasy

            // Attack On Titan: Action, Drama, Suspense, Award Winning
            new AnimeGenre { AnimeId = 5, GenreId = 1, IsDeleted = false },  // Action
            new AnimeGenre { AnimeId = 5, GenreId = 7, IsDeleted = false },  // Drama
            new AnimeGenre { AnimeId = 5, GenreId = 18, IsDeleted = false }, // Suspense
            new AnimeGenre { AnimeId = 5, GenreId = 4, IsDeleted = false }, // Award Winning

            // Death Note: Suspense, Supernatural
            new AnimeGenre { AnimeId = 6, GenreId = 18, IsDeleted = false }, // Suspense
            new AnimeGenre { AnimeId = 6, GenreId = 17, IsDeleted = false }, // Supernatural

            // Demon Slayer: Action, Supernatural, Award Winning
            new AnimeGenre { AnimeId = 7, GenreId = 1, IsDeleted = false },  // Action
            new AnimeGenre { AnimeId = 7, GenreId = 17, IsDeleted = false }, // Supernatural
            new AnimeGenre { AnimeId = 7, GenreId = 4, IsDeleted = false },  // Award Winning

            // Jujutsu Kaisen: Action, Supernatural, Award Winning
            new AnimeGenre { AnimeId = 8, GenreId = 1, IsDeleted = false },  // Action
            new AnimeGenre { AnimeId = 8, GenreId = 17, IsDeleted = false }, // Supernatural
            new AnimeGenre { AnimeId = 8, GenreId = 4, IsDeleted = false },  // Award Winning

            // My Hero Academia: Action
            new AnimeGenre { AnimeId = 9, GenreId = 1, IsDeleted = false },  // Action


            // One Punch Man: Action, Comedy
            new AnimeGenre { AnimeId = 10, GenreId = 1, IsDeleted = false }, // Action
            new AnimeGenre { AnimeId = 10, GenreId = 6, IsDeleted = false }, // Comedy

        };
            return animeGenres;
        }
    }
}

