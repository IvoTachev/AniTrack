namespace AniTrack.Data.Seeding
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Seeding.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ReviewSeeder : IReviewSeeder
    {
        private readonly AniTrackDbContext dbContext;
        private readonly IConfiguration configuration;

        public ReviewSeeder(AniTrackDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task SeedReviewAsync()
        {
            // Seed reviews for the two seeded users before.

            var reviewsToSeed = new List<AnimeReview>
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

            foreach (var review in reviewsToSeed)
            {
                var exists = await dbContext.AnimeReviews
                    .AnyAsync(r => r.AuthorId == review.AuthorId && r.AnimeId == review.AnimeId);

                if (!exists)
                {
                    await dbContext.AnimeReviews.AddAsync(review);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
