namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;

    public class AnimeGenreRepository : BaseRepository<AnimeGenre, object>, IAnimeGenreRepository
    {
        public AnimeGenreRepository(AniTrackDbContext dbContext) : base(dbContext) { }

        public async Task<List<AnimeGenre>> GetByAnimeIdAsync(int animeId, bool includeDeleted = false)
        {
            var query = this.GetAllAttached().Where(ag => ag.AnimeId == animeId);
            if (includeDeleted)
                query = query.IgnoreQueryFilters();
            return await query.ToListAsync();
        }

        public async Task HardDeleteList(IEnumerable<AnimeGenre> animeGenres)
        {
            this.dbContext.AnimesGenres.RemoveRange(animeGenres);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
