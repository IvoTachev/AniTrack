namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Admin;
    using AniTrack.Web.ViewModels.Admin.Home;
    using Moq;
    using MockQueryable;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class AdminHomeServiceTests
    {
        private Mock<IAnimeRepository> animeRepositoryMock;
        private Mock<IAnimeGenreRepository> animeGenreRepositoryMock;
        private HomeService homeService;

        [SetUp]
        public void Setup()
        {
            this.animeRepositoryMock = new Mock<IAnimeRepository>(MockBehavior.Strict);
            this.animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>(MockBehavior.Strict);
            this.homeService = new HomeService(this.animeRepositoryMock.Object, this.animeGenreRepositoryMock.Object);
        }

        [Test]
        // Test if GetAnimesForRestoreAsync returns only deleted animes
        public async Task GetAnimesForRestoreAsyncShouldReturnDeletedAnimes()
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
                    IsDeleted = true
                },
                new Anime
                {
                    Id = 2,
                    Title = "Bleach",
                    ImageUrl = "bleach.jpg",
                    Episodes = 366,
                    AirDate = new DateOnly(2004, 10, 5),
                    EndDate = new DateOnly(2012, 3, 27),
                    IsDeleted = false
                },
                new Anime
                {
                    Id = 3,
                    Title = "One Piece",
                    ImageUrl = "onepiece.jpg",
                    Episodes = 1000,
                    AirDate = new DateOnly(1999, 10, 20),
                    EndDate = null,
                    IsDeleted = true
                }
            };

            // Mock the repository to return the animes
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            // Create a list of the deleted animes
            List<Anime> deletedAnimes = animes.Where(a => a.IsDeleted).ToList();
           
            List<RestoreAnimesViewModel> result = await this.homeService.GetAnimesForRestoreAsync();

            // Assert that the result contains only the deleted anime
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Naruto"));
            Assert.That(result[1].Title, Is.EqualTo("One Piece"));
            Assert.That(result[0].EndDate, Is.EqualTo("2007-02-08"));
            Assert.That(result[1].EndDate, Is.EqualTo("???"));
        }

        [Test]
        // Test if GetAnimesForRestoreAsync returns empty list when no deleted animes exist
        public async Task GetAnimesForRestoreAsyncShouldReturnEmptyList()
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
                    IsDeleted = false
                }
            };
            // Mock the repository to return the animes
            IQueryable<Anime> animeQueryable = animes.BuildMock();
            this.animeRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(animeQueryable);

            List<RestoreAnimesViewModel> result = await this.homeService.GetAnimesForRestoreAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
