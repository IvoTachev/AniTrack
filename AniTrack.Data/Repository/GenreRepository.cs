namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class GenreRepository : BaseRepository<Genre, int>, IGenreRepository
    {
        public GenreRepository(AniTrackDbContext dbContext)
           : base(dbContext)
        {
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await this.dbSet
                .Include(g => g.AnimesGenre)
                .ThenInclude(ag => ag.Anime)
                .FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}
