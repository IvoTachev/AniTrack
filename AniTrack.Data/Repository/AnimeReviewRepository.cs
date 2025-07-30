namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AnimeReviewRepository: BaseRepository<AnimeReview, object>, IAnimeReviewRepository
    {
        public AnimeReviewRepository(AniTrackDbContext dbContext)
            :base(dbContext)
        {
            
        }

        public async Task<List<AnimeReview>> GetAllAnimeReviewsAsync()
        {
            return await this.dbSet
             .Include(an => an.Anime)
             .Include(au => au.Author)
             .ToListAsync();
        }
    }
}
