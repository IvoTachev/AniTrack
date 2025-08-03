namespace AniTrack.Services.Tests
{
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core;
    using AniTrack.Services.Core.Interfaces;

    using Moq;
    using MockQueryable;
    using AniTrack.Data.Models;
    using AniTrack.Web.ViewModels.Animelist;
    using System.Linq.Expressions;

    [TestFixture]
    public class AnimelistServiceTests
    {
        private Mock<IAnimelistRepository> animelistRepositoryMock;
        private Mock<IApplicationUserRepository> applicationUserRepositoryMock;
        private IAnimelistService animelistService;

        [SetUp]
        public void Setup()
        {
            this.animelistRepositoryMock = new Mock<IAnimelistRepository>(MockBehavior.Strict);
            this.applicationUserRepositoryMock = new Mock<IApplicationUserRepository>(MockBehavior.Strict);
            this.animelistService = new AnimelistService(
                this.animelistRepositoryMock.Object,
                this.applicationUserRepositoryMock.Object
            );
        
        }

        [Test]
        // Test to ensure the test framework is working correctly
        public void PassAlways()
        {
            Assert.Pass();
        }

        [Test]
        // Test if method GetAnimelistAsync returns an empty collection when no user is found
        public async Task GetAnimelistAsyncShouldReturnEmptyCollection()
        {   
            // Setup the mock to return an empty collection when GetAllAttached is called
            IQueryable<UserAnime> emptyUserAnimeQueryable = new List<UserAnime>().BuildMock();
            this.animelistRepositoryMock
                .Setup(al => al.GetAllAttached())   
                .Returns(emptyUserAnimeQueryable);
            // Setup the mock to return null when trying to get userId by username
            string nonExistentUsername = "nonexistentuser";
            this.applicationUserRepositoryMock
                .Setup(repo => repo.GetUserIdByUsernameAsync(nonExistentUsername))
                .ReturnsAsync((string?)null);

            IEnumerable<AnimelistViewModel> emptyViewModelCollection = await this.animelistService.GetAnimelistAsync(nonExistentUsername);

            Assert.IsNotNull(emptyViewModelCollection);
            Assert.That(emptyViewModelCollection.Count(), Is.EqualTo(0));
        }


        [Test]
        // Test if method GetAnimelistAsync returns user animelist when user exists
        public async Task GetAnimelistAsyncShouldReturnUserAnimelist()
        {
            
            string username = "existinguser";
            string userId = "user1";
            int animeId = 123;

            Anime anime = new Anime
            {
                Id = animeId,
                Title = "Naruto",
                ImageUrl = "naruto.jpg",
                Episodes = 220,
                AnimeGenres = new List<AnimeGenre>
                {
                    new AnimeGenre
                    {
                        GenreId = 1,
                        Genre = new Genre { Id = 1, Name = "Action" }
                    }
                }
            };

            UserAnime userAnime = new UserAnime
            {
                UserId = userId,
                AnimeId = animeId,
                Anime = anime
            };

            List<UserAnime> userAnimes = new List<UserAnime> { userAnime };
            IQueryable<UserAnime> userAnimeQueryable = userAnimes.BuildMock();
            // Setup the mock to return userId when GetUserIdByUsernameAsync is called
            this.applicationUserRepositoryMock
                .Setup(repo => repo.GetUserIdByUsernameAsync(username))
                .ReturnsAsync(userId);
            // Setup the mock to return a collection of UserAnime objects when GetAllAttached is called
            this.animelistRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(userAnimeQueryable);

     
            IEnumerable<AnimelistViewModel> result = await this.animelistService.GetAnimelistAsync(username);

            // Verify the data returned
            Assert.IsNotNull(result);
            List<AnimelistViewModel> list = result.ToList();
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0].AnimeId, Is.EqualTo(animeId.ToString()));
            Assert.That(list[0].Title, Is.EqualTo("Naruto"));
            Assert.That(list[0].ImageUrl, Is.EqualTo("naruto.jpg"));
            Assert.That(list[0].Episodes, Is.EqualTo(220));
            Assert.IsNotNull(list[0].Genres);
            Assert.That(list[0].Genres[0].Name, Is.EqualTo("Action"));
        }

        [Test]
        // Test if method AddAnimeToUserAnimelistAsync adds a new record when the anime is not in the user's animelist
        public async Task AddAnimeToUserAnimelistAsyncShouldAddNewRecord()
        {
            string userId = "user1";
            string animeId = "123";
            int validAnimeId = 123;
            // Setup the mock to return an empty collection when GetAllAttached is called
            this.animelistRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<UserAnime>().BuildMock());
            // Setup the mock to return true when trying to add a new UserAnime
            this.animelistRepositoryMock
                .Setup(r => r.AddAsync(It.Is<UserAnime>(ua => ua.UserId == userId && ua.AnimeId == validAnimeId)))
                .Returns(Task.CompletedTask);

            bool result = await this.animelistService.AddAnimeToUserAnimelistAsync(animeId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        // Test if method AddAnimeToUserAnimelistAsync restores a deleted record when the anime is already in the user's animelist
        public async Task AddAnimeToUserAnimelistAsyncShouldRestoreDeletedRecord()
        {
            string userId = "user1";
            string animeId = "123";
            int validAnimeId = 123;
            UserAnime userAnime = new UserAnime { UserId = userId, AnimeId = validAnimeId, IsDeleted = true };

            IQueryable<UserAnime> mockQueryable = new List<UserAnime> { userAnime }.BuildMock();
            // Setup the mock to return a UserAnime object when queried
            this.animelistRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockQueryable);
            // Setup the mock to return true when trying to update the UserAnime
            this.animelistRepositoryMock
                .Setup(r => r.UpdateAsync(It.Is<UserAnime>(ua => ua.UserId == userId && ua.AnimeId == validAnimeId && ua.IsDeleted == false)))
                .ReturnsAsync(true);

            bool result = await this.animelistService.AddAnimeToUserAnimelistAsync(animeId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        // Test if method AddAnimeToUserAnimelistAsync returns false when the animeId or userId is invalid
        public async Task AddAnimeToUserAnimelistAsyncShouldReturnFalse()
        {
            bool result = await this.animelistService.AddAnimeToUserAnimelistAsync(null, null);
            Assert.IsFalse(result);

            result = await this.animelistService.AddAnimeToUserAnimelistAsync("notanint", "user1");
            Assert.IsFalse(result);
        }

        [Test]
        // Test if method RemoveAnimeFromUserAnimelistAsync deletes the record when the anime is in the user's animelist
        public async Task RemoveAnimeFromUserAnimelistAsyncShouldDeleteRecord()
        {
            string userId = "user1";
            string animeId = "123";
            int validAnimeId = 123;
            UserAnime userAnime = new UserAnime { UserId = userId, AnimeId = validAnimeId };

            // Setup the mock to return a UserAnime object when queried
            this.animelistRepositoryMock
                .Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserAnime, bool>>>()))
                .ReturnsAsync(userAnime);
            // Setup the mock to return true when trying to delete the UserAnime
            this.animelistRepositoryMock
                .Setup(r => r.DeleteAsync(userAnime))
                .ReturnsAsync(true);

            bool result = await this.animelistService.RemoveAnimeFromUserAnimelistAsync(animeId, userId);

            Assert.That(result, Is.True);
        }

        [Test]
        // Test if method RemoveAnimeFromUserAnimelistAsync returns false when the anime is not in the user's animelist
        public async Task RemoveAnimeFromUserAnimelistAsyncShouldReturnFalse()
        {
            string userId = "user1";
            string animeId = "123";

            // Setup the mock to return null when trying to find the UserAnime
            this.animelistRepositoryMock
                .Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserAnime, bool>>>()))
                .ReturnsAsync((UserAnime?)null);

            bool result = await this.animelistService.RemoveAnimeFromUserAnimelistAsync(animeId, userId);

            Assert.IsFalse(result);
        }

        [Test]
        // Test if method IsAnimeInUserAnimelist returns true when the anime is in the user's animelist
        public async Task IsAnimeInUserAnimelistShouldReturnTrue()
        {
            string userId = "user1";
            string animeId = "123";
            int validAnimeId = 123;
            var userAnime = new UserAnime { UserId = userId, AnimeId = validAnimeId };

            // Setup the mock to return a UserAnime object when queried
            this.animelistRepositoryMock
                .Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserAnime, bool>>>()))
                .ReturnsAsync(userAnime);

            bool result = await this.animelistService.IsAnimeInUserAnimelist(userId, animeId);

            Assert.IsTrue(result);
        }

        [Test]
        // Test if method IsAnimeInUserAnimelist returns false when the anime is not in the user's animelist
        public async Task IsAnimeInUserAnimelistShouldReturnFalse()
        {
            string userId = "user1";
            string animeId = "123";

            // Setup the mock to return null when queried
            this.animelistRepositoryMock
                .Setup(r => r.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserAnime, bool>>>()))
                .ReturnsAsync((UserAnime?)null);

            bool result = await this.animelistService.IsAnimeInUserAnimelist(userId, animeId);

            Assert.IsFalse(result);
        }
    }
}
