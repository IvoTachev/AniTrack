namespace AniTrack.Services.Core
{
    using AniTrack.Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Anime;
    public class AnimeService : IAnimeService
    {
        private readonly AniTrackDbContext dbContext;
        public AnimeService(AniTrackDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<TopAnimesViewModel>> GetTopAnimesAsync()
        {
            IEnumerable<TopAnimesViewModel> topAnimes = await this.dbContext
                .Animes
                .AsNoTracking()
                .Take(10)
                .Select(a => new TopAnimesViewModel()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    ImageUrl = a.ImageUrl
                })
                .ToListAsync();

            return topAnimes;
        }
    }
}
