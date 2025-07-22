
namespace AniTrack.Data.Configuration
{
    using AniTrack.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Common.EntityConstants.Genre;
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            // Setting up the primary key for the Genre
            entity
                .HasKey(g => g.Id);
            // Setting restraints on the Name property
            entity
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(GenreNameMaxLength);
            // Seeding initial data for genres
            entity.HasData(SeedGenre());
            // Setting up the IsDeleted property for soft deletion
            entity
                .Property(g => g.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
            // Apply global query filter to exclude soft-deleted records
            entity
                .HasQueryFilter(g => g.IsDeleted == false);
        }

        public List<Genre> SeedGenre()
        {
            List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1,
                    Name = "Action",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Adventure",
                    IsDeleted = false
                    },
                new Genre()
                {
                    Id = 3,
                    Name = "Avant Garde",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Award Winning",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Boys Love",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 6,
                    Name = "Comedy",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 7,
                    Name = "Drama",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 8,
                    Name = "Fantasy",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 9,
                    Name = "Girls Love",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 10,
                    Name = "Gourmet",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 11,
                    Name = "Horror",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 12,
                    Name = "Mystery",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 13,
                    Name = "Romance",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 14,
                    Name = "Sci-Fi",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 15,
                    Name = "Slice of Life",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 16,
                    Name = "Sports",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 17,
                    Name = "Supernatural",
                    IsDeleted = false
                },
                new Genre()
                {
                    Id = 18,
                    Name = "Suspense",
                    IsDeleted = false
                },
            };
            return genres;
        }
    }
}
