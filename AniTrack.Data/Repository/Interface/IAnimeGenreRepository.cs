namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimeGenreRepository 
        : IAsyncRepository<AnimeGenre, object>
    {
        // Gets all AnimeGenre entries for a specific anime ID, optionally including deleted entries.
        Task<List<AnimeGenre>> GetByAnimeIdAsync(int animeId, bool includeDeleted = false);
        // Hard deletes a list of AnimeGenre entries.
        Task HardDeleteList(IEnumerable<AnimeGenre> animeGenres);
    }
}