namespace AniTrack.Services.Core
{
    using AniTrack.Data;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Animelist;
    using Microsoft.EntityFrameworkCore;
    using AniTrack.Data.Models;

    public class AnimelistService : IAnimelistService
    {
        private readonly AniTrackDbContext dbContext;

        public AnimelistService(AniTrackDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AnimelistViewModel>> GetAnimelistAsync(string userId)
        {
            IEnumerable<AnimelistViewModel> userAnimelist = await this.dbContext
                .UsersAnimes
                .Include(au => au.Anime)
                .AsNoTracking()
                .Where(au => au.UserId.ToLower() == userId.ToLower())
                .Select(au => new AnimelistViewModel
                {
                    AnimeId = au.AnimeId.ToString(),
                    Title = au.Anime.Title,
                    ImageUrl = au.Anime.ImageUrl,
                    Episodes = au.Anime.Episodes,
                    Genres = au.Anime.AnimeGenres
                        .Select(g => new Genre
                        {
                            Id = g.GenreId,
                            Name = g.Genre.Name
                        })
                       .ToList()
                })
                .ToListAsync();

            return userAnimelist;
        }
        public async Task<bool> AddAnimeToUserAnimelistAsync(string? animeId, string? userId)
        {
            bool result = false;
            if (animeId != null && userId != null)
            {
                bool isAnimeIdValid = int.TryParse(animeId, out int validAnimeId);
                if (isAnimeIdValid)
                {
                    UserAnime? userAnime = await this.dbContext
                        .UsersAnimes
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(au => au.UserId.ToLower() == userId.ToLower() &&
                                                                           au.AnimeId == validAnimeId);
                    if (userAnime != null)
                    {
                        userAnime.IsDeleted = false; // Restore the record if it exists
                    }
                    else
                    {
                        userAnime = new UserAnime
                        {
                            UserId = userId,
                            AnimeId = validAnimeId
                        };
                        await this.dbContext.UsersAnimes.AddAsync(userAnime);
                    }

                    await this.dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            return result;
        }

        public async Task<bool> RemoveAnimeFromUserAnimelistAsync(string? animeId, string? userId)
        {
            bool result = false;
            if (animeId != null && userId != null)
            {
                bool isAnimeIdValid = int.TryParse(animeId, out int validAnimeId);
                if (isAnimeIdValid)
                {
                    UserAnime? userAnime = await this.dbContext
                        .UsersAnimes
                        .SingleOrDefaultAsync(au => au.UserId.ToLower() == userId.ToLower() &&
                                                                           au.AnimeId == validAnimeId);
                    if (userAnime != null)
                    {
                        userAnime.IsDeleted = true; //Soft delete the record
                        await this.dbContext.SaveChangesAsync();
                        result = true;
                    }
                }
            }
            return result;
        }

        public async Task<bool> IsAnimeInUserAnimelist(string? userId, string? animeId)
        {
            bool result = false;
            if (animeId != null && userId != null)
            {
                bool isAnimeIdValid = int.TryParse(animeId, out int validAnimeId);
                if (isAnimeIdValid)
                {
                    UserAnime? userAnime = await this.dbContext
                        .UsersAnimes
                        .SingleOrDefaultAsync(au => au.UserId.ToLower() == userId.ToLower() &&
                                                                           au.AnimeId == validAnimeId);

                    if (userAnime != null)
                    {
                        result = true; // Anime is in user's animelist
                    }
                }

            }
            return result;
        }
    }
}
