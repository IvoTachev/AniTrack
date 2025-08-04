namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Web.ViewModels.Anime;
    using AniTrack.Web.ViewModels.Review;
    using Moq;
    using MockQueryable;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class AnimeServiceTests
    {
        private Mock<IAnimeRepository> animeRepositoryMock;
        private Mock<IAnimeGenreRepository> animeGenreRepositoryMock;
        private Mock<IGenreRepository> genreRepositoryMock;
        private Mock<IAnimeReviewRepository> reviewRepositoryMock;
        private AnimeService animeService;

        [SetUp]
        public void Setup()
        {
            this.animeRepositoryMock = new Mock<IAnimeRepository>(MockBehavior.Strict);
            this.animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>(MockBehavior.Strict);
            this.genreRepositoryMock = new Mock<IGenreRepository>(MockBehavior.Strict);
            this.reviewRepositoryMock = new Mock<IAnimeReviewRepository>(MockBehavior.Strict);
            this.animeService = new AnimeService(
                this.animeRepositoryMock.Object,
                this.animeGenreRepositoryMock.Object,
                this.genreRepositoryMock.Object,
                this.reviewRepositoryMock.Object
            );
        }

        // GetTopAnimesAsync
        [Test]
        // Test if GetTopAnimesAsync returns top animes ordered by UserWatchlists count
        public async Task GetTopAnimesAsyncShouldReturnOrderedTopAnimes()
        {
            List<Anime> animes = new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg", UserWatchlists = new List<UserAnime> { new UserAnime(), new UserAnime() } }, // 2nd most popular
                new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg", UserWatchlists = new List<UserAnime> { new UserAnime() } }, // Least popular
                new Anime { Id = 3, Title = "One Piece", ImageUrl = "onepiece.jpg", UserWatchlists = new List<UserAnime> { new UserAnime(), new UserAnime(), new UserAnime() } } // Most popular
            };
            // Setup the mock to return the animes
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            IEnumerable<TopAnimesViewModel> result = await this.animeService.GetTopAnimesAsync();

            // Assert that the result is not null and contains the expected number of animes in the correct order
            Assert.IsNotNull(result);
            List<TopAnimesViewModel> list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[0].Title, Is.EqualTo("One Piece"));
            Assert.That(list[1].Title, Is.EqualTo("Naruto"));
            Assert.That(list[2].Title, Is.EqualTo("Bleach"));
        }

        // GetPagedAnimesAsync
        [Test]
        // Test if GetPagedAnimesAsync returns correct page and total pages
        public async Task GetPagedAnimesAsyncShouldReturnPagedResult()
        {
            List<Anime> animes = new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg", UserWatchlists = new List<UserAnime> { new UserAnime() } },
                new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg", UserWatchlists = new List<UserAnime> { new UserAnime() } },
                new Anime { Id = 3, Title = "One Piece", ImageUrl = "onepiece.jpg", UserWatchlists = new List<UserAnime> { new UserAnime() } }
            };

            // Setup the mock to return the animes
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            AnimePageViewModel result = await this.animeService.GetPagedAnimesAsync(1, 2);

            // Assert that the result is not null and contains the expected number of animes, current page, and total pages
            Assert.IsNotNull(result);
            Assert.That(result.Animes.Count, Is.EqualTo(2));
            Assert.That(result.CurrentPage, Is.EqualTo(1));
            Assert.That(result.TotalPages, Is.EqualTo(2));
        }

        // AddAnimeAsync
        [Test]
        // Test if AddAnimeAsync adds anime and genres
        public async Task AddAnimeAsyncShouldAddAnimeAndGenres()
        {
            AddAnimeFormModel inputModel = new AddAnimeFormModel
            {
                Title = "Naruto",
                Episodes = 220,
                AirDate = "2002-10-03",
                EndDate = "2007-02-08",
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                SelectedGenreIds = new List<int> { 1, 2 }
            };

            // Setup the mock to add anime and genre associations
            this.animeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Anime>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            
            this.animeGenreRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<AnimeGenre>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await this.animeService.AddAnimeAsync(inputModel);

            // Verify that the anime and genres associations were added
            this.animeRepositoryMock.Verify();
            this.animeGenreRepositoryMock.Verify();
        }

        [Test]
        // Test if AddAnimeAsync works with empty genre list
        public async Task AddAnimeAsyncShouldWorkWithEmptyGenreList()
        {
            AddAnimeFormModel inputModel = new AddAnimeFormModel
            {
                Title = "Naruto",
                Episodes = 220,
                AirDate = "2002-10-03",
                EndDate = "2007-02-08",
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                SelectedGenreIds = new List<int>()
            };
            // Setup the mock to add anime without genres
            this.animeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Anime>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await this.animeService.AddAnimeAsync(inputModel);

            this.animeRepositoryMock.Verify();
            // No genres to add, so no need to verify animeGenreRepositoryMock
        }

        // GetAnimeDetailsAsync
        [Test]
        // Test if GetAnimeDetailsAsync returns correct details
        public async Task GetAnimeDetailsAsyncShouldReturnDetails()
        {
            int animeId = 1;
            List<Anime> animes = new List<Anime>
            {
                new Anime
                {
                    Id = animeId,
                    Title = "Naruto",
                    AirDate = new DateOnly(2002, 10, 3),
                    EndDate = new DateOnly(2007, 2, 8),
                    Synopsis = "Ninja story",
                    ImageUrl = "naruto.jpg",
                    Episodes = 220,
                    AnimeGenres = new List<AnimeGenre>
                    {
                        new AnimeGenre { GenreId = 1, Genre = new Genre { Id = 1, Name = "Action" } }
                    }
                }
            };

            // Setup the mock to return the anime details
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            AnimeDetailsViewModel? result = await this.animeService.GetAnimeDetailsAsync(animeId.ToString());

            // Assert that the result is not null and contains the expected details
            Assert.NotNull(result);
            Assert.That(result.Title, Is.EqualTo("Naruto"));
            Assert.That(result.Genres!.Count, Is.EqualTo(1));
            Assert.That(result.Genres[0].Name, Is.EqualTo("Action"));
        }

        [Test]
        // Test if GetAnimeDetailsAsync returns null for invalid id
        public async Task GetAnimeDetailsAsyncShouldReturnNullForInvalidId()
        {
            AnimeDetailsViewModel? result = await this.animeService.GetAnimeDetailsAsync("notanint");
            Assert.That(result, Is.Null);
        }

        // GetAnimeDetailsWithReviewViewModelAsync
        [Test]
        // Test if GetAnimeDetailsWithReviewViewModelAsync returns details and review
        public async Task GetAnimeDetailsWithReviewViewModelAsyncShouldReturnDetailsAndReview()
        {
            int animeId = 1;
            string animeIdStr = animeId.ToString();
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                AirDate = new DateOnly(2002, 10, 3),
                EndDate = new DateOnly(2007, 2, 8),
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                AnimeGenres = new List<AnimeGenre>()
            };

            // Setup the mock to return the anime details
            IQueryable<Anime> animeQueryable = new List<Anime> { anime }.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            AnimeReview review = new AnimeReview
            {
                AnimeId = animeId,
                Anime = anime,
                Author = new ApplicationUser { UserName = "user1" },
                Content = "Great anime!",
                isAnimeRecommended = true
            };
            this.reviewRepositoryMock
                .Setup(r => r.GetAnimeReviewByAnimeIdAsync(animeIdStr))
                .ReturnsAsync(review);

            AnimeDetailsWithReviewViewModel result = await this.animeService.GetAnimeDetailsWithReviewViewModelAsync(animeIdStr);

            // Assert that the result is not null and contains the expected details and review
            Assert.IsNotNull(result);
            Assert.That(result.AnimeDetails, Is.Not.Null);
            Assert.That(result.AnimeDetails.Title, Is.EqualTo("Naruto"));
            Assert.That(result.ReviewDetails, Is.Not.Null);
            Assert.That(result.ReviewDetails.Content, Is.EqualTo("Great anime!"));
            Assert.That(result.ReviewDetails.AuthorName, Is.EqualTo("user1"));
        }

        // GetAnimeDetailsByIdAsync
        [Test]
        // Test if GetAnimeDetailsByIdAsync returns edit form model
        public async Task GetAnimeDetailsByIdAsyncShouldReturnEditFormModel()
        {
            int animeId = 1;
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                AirDate = new DateOnly(2002, 10, 3),
                EndDate = new DateOnly(2007, 2, 8),
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                AnimeGenres = new List<AnimeGenre>
                {
                    new AnimeGenre { GenreId = 1 }
                }
            };
            // Setup the mock to return the anime details
            IQueryable<Anime> animeQueryable = new List<Anime> { anime }.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);
            // Setup the mock to return all genres
            this.genreRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<Genre> { new Genre { Id = 1} }.BuildMock());

            EditAnimeFormModel? result = await this.animeService.GetAnimeDetailsByIdAsync(animeId.ToString());

            // Assert that the result is not null and contains the expected details
            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Naruto"));
            Assert.That(result.SelectedGenreIds.Count, Is.EqualTo(1));
            Assert.That(result.SelectedGenreIds[0], Is.EqualTo(1));
        }

        // EditAnimeAsync
        [Test]
        // Test if EditAnimeAsync returns false for invalid id
        public async Task EditAnimeAsyncShouldReturnFalseForInvalidId()
        {
            EditAnimeFormModel inputModel = new EditAnimeFormModel
            {
                Id = "notanint",
                Title = "Naruto",
                AirDate = "2002-10-03",
                EndDate = "2007-02-08",
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                SelectedGenreIds = new List<int> { 1 }
            };

            bool result = await this.animeService.EditAnimeAsync(inputModel);
            Assert.That(result, Is.False);
        }

        [Test]
        // Test if EditAnimeAsync returns false if anime not found
        public async Task EditAnimeAsyncShouldReturnFalseIfAnimeNotFound()
        {
            EditAnimeFormModel inputModel = new EditAnimeFormModel
            {
                Id = "1",
                Title = "Naruto",
                AirDate = "2002-10-03",
                EndDate = "2007-02-08",
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                SelectedGenreIds = new List<int> { 1 }
            };
            // Setup the mock to return null for the anime
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Anime?)null);

            bool result = await this.animeService.EditAnimeAsync(inputModel);

            Assert.IsFalse(result);
        }

        [Test]
        // Test if EditAnimeAsync successfully edits anime and genres
        public async Task EditAnimeAsyncShouldEditAnimeAndGenres()
        {
            int animeId = 1;
            EditAnimeFormModel inputModel = new EditAnimeFormModel
            {
                Id = animeId.ToString(),
                Title = "Naruto",
                AirDate = "2002-10-03",
                EndDate = "2007-02-08",
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                SelectedGenreIds = new List<int> { 1, 2 } // 1 is kept, 2 is new, 3 is removed
            };
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                AirDate = new DateOnly(2002, 10, 3),
                EndDate = new DateOnly(2007, 2, 8),
                Synopsis = "Ninja story",
                ImageUrl = "naruto.jpg",
                Episodes = 220
            };
            // Existing genres: 1 (kept), 3 (should be hard deleted)
            List<AnimeGenre> allGenres = new List<AnimeGenre>
            {
                new AnimeGenre { AnimeId = animeId, GenreId = 1, IsDeleted = false },
                new AnimeGenre { AnimeId = animeId, GenreId = 3, IsDeleted = false }
            };

            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(animeId))
                .ReturnsAsync(anime);
            this.animeRepositoryMock
                .Setup(r => r.UpdateAsync(anime))
                .ReturnsAsync(true)
                .Verifiable();

            this.animeGenreRepositoryMock
                .Setup(r => r.GetByAnimeIdAsync(animeId, true))
                .ReturnsAsync(allGenres);       

            // Expect hard delete for genreId 3
            this.animeGenreRepositoryMock
                .Setup(r => r.HardDeleteAsync(It.Is<AnimeGenre>(ag => ag.GenreId == 3)))
                .ReturnsAsync(true)
                .Verifiable();

            // Expect add for genreId 2 (new)
            this.animeGenreRepositoryMock
                .Setup(r => r.AddAsync(It.Is<AnimeGenre>(ag => ag.GenreId == 2)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // No update or delete for genreId 1 (kept)

            bool result = await this.animeService.EditAnimeAsync(inputModel);

            Assert.IsTrue(result);
            this.animeRepositoryMock.Verify();
            this.animeGenreRepositoryMock.Verify();
        }

        // GetAnimeDetailsForDeleteByIdAsync
        [Test]
        // Test if GetAnimeDetailsForDeleteByIdAsync returns delete view model
        public async Task GetAnimeDetailsForDeleteByIdAsyncShouldReturnDeleteViewModel()
        {
            int animeId = 1;
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                ImageUrl = "naruto.jpg"
            };
            IQueryable<Anime> animeQueryable = new List<Anime> { anime }.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            DeleteAnimeViewModel? result = await this.animeService.GetAnimeDetailsForDeleteByIdAsync(animeId.ToString());

            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Naruto"));
            Assert.That(result.ImageUrl, Is.EqualTo("naruto.jpg"));
        }

        // GetAnimeDetailsForRestoreByIdAsync
        [Test]
        // Test if GetAnimeDetailsForRestoreByIdAsync returns restore view model
        public async Task GetAnimeDetailsForRestoreByIdAsyncShouldReturnRestoreViewModel()
        {
            int animeId = 1;
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                ImageUrl = "naruto.jpg",
                IsDeleted = true
            };

            // Setup the mock to return the anime details
            IQueryable<Anime> animeQueryable = new List<Anime> { anime }.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            DeleteAnimeViewModel? result = await this.animeService.GetAnimeDetailsForRestoreByIdAsync(animeId.ToString());

            // Assert that the result is not null and contains the expected details
            Assert.NotNull(result);
            Assert.That(result.Title, Is.EqualTo("Naruto"));
            Assert.That(result.ImageUrl, Is.EqualTo("naruto.jpg"));
        }

        // RestoreAnimeAsync
        [Test]
        // Test if RestoreAnimeAsync restores anime and genres
        public async Task RestoreAnimeAsyncShouldRestoreAnimeAndGenres()
        {
            int animeId = 1;
            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                IsDeleted = true
            };
            IQueryable<Anime> animeQueryable = new List<Anime> { anime }.BuildMock();
            // Setup the mock to return the anime details
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);
            // Setup the mock to update the anime
            this.animeRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Anime>()))
                .ReturnsAsync(true)
                .Verifiable();
            var genre = new Genre
            {
                Id = 1,
                Name = "Action",
                IsDeleted = false
            };
            List <AnimeGenre> genres = new List<AnimeGenre>
            {
                new AnimeGenre { AnimeId = animeId, GenreId = 1, IsDeleted = true, Genre = genre }
            };
            // Setup the mock to return the genres
            this.animeGenreRepositoryMock
                .Setup(r => r.GetByAnimeIdAsync(animeId, true))
                .ReturnsAsync(genres);
            // Setup the mock to update the genres
            this.animeGenreRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<AnimeGenre>()))
                .ReturnsAsync(true)
                .Verifiable();

            bool result = await this.animeService.RestoreAnimeAsync(animeId.ToString());
            // Assert that the result is true and the anime and genres were updated
            Assert.IsTrue(result);
            this.animeRepositoryMock.Verify();
            this.animeGenreRepositoryMock.Verify();
        }

        // SoftDeleteAnimeAsync
        [Test]
        // Test if SoftDeleteAnimeAsync returns false for invalid id
        public async Task SoftDeleteAnimeAsyncShouldReturnFalseForInvalidId()
        {
            bool result = await this.animeService.SoftDeleteAnimeAsync("notanint");
            Assert.IsFalse(result);
        }

        [Test]
        // Test if SoftDeleteAnimeAsync returns false if anime not found
        public async Task SoftDeleteAnimeAsyncShouldReturnFalseIfAnimeNotFound()
        {
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Anime?)null);

            bool result = await this.animeService.SoftDeleteAnimeAsync("1");
            Assert.IsFalse(result);
        }

        [Test]
        public async Task SoftDeleteAnimeAsyncShouldSoftDeleteAnimeAndGenres()
        {
            int animeId = 1;
            Anime anime = new Anime { Id = animeId };
            List<AnimeGenre> genres = new List<AnimeGenre>
            {
                new AnimeGenre { AnimeId = animeId, GenreId = 1, IsDeleted = false }
            };
            // Setup the mock to return the anime and genres
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(animeId))
                .ReturnsAsync(anime);
            this.animeGenreRepositoryMock
                .Setup(r => r.GetByAnimeIdAsync(animeId, false))
                .ReturnsAsync(genres);
            // Setup the mock to update the genres and anime
            this.animeGenreRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<AnimeGenre>()))
                .ReturnsAsync(true)
                .Verifiable();
            this.animeRepositoryMock
                .Setup(r => r.DeleteAsync(anime))
                .ReturnsAsync(true)
                .Verifiable();

            bool result = await this.animeService.SoftDeleteAnimeAsync(animeId.ToString());
            // Assert that the result is true and the anime and genres were updated
            Assert.IsTrue(result);
            this.animeGenreRepositoryMock.Verify();
            this.animeRepositoryMock.Verify();
        }

        // HardDeleteAnimeAsync
        [Test]
        // Test if HardDeleteAnimeAsync returns false for invalid id
        public async Task HardDeleteAnimeAsyncShouldReturnFalseForInvalidId()
        {
            bool result = await this.animeService.HardDeleteAnimeAsync("notanint");
            Assert.IsFalse(result);
        }

        [Test]
        // Test if HardDeleteAnimeAsync returns false if anime not found
        public async Task HardDeleteAnimeAsyncShouldReturnFalseIfAnimeNotFound()
        {
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Anime?)null);

            bool result = await this.animeService.HardDeleteAnimeAsync("1");
            Assert.IsFalse(result);
        }

        [Test]
        public async Task HardDeleteAnimeAsyncShouldHardDeleteAnimeAndGenres()
        {
            int animeId = 1;
            Anime anime = new Anime { Id = animeId };
            List<AnimeGenre> genres = new List<AnimeGenre>
            {
                new AnimeGenre { AnimeId = animeId, GenreId = 1 }
            };
            // Setup the mock to return the anime and genres
            this.animeRepositoryMock
                .Setup(r => r.GetByIdAsync(animeId))
                .ReturnsAsync(anime);
            this.animeGenreRepositoryMock
                .Setup(r => r.GetByAnimeIdAsync(animeId, true))
                .ReturnsAsync(genres);
            this.animeGenreRepositoryMock
                .Setup(r => r.HardDeleteList(genres))
                .Returns(Task.CompletedTask)
                .Verifiable();
            this.animeRepositoryMock
                .Setup(r => r.HardDeleteAsync(anime))
                .ReturnsAsync(true)
                .Verifiable();

            bool result = await this.animeService.HardDeleteAnimeAsync(animeId.ToString());
            // Assert that the result is true and the anime and genres were deleted
            Assert.IsTrue(result);
            this.animeGenreRepositoryMock.Verify();
            this.animeRepositoryMock.Verify();
        }

        // SearchAnimeByWordAsync
        [Test]
        // Test if SearchAnimeByWordAsync returns correct results
        public async Task SearchAnimeByWordAsyncShouldReturnResults()
        {
            string searchTerm = "naruto";
            List<Anime> animes = new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg" },
                new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg" }
            };

            // Setup the mock to return the animes
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            this.genreRepositoryMock
                .Setup(r => r.GetAllGenresWithCountAsync())
                .ReturnsAsync(new List<Genre>());

            SearchAnimeViewModel result = await this.animeService.SearchAnimeByWordAsync(searchTerm);

            Assert.IsNotNull(result);
            Assert.That(result.SearchResults.Count, Is.EqualTo(1));
            Assert.That(result.SearchResults.First().Title, Is.EqualTo("Naruto"));
        }

        [Test]
        // Test if SearchAnimeByWordAsync returns empty results for no match
        public async Task SearchAnimeByWordAsyncShouldReturnEmptyResultsForNoMatch()
        {
            string searchTerm = "notfound";
            List<Anime> animes = new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", ImageUrl = "naruto.jpg" },
                new Anime { Id = 2, Title = "Bleach", ImageUrl = "bleach.jpg" }
            };
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);
            this.genreRepositoryMock
                .Setup(r => r.GetAllGenresWithCountAsync())
                .ReturnsAsync(new List<Genre>());

            SearchAnimeViewModel result = await this.animeService.SearchAnimeByWordAsync(searchTerm);

            Assert.NotNull(result);
            Assert.That(result.SearchResults.Count, Is.EqualTo(0));
        }

        [Test]
        // Test if SearchAnimeByWordAsync works with null search term
        public async Task SearchAnimeByWordAsyncShouldWorkWithNullSearchTerm()
        {
            this.genreRepositoryMock
                .Setup(r => r.GetAllGenresWithCountAsync())
                .ReturnsAsync(new List<Genre>());

            SearchAnimeViewModel result = await this.animeService.SearchAnimeByWordAsync(null);

            Assert.NotNull(result);
            Assert.That(result.SearchResults, Is.Empty);
        }
    }
}
