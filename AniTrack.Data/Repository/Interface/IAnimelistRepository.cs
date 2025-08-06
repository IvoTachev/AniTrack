namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimelistRepository
        :IRepository<UserAnime,object>,IAsyncRepository<UserAnime,object>
    {
        // Gets a UserAnime by composite key of userId and animeId.
        UserAnime? GetByCompositeKey(string userId, string animeId);
        // Asynchronously gets a UserAnime by composite key of userId and animeId.
        Task<UserAnime?> GetByCompositeKeyAsync(string userId, string animeId);
        // Checks if a UserAnime exists by composite key of userId and animeId.
        bool Exists(string userId, string animeId);
        // Asynchronously checks if a UserAnime exists by composite key of userId and animeId.
        Task<bool> ExistsAsync(string userId, string animeId);
    }
}
