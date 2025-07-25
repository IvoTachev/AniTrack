namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimelistRepository
        :IRepository<UserAnime,object>,IAsyncRepository<UserAnime,object>
    {
        UserAnime? GetByCompositeKey(string userId, string animeId);

        Task<UserAnime?> GetByCompositeKeyAsync(string userId, string animeId);

        bool Exists(string userId, string animeId);

        Task<bool> ExistsAsync(string userId, string animeId);
    }
}
