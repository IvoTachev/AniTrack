namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using AniTrack.Web.ViewModels.Genre;
    using Moq;
    using MockQueryable;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class GenreServiceTests
    {
        private Mock<IGenreRepository> genreRepositoryMock;
        private IGenreService genreService;

        [SetUp]
        public void Setup()
        {
            this.genreRepositoryMock = new Mock<IGenreRepository>(MockBehavior.Strict);
            this.genreService = new GenreService(this.genreRepositoryMock.Object);
        }

        [Test]
        // Test if method GetAnimesDetailsByGenreNameAsync returns null when genre is not found
        public async Task GetAnimesDetailsByGenreNameAsyncShouldReturnNull()
        {
            // Setup the mock to return null when genre is not found
            string genreName = "NonExistentGenre";
            this.genreRepositoryMock
                .Setup(r => r.GetByNameAsync(genreName))
                .ReturnsAsync((Genre?)null);

            GenreViewModel result = await this.genreService.GetAnimesDetailsByGenreNameAsync(genreName);

            Assert.IsNull(result);
        }

        [Test]
        // Test if method GetAnimesDetailsByGenreNameAsync returns correct GenreViewModel when genre is found
        public async Task GetAnimesDetailsByGenreNameAsyncShouldReturnGenreViewModel()
        {
            string genreName = "Action";
            Genre genre = new Genre
            {
                Id = 1,
                Name = genreName,
                AnimesGenre = new List<AnimeGenre>
                {
                    new AnimeGenre
                    {
                        Anime = new Anime
                        {
                            Id = 101,
                            Title = "Naruto",
                            ImageUrl = "naruto.jpg",
                            AirDate = new DateOnly(2002, 10, 3),
                            EndDate = new DateOnly(2007, 2, 8),
                            Episodes = 220
                        }
                    }
                }
            };
            // Setup the mock to return the genre with its animes
            this.genreRepositoryMock
                .Setup(r => r.GetByNameAsync(genreName))
                .ReturnsAsync(genre);

            GenreViewModel result = await this.genreService.GetAnimesDetailsByGenreNameAsync(genreName);

            Assert.IsNotNull(result);
            Assert.That(result.Genre, Is.EqualTo(genre));
            Assert.That(result.Animes, Is.Not.Null);
            Assert.That(result.Animes.Count, Is.EqualTo(1));
            Assert.That(result.Animes[0].Id, Is.EqualTo("101"));
            Assert.That(result.Animes[0].Title, Is.EqualTo("Naruto"));
            Assert.That(result.Animes[0].ImageUrl, Is.EqualTo("naruto.jpg"));
            Assert.That(result.Animes[0].AirDate, Is.EqualTo("2002-10-03"));
            Assert.That(result.Animes[0].EndDate, Is.EqualTo("2007-02-08"));
            Assert.That(result.Animes[0].Episodes, Is.EqualTo("220"));
        }

        [Test]
        // Test if method AddGenreAsync returns false when genre already exists
        public async Task AddGenreAsyncShouldReturnFalse()
        {
            // Setup the mock to return an existing genre
            string genreName = "Action";
            Genre existingGenre = new Genre { Name = genreName };
            genreRepositoryMock.Setup(r => r.GetByNameAsync(genreName)).ReturnsAsync(existingGenre);
           
            bool result = await genreService.AddGenreAsync(genreName);
            // Verify that the genre was not added
            Assert.IsFalse(result);
        }

        // Test if method AddGenreAsync adds a new genre when it does not exist
        [Test]
        public async Task AddGenreAsyncShouldReturnTrue()
        {
            // Setup the mock to return null for a non-existing genre
            string genreName = "Adventure";
            genreRepositoryMock.Setup(r => r.GetByNameAsync(genreName)).ReturnsAsync((Genre?)null);
            genreRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Genre>())).Returns(Task.CompletedTask);

            bool result = await genreService.AddGenreAsync(genreName);
            // Verify that the genre was added
            Assert.IsTrue(result);
            genreRepositoryMock.Verify(r => r.AddAsync(It.Is<Genre>(g => g.Name == genreName)), Times.Once);
        }

        // Test if method GetAllGenreDetailsForDeleteAsync returns null when selected genre does not exist
        [Test]
        public async Task GetAllGenreDetailsForDeleteAsyncShouldReturnNull()
        {
            // Setup the mock to return a list of genres without the selected genre
            List<Genre> genres = new List<Genre> { new Genre { Name = "Action" } };
            IQueryable<Genre> mock = genres.BuildMock();
            genreRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mock);
            // Attempt to get details for a non-existing genre
            DeleteGenreViewModel? result = await genreService.GetAllGenreDetailsForDeleteAsync("NonExistent");
            // Verify that the result is null
            Assert.IsNull(result);
        }
        // Test if method GetAllGenreDetailsForDeleteAsync returns view model when selected genre is null or exists
        [Test]
        public async Task GetAllGenreDetailsForDeleteAsyncShouldReturnViewMode()
        {
            // Setup the mock to return a list of genres
            List<Genre> genres = new List<Genre> { new Genre { Name = "Action" }, new Genre { Name = "Adventure" } };
            IQueryable<Genre> mock = genres.BuildMock();
            genreRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mock);
            // Attempt to get details for a null selected genre
            DeleteGenreViewModel? resultNull = await genreService.GetAllGenreDetailsForDeleteAsync(null);
            DeleteGenreViewModel? resultExists = await genreService.GetAllGenreDetailsForDeleteAsync("Action");
            // Verify that the result is not null and contains the expected data
            Assert.IsNotNull(resultNull);
            Assert.IsNotNull(resultExists);
            Assert.That(resultExists.SelectedGenreName,Is.EqualTo("Action"));
            Assert.That(resultExists.Genres.Count,Is.EqualTo(2));
        }

        // Test if method DeleteGenreByNameAsync returns false when genre does not exist
        [Test]
        public async Task DeleteGenreByNameAsyncShouldReturnFalse()
        {
            // Setup the mock to return null for a non-existing genre
            genreRepositoryMock.Setup(r => r.GetByNameAsync("NonExistent")).ReturnsAsync((Genre?)null);
            // Attempt to delete a non-existing genre
            bool result = await genreService.DeleteGenreByNameAsync("NonExistent");
            // Verify that the result is false
            Assert.IsFalse(result);
        }
        // Test if method DeleteGenreByNameAsync returns true when genre exists and is deleted
        [Test]
        public async Task DeleteGenreByNameAsyncShouldReturnTrue()
        {
            // Setup the mock to return an existing genre
            Genre genre = new Genre { Name = "Action" };
            genreRepositoryMock.Setup(r => r.GetByNameAsync("Action")).ReturnsAsync(genre);
            genreRepositoryMock.Setup(r => r.DeleteAsync(genre)).ReturnsAsync(true);
            // Attempt to delete the existing genre
            bool result = await genreService.DeleteGenreByNameAsync("Action");
            // Verify that the genre was deleted
            Assert.IsTrue(result);
        }

        [Test]
        // Test if method GetAllGenreDetailsForRestoreAsync returns null when selected genre does not exist
        public async Task GetAllGenreDetailsForRestoreAsyncShouldReturnNull()
        {
            // Setup the mock to return a list of genres with no deleted genres
            List<Genre> genres = new List<Genre> { new Genre { Name = "Action", IsDeleted = true } };
            IQueryable<Genre> mock = genres.BuildMock();
            genreRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mock);
            // Attempt to get details for a non-existing genre
            RestoreGenreViewModel? result = await genreService.GetAllGenreDetailsForRestoreAsync("NonExistent");
            // Verify that the result is null
            Assert.IsNull(result);
        }

        // Test if method GetAllGenreDetailsForRestoreAsync returns view model when selected genre is null or exists
        [Test]
        public async Task GetAllGenreDetailsForRestoreAsyncShouldReturnViewModel()
        {
            // Setup the mock to return a list of genres with deleted genres
            List<Genre> genres = new List<Genre>
            {
                new Genre { Name = "Action", IsDeleted = true },
                new Genre { Name = "Adventure", IsDeleted = true }
            };
            IQueryable<Genre> mock = genres.BuildMock();
            genreRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mock);
            /// Attempt to get details for a null selected genre
            RestoreGenreViewModel? resultNull = await genreService.GetAllGenreDetailsForRestoreAsync(null);
            RestoreGenreViewModel? resultExists = await genreService.GetAllGenreDetailsForRestoreAsync("Action");
            // Verify that the result is not null and contains the expected data
            Assert.IsNotNull(resultNull);
            Assert.IsNotNull(resultExists);
            Assert.That(resultExists.SelectedGenreName, Is.EqualTo("Action"));
            Assert.That(resultExists.Genres.Count,Is.EqualTo(2));
        }

        // Test if method RestoreGenreByNameAsync returns false when genre does not exist or is not deleted
        [Test]
        public async Task RestoreGenreByNameAsyncShouldReturnFalse()
        {
            // Setup the mock to return a list of genres with no deleted genres
            List<Genre> genres = new List<Genre>
            {
                new Genre { Name = "Action", IsDeleted = false }
            };
            IQueryable<Genre> mock = genres.BuildMock();
            genreRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mock);
            // Attempt to restore a non-existing or not deleted genre
            bool result = await genreService.RestoreGenreByNameAsync("NonExistent");
            // Verify that the result is false
            Assert.IsFalse(result);
        }
    }
}
