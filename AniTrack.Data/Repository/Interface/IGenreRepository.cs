namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IGenreRepository
        : IRepository<Genre, int>, IAsyncRepository<Genre, int>
    {
        public Task<Genre?> GetByNameAsync(string name);
    }
}
