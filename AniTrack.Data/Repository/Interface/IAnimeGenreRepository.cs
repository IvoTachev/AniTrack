namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimeGenreRepository : IAsyncRepository<AnimeGenre, object>
    {
    Task<List<AnimeGenre>> GetByAnimeIdAsync(int animeId, bool includeDeleted = false);
    Task HardDeleteList(IEnumerable<AnimeGenre> animeGenres);
    // Add other methods as needed
    }
}