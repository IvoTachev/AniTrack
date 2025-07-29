namespace AniTrack.Services.Core
{
    using AniTrack.Data;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Animelist;
    using Microsoft.EntityFrameworkCore;
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;

    public class AnimelistService : IAnimelistService
    {
        private readonly IAnimelistRepository animelistRepository;

        public AnimelistService(IAnimelistRepository animelistRepository)
        {
            this.animelistRepository = animelistRepository;
        }
        public async Task<IEnumerable<AnimelistViewModel>> GetAnimelistAsync(string userId)
        {
            IEnumerable<AnimelistViewModel> userAnimelist = await this.animelistRepository
                .GetAllAttached()
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
                    UserAnime? userAnime = await this.animelistRepository
                        .GetAllAttached()
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(au => au.UserId.ToLower() == userId.ToLower() &&
                                                                           au.AnimeId == validAnimeId);
                    if (userAnime != null)
                    {
                        userAnime.IsDeleted = false; // Restore the record if it exists
                        result = await this.animelistRepository.UpdateAsync(userAnime); // Update the record
                    }
                    else
                    {
                        userAnime = new UserAnime
                        {
                            UserId = userId,
                            AnimeId = validAnimeId
                        };
                        await this.animelistRepository.AddAsync(userAnime); // Add new record
                        result = true; // Successfully added new record
                    }
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
                    UserAnime? userAnime = await this
                        .animelistRepository                        
                        .SingleOrDefaultAsync(au => au.UserId.ToLower() == userId.ToLower() &&
                                                                           au.AnimeId == validAnimeId);
                    if (userAnime != null)
                    {
                        result = await this.animelistRepository
                            .DeleteAsync(userAnime); // Soft delete the record
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
                    UserAnime? userAnime = await this
                        .animelistRepository
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
