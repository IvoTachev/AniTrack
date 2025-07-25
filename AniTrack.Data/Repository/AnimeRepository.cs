namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;

    public class AnimeRepository : BaseRepository<Anime, int>, IAnimeRepository
    {
        public AnimeRepository(AniTrackDbContext dbContext)
            :base(dbContext)
        {
            
        }
    }
}
