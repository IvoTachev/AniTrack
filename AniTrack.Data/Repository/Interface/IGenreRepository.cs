namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IGenreRepository
        : IRepository<Genre, int>, IAsyncRepository<Genre, int>
    {
        // Find a Genre by its name and include the related AnimeGenre entities.
        public Task<Genre?> GetByNameAsync(string name);
        // Get all Genres with their associated AnimeGenre entities.
        public Task<List<Genre>> GetAllGenresWithCountAsync();
    }
}
