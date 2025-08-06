namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimeReviewRepository
        : IRepository<AnimeReview, object>, IAsyncRepository<AnimeReview, object>
    {
        // Gets all anime reviews asynchronously.
        public Task<List<AnimeReview>> GetAllAnimeReviewsAsync();
        // Gets an anime review by its ID asynchronously.
        public Task<AnimeReview?> GetAnimeReviewByAnimeIdAsync(string? id);
    }
}
