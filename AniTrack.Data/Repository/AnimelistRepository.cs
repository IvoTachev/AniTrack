namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AnimelistRepository : BaseRepository<UserAnime, object>, IAnimelistRepository
    {
        public AnimelistRepository(AniTrackDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public UserAnime? GetByCompositeKey(string userId, string animeId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefault(ua => ua.UserId.ToString().ToLower() == userId.ToLower() &&
                                      ua.AnimeId.ToString().ToLower() == animeId.ToLower());
        }

        public Task<UserAnime?> GetByCompositeKeyAsync(string userId, string animeId)
        {
            return this
               .GetAllAttached()
               .SingleOrDefaultAsync(ua => ua.UserId.ToString().ToLower() == userId.ToLower() &&
                                     ua.AnimeId.ToString().ToLower() == animeId.ToLower());
        }
        public bool Exists(string userId, string animeId)
        {
            return this
                .GetAllAttached()
                .Any(ua => ua.UserId.ToString().ToLower() == userId.ToLower() &&
                                      ua.AnimeId.ToString().ToLower() == animeId.ToLower());
        }

        public Task<bool> ExistsAsync(string userId, string animeId)
        {
            return this
                .GetAllAttached()
                .AnyAsync(ua => ua.UserId.ToString().ToLower() == userId.ToLower() &&
                                      ua.AnimeId.ToString().ToLower() == animeId.ToLower());
        }
  
    }
}
