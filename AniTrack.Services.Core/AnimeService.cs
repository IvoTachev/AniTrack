namespace AniTrack.Services.Core
{
    using AniTrack.Data;
    using AniTrack.Data.Models;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using Web.ViewModels.Anime;
    using static AniTrack.GCommon.ApplicationConstants;
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
        public async Task AddAnimeAsync(AddAnimeFormModel inputModel)
        {
            Anime newAnime = new Anime
            {
                Title = inputModel.Title,
                Episodes = inputModel.Episodes,
                AirDate = DateOnly.ParseExact(inputModel.AirDate, ApplicationDateFormat, CultureInfo.InvariantCulture,DateTimeStyles.None), 
                EndDate = string.IsNullOrEmpty(inputModel.EndDate) ? null : DateOnly.ParseExact(inputModel.EndDate, ApplicationDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                Synopsis = inputModel.Synopsis,
                ImageUrl = inputModel.ImageUrl
            };

            await this.dbContext.Animes.AddAsync(newAnime);
            await this.dbContext.SaveChangesAsync();

            // Add AnimeGenre entries for each selected genre
            foreach (var genreId in inputModel.SelectedGenreIds)
            {
                var animeGenre = new AnimeGenre
                {
                    AnimeId = newAnime.Id,
                    GenreId = genreId
                };
                await this.dbContext.AnimesGenres.AddAsync(animeGenre);
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
   
}
