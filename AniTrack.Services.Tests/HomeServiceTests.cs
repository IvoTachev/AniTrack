namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using Moq;
    using MockQueryable;

    [TestFixture]
    public class HomeServiceTests
    {
        private Mock<IAnimeReviewRepository> reviewRepositoryMock;
        private Mock<IAnimeRepository> animeRepositoryMock;
        private IHomeService homeService;

        [SetUp]
        public void Setup()
        {
            this.reviewRepositoryMock = new Mock<IAnimeReviewRepository>(MockBehavior.Strict);
            this.animeRepositoryMock = new Mock<IAnimeRepository>(MockBehavior.Strict);
            this.homeService = new HomeService(this.reviewRepositoryMock.Object, this.animeRepositoryMock.Object);
        }

        [Test]
        // Test if GetIndexViewModelAsync returns correct recommended animes and recent reviews
        public async Task GetIndexViewModelAsyncShouldReturnRecommendedAnimesAndRecentReviews()
        {
            List<Anime> animes = new List<Anime>
            {
                new Anime
                {
                    Id = 1,
                    Title = "Naruto",
                    ImageUrl = "naruto.jpg",
                    Episodes = 220,
                    AirDate = new DateOnly(2002, 10, 3),
                    EndDate = new DateOnly(2007, 2, 8),
                    Reviews = new List<AnimeReview>
                    {
                        new AnimeReview { isAnimeRecommended = true },
                        new AnimeReview { isAnimeRecommended = true }
                    }
                },
                new Anime
                {
                    Id = 2,
                    Title = "Bleach",
                    ImageUrl = "bleach.jpg",
                    Episodes = 366,
                    AirDate = new DateOnly(2004, 10, 5),
                    EndDate = new DateOnly(2012, 3, 27),
                    Reviews = new List<AnimeReview>
                    {
                        new AnimeReview { isAnimeRecommended = true }
                    }
                },
                new Anime
                {
                    Id = 3,
                    Title = "One Piece",
                    ImageUrl = "onepiece.jpg",
                    Episodes = 1000,
                    AirDate = new DateOnly(1999, 10, 20),
                    EndDate = null,
                    Reviews = new List<AnimeReview>
                    {
                        new AnimeReview { isAnimeRecommended = false }
                    }
                }
            };
            // Mock the anime repository to return the list of animes
            IQueryable<Anime> animeQueryableMock = animes.BuildMock();

            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryableMock);

            List<AnimeReview> reviews = new List<AnimeReview>
            {
                new AnimeReview
                {
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                    Author = new ApplicationUser { UserName = "user1" },
                    Anime = animes[0],
                    AnimeId = animes[0].Id,
                    Content = "Great anime!",
                    isAnimeRecommended = true
                },
                new AnimeReview
                {
                    CreatedOn = DateTime.UtcNow,
                    Author = new ApplicationUser { UserName = "user2" },
                    Anime = animes[1],
                    AnimeId = animes[1].Id,
                    Content = "Awesome!",
                    isAnimeRecommended = true
                },
                new AnimeReview
                {
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    Author = new ApplicationUser { UserName = "user3" },
                    Anime = animes[2],
                    AnimeId = animes[2].Id,
                    Content = "Not bad.",
                    isAnimeRecommended = false
                }
            };
            // Mock the review repository to return the list of reviews
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAnimeReviewsAsync())
                .ReturnsAsync(reviews);


            HomeIndexViewModel result = await this.homeService.GetIndexViewModelAsync();

            // Check if the result is not null and contains the expected data
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.RecommendedAnimes);
            Assert.That(result.RecommendedAnimes.Count, Is.EqualTo(3));
            Assert.That(result.RecommendedAnimes[0].AnimeTitle, Is.EqualTo("Naruto"));
            Assert.That(result.RecommendedAnimes[1].AnimeTitle, Is.EqualTo("Bleach"));
            Assert.That(result.RecommendedAnimes[2].AnimeTitle, Is.EqualTo("One Piece"));

            Assert.IsNotNull(result.RecentReviews);
            Assert.That(result.RecentReviews.Count, Is.EqualTo(3));
            Assert.That(result.RecentReviews[0].AuthorName, Is.EqualTo("user2")); // Most recent
            Assert.That(result.RecentReviews[1].AuthorName, Is.EqualTo("user1"));
            Assert.That(result.RecentReviews[2].AuthorName, Is.EqualTo("user3"));
        }

        [Test]
        // Test if GetIndexViewModelAsync returns empty lists when no animes or reviews exist
        public async Task GetIndexViewModelAsyncShouldReturnEmptyLists()
        {
            IQueryable<Anime> emptyAnimeQueryable = new List<Anime>().BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(emptyAnimeQueryable);

            this.reviewRepositoryMock
                .Setup(r => r.GetAllAnimeReviewsAsync())
                .ReturnsAsync(new List<AnimeReview>());

            HomeIndexViewModel result = await this.homeService.GetIndexViewModelAsync();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.RecommendedAnimes);
            Assert.That(result.RecommendedAnimes.Count, Is.EqualTo(0));
            Assert.IsNotNull(result.RecentReviews);
            Assert.That(result.RecentReviews.Count, Is.EqualTo(0));
        }
    }
}
