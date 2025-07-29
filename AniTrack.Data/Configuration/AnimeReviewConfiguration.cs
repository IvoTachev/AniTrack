namespace AniTrack.Data.Configuration
{
    using AniTrack.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static AniTrack.Common.EntityConstants.AnimeReview;
    public class AnimeReviewConfiguration : IEntityTypeConfiguration<AnimeReview>
    {
     
        public void Configure(EntityTypeBuilder<AnimeReview> entity)
        {
            // Composite Primary Key
            entity
                .HasKey(ar => new { ar.AuthorId, ar.AnimeId });
            // Relation between AnimeReview and ApplicationUser 
            entity
                .HasOne(ar => ar.Author)
                .WithMany(u => u.Reviews)
                .HasForeignKey(ar => ar.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Relation between AnimeReview and Anime
            entity
                .HasOne(ar => ar.Anime)
                .WithMany(a => a.Reviews)
                .HasForeignKey(ar => ar.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);
            // Setting up the Content property
            entity.
                Property(ar => ar.Content)
                .IsRequired()
                .HasMaxLength(ReviewContentMaxLength); 
            // Setting up the CreatedOn property
            entity
                .Property(ar => ar.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql(CreatedOnDefaultValue); // Default value for CreatedOn

            // Setting up the IsDeleted property for soft deletion
            entity
               .Property(ar => ar.IsDeleted)
               .IsRequired()
               .HasDefaultValue(false);
            // Setting up the isAnimeRecommended property
            entity
                .Property(ar => ar.isAnimeRecommended)
                .IsRequired();
            // Query filter for soft deletion
            entity
                .HasQueryFilter(ar => ar.Anime.IsDeleted == false && ar.IsDeleted == false);

            entity.HasData(this.SeedAnimeReview());
        }
        public List<AnimeReview> SeedAnimeReview()
        {
            var animeReviews = new List<AnimeReview>
            {
                // Positive Review for Sousou no Frieren by TestUser
                new AnimeReview
                {
                    AuthorId = "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59",
                    AnimeId = 1,
                    Content = "A beautiful exploration of life and death, with stunning visuals and deep themes.",
                    IsDeleted = false,
                    isAnimeRecommended = true,
                    CreatedOn = DateTime.UtcNow
                },
                // Positive Review for Fullmetal Alchemist: Brotherhood by TestAdmin. Posted on Midnight
                new AnimeReview
                {
                    AuthorId = "ebc3f310-2e0d-4dd4-8493-b144cd98d282",
                    AnimeId = 2,
                    Content = "An epic tale of sacrifice, friendship, and the pursuit of knowledge.",
                    IsDeleted = false,
                    isAnimeRecommended = true,
                    CreatedOn = DateTime.UtcNow.Date
                },
                // 2nd Positive Review for Fullmetal Alchemist: Brotherhood by TestUser
                new AnimeReview
                {
                    AuthorId = "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59",
                    AnimeId = 2,
                    Content = "A masterpiece of storytelling and character development, with a perfect blend of action and emotion.",
                    IsDeleted = false,
                    isAnimeRecommended = true,
                    CreatedOn = DateTime.UtcNow
                },
                // Positive Review for Steins;Gate by TestAdmin. Posted on Midnight
                new AnimeReview
                {
                    AuthorId = "ebc3f310-2e0d-4dd4-8493-b144cd98d282",
                    AnimeId = 3,
                    Content = "A mind-bending journey through time with unforgettable characters.",
                    IsDeleted = false,
                    isAnimeRecommended = true,
                    CreatedOn = DateTime.UtcNow.Date
                },
                // Negative Review for Hunter x Hunter (2011) by TestUser
                new AnimeReview
                {
                    AuthorId = "72f4dc29-8a94-4f93-b5bb-b04e0b78eb59",
                    AnimeId = 4,
                    Content = "I found the pacing too slow and the story dragged on unnecessarily.",
                    IsDeleted = false,
                    isAnimeRecommended = false,
                    CreatedOn = DateTime.UtcNow
                },
                // Positive Review for Hunter x Hunter (2011) by TestAdmin. Posted on Midnight
                new AnimeReview
                {
                    AuthorId = "ebc3f310-2e0d-4dd4-8493-b144cd98d282",
                    AnimeId = 4,
                    Content = "An incredible adventure with complex characters and a rich world.",
                    IsDeleted = false,
                    isAnimeRecommended = true,
                    CreatedOn = DateTime.UtcNow.Date
                },


            };
            return animeReviews;
        }
    }
}
