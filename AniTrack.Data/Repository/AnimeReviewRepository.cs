namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;

    public class AnimeReviewRepository: BaseRepository<AnimeReview, object>, IAnimeReviewRepository
    {
        public AnimeReviewRepository(AniTrackDbContext dbContext)
            :base(dbContext)
        {
            
        }
    }
}
