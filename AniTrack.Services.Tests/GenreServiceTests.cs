namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using Moq;    

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
    }
}
