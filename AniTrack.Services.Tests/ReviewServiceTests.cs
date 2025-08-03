namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Review;
    using Moq;
    using MockQueryable;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IAnimeReviewRepository> reviewRepositoryMock;
        private Mock<IAnimeRepository> animeRepositoryMock;
        private IReviewService reviewService;

        [SetUp]
        public void Setup()
        {
            this.reviewRepositoryMock = new Mock<IAnimeReviewRepository>(MockBehavior.Strict);
            this.animeRepositoryMock = new Mock<IAnimeRepository>(MockBehavior.Strict);
            this.reviewService = new ReviewService(this.reviewRepositoryMock.Object, this.animeRepositoryMock.Object);
        }

        [Test]
        // Test if GetAllReviewsPagedAsync returns correct paged reviews
        public async Task GetAllReviewsPagedAsyncShouldReturnPagedReviews()
        {
            List<AnimeReview> reviews = new List<AnimeReview>
            {
                new AnimeReview
                {
                    AnimeId = 1,
                    Content = "Amazing anime!",
                    isAnimeRecommended = true,
                    Author = new ApplicationUser { UserName = "user1", Id = "u1" },
                    Anime = new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg" },
                    CreatedOn = DateTime.UtcNow.AddDays(-2)
                },
                new AnimeReview
                {
                    AnimeId = 2,
                    Content = "Not my favorite.",
                    isAnimeRecommended = false,
                    Author = new ApplicationUser { UserName = "user2", Id = "u2" },
                    Anime = new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg" },
                    CreatedOn = DateTime.UtcNow.AddDays(-1)
                },
                new AnimeReview
                {
                    AnimeId = 3,
                    Content = "One Piece is epic!",
                    isAnimeRecommended = true,
                    Author = new ApplicationUser { UserName = "user3", Id = "u3" },
                    Anime = new Anime { Id = 3, Title = "One Piece", ImageUrl = "onepiece.jpg" },
                    CreatedOn = DateTime.UtcNow
                }
            };

            this.reviewRepositoryMock
                .Setup(r => r.GetAllAnimeReviewsAsync())
                .ReturnsAsync(reviews);

            int page = 1;
            int pageSize = 2;
            ReviewPageViewModel result = await this.reviewService.GetAllReviewsPagedAsync(page, pageSize);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Reviews);
            Assert.That(result.Reviews.Count(), Is.EqualTo(2));
            Assert.That(result.CurrentPage, Is.EqualTo(page));
            Assert.That(result.TotalPages, Is.EqualTo(2));

            // Reviews should be ordered by CreatedOn descending
            AnimeReviewViewModel[] reviewArray = Enumerable.ToArray(result.Reviews);
            Assert.That(reviewArray[0].Content, Is.EqualTo("One Piece is epic!"));
            Assert.That(reviewArray[1].Content, Is.EqualTo("Not my favorite."));
        }

        [Test]
        // Test if GetAllReviewsPagedAsync returns empty list when no reviews exist
        public async Task GetAllReviewsPagedAsyncShouldReturnEmptyList()
        {
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAnimeReviewsAsync())
                .ReturnsAsync(new List<AnimeReview>());

            int page = 1;
            int pageSize = 5;
            ReviewPageViewModel result = await this.reviewService.GetAllReviewsPagedAsync(page, pageSize);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Reviews);
            Assert.That(result.CurrentPage, Is.EqualTo(page));
            Assert.That(result.TotalPages, Is.EqualTo(0));
        }

        [Test]
        // Test if GetUserReviewsPagedAsync returns correct paged user reviews
        public async Task GetUserReviewsPagedAsyncShouldReturnPagedUserReviews()
        {
            List<AnimeReview> reviews = new List<AnimeReview>
            {
                new AnimeReview
                {
                    AnimeId = 1,
                    Content = "Amazing anime!",
                    isAnimeRecommended = true,
                    Author = new ApplicationUser { UserName = "user1", Id = "u1" },
                    AuthorId = "u1",
                    Anime = new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg" },
                    CreatedOn = DateTime.UtcNow.AddDays(-2)
                },
                new AnimeReview
                {
                    AnimeId = 2,
                    Content = "Not my favorite.",
                    isAnimeRecommended = false,
                    Author = new ApplicationUser { UserName = "user2", Id = "u2" },
                    AuthorId = "u2",
                    Anime = new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg" },
                    CreatedOn = DateTime.UtcNow.AddDays(-1)
                },
                new AnimeReview
                {
                    AnimeId = 3,
                    Content = "One Piece is epic!",
                    isAnimeRecommended = true,
                    Author = new ApplicationUser { UserName = "user1", Id = "u1" },
                    AuthorId = "u1",
                    Anime = new Anime { Id = 3, Title = "One Piece", ImageUrl = "onepiece.jpg" },
                    CreatedOn = DateTime.UtcNow
                }
            };
            // Mock the repository to return the reviews
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAnimeReviewsAsync())
                .ReturnsAsync(reviews);

            int page = 1;
            int pageSize = 2;
            string username = "user1";
            ReviewUserPageViewModel result = await this.reviewService.GetUserReviewsPagedAsync(username, page, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Reviews.Count(), Is.EqualTo(2));
            Assert.That(result.CurrentPage, Is.EqualTo(page));
            Assert.That(result.TotalPages, Is.EqualTo(1));
            Assert.That(result.AuthorName, Is.EqualTo(username));
            Assert.That(result.AuthorId, Is.EqualTo("u1"));

            // Reviews should be ordered by CreatedOn descending
            AnimeReviewViewModel[] reviewArray = Enumerable.ToArray(result.Reviews);
            Assert.That(reviewArray[0].Content, Is.EqualTo("One Piece is epic!"));
            Assert.That(reviewArray[1].Content, Is.EqualTo("Amazing anime!"));
        }

        [Test]
        // Test if GetWriteFormAsync returns correct WriteFormViewModel
        public async Task GetWriteFormAsyncShouldReturnWriteFormViewModel()
        {
            Anime anime = new Anime
            {
                Id = 1,
                Title = "Naruto",
                ImageUrl = "naruto.jpg"
            };
            // Mock the anime repository to return the anime
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(anime);

            ReviewWriteViewModel result = await this.reviewService.GetWriteFormAsync("1");

            Assert.IsNotNull(result);
            Assert.That(result.AnimeId, Is.EqualTo(1));
            Assert.That(result.AnimeTitle, Is.EqualTo("Naruto"));
            Assert.That(result.AnimeImageUrl, Is.EqualTo("naruto.jpg"));
            Assert.That(result.Content, Is.EqualTo(string.Empty));
        }

        [Test]
        // Test if WriteReviewAsync calls AddAsync with correct parameters
        public async Task WriteReviewAsyncShouldCallAddAsyncWithCorrectReview()
        {
            ReviewWriteViewModel inputModel = new ReviewWriteViewModel
            {
                AnimeId = 1,
                AnimeTitle = "Naruto",
                AnimeImageUrl = "naruto.jpg",
                Content = "Great anime!",
                isAnimeRecommended = true
            };
            string userId = "u1";
            // Setup the anime repository to return the anime.
            this.reviewRepositoryMock
                .Setup(r => r.AddAsync(It.Is<AnimeReview>(ar =>
                    ar.Content == inputModel.Content &&
                    ar.isAnimeRecommended == inputModel.isAnimeRecommended &&
                    ar.AuthorId == userId &&
                    ar.AnimeId == inputModel.AnimeId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await this.reviewService.WriteReviewAsync(inputModel, userId);

            // Verify that AddAsync was called with the correct parameters
            this.reviewRepositoryMock.Verify();
        }

        [Test]
        // Test if GetEditFormAsync returns correct ReviewEditViewModel
        public async Task GetEditFormAsyncShouldReturnEditFormViewModel()
        {
            Anime anime = new Anime
            {
                Id = 1,
                Title = "Naruto",
                ImageUrl = "naruto.jpg"
            };
            AnimeReview review = new AnimeReview
            {
                AnimeId = 1,
                AuthorId = "u1",
                Content = "Great anime!",
                isAnimeRecommended = true
            };

            IQueryable<AnimeReview> reviewQueryable = new List<AnimeReview> { review }.BuildMock();

            // Setup the anime repository to return the anime.
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(anime);
            // Setup the review repository to return the review.
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(reviewQueryable);
            
            ReviewEditViewModel? result = await this.reviewService.GetEditFormAsync("1", "u1");

            Assert.IsNotNull(result);
            Assert.That(result.AnimeId, Is.EqualTo(1));
            Assert.That(result.AuthorId, Is.EqualTo("u1"));
            Assert.That(result.AnimeTitle, Is.EqualTo("Naruto"));
            Assert.That(result.AnimeImageUrl, Is.EqualTo("naruto.jpg"));
            Assert.That(result.Content, Is.EqualTo("Great anime!"));
            Assert.IsTrue(result.isAnimeRecommended);
        }

        [Test]
        // Test if GetEditFormAsync updates the view model correctly when review exists
        public async Task EditReviewAsyncShouldUpdateReview()
        {
            ReviewEditViewModel inputModel = new ReviewEditViewModel
            {
                AnimeId = 1,
                AuthorId = "u1",
                AnimeTitle = "Naruto",
                AnimeImageUrl = "naruto.jpg",
                Content = "Updated review!",
                isAnimeRecommended = false
            };

            AnimeReview review = new AnimeReview
            {
                AnimeId = 1,
                AuthorId = "u1",
                Content = "Old review",
                isAnimeRecommended = true
            };

            IQueryable<AnimeReview> reviewQueryable = new List<AnimeReview> { review }.BuildMock();
            // Setup the review repository to return the review.
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(reviewQueryable);
            // Setup the update method to be called with the updated review.
            this.reviewRepositoryMock
                .Setup(r => r.UpdateAsync(It.Is<AnimeReview>(ar =>
                    ar.Content == inputModel.Content &&
                    ar.isAnimeRecommended == inputModel.isAnimeRecommended)))
                .ReturnsAsync(true)
                .Verifiable();

            bool result = await this.reviewService.EditReviewAsync(inputModel);

            // Verify that the review was updated correctly
            Assert.IsTrue(result);
            this.reviewRepositoryMock.Verify();
        }

        [Test]
        // Test if EditReviewAsync returns false when review does not exist
        public async Task EditReviewAsyncShouldReturnFalse()
        {
            ReviewEditViewModel inputModel = new ReviewEditViewModel
            {
                AnimeId = 1,
                AuthorId = "u1",
                AnimeTitle = "Naruto",
                AnimeImageUrl = "naruto.jpg",
                Content = "Updated review!",
                isAnimeRecommended = false
            };

            IQueryable<AnimeReview> emptyQueryable = new List<AnimeReview>().BuildMock();
            // Setup the review repository to return an empty queryable.
            this.reviewRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(emptyQueryable);

            bool result = await this.reviewService.EditReviewAsync(inputModel);

            Assert.IsFalse(result);
        }
    }
}
