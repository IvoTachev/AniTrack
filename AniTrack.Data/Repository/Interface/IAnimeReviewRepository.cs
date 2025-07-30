namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimeReviewRepository
        : IRepository<AnimeReview, object>, IAsyncRepository<AnimeReview, object>
    {
        public Task<List<AnimeReview>> GetAllAnimeReviewsAsync();

        public Task<AnimeReview?> GetAnimeReviewByAnimeIdAsync(string? id);
    }
}
