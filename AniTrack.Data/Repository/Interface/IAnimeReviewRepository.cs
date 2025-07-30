namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;
    public interface IAnimeReviewRepository
        : IRepository<AnimeReview, object>, IAsyncRepository<AnimeReview, object>
    {

    }
}
