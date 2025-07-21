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

        public async Task<AnimeDetailsViewModel?> GetAnimeDetailsAsync(string? id)
        {
            AnimeDetailsViewModel? animeDetails = null;
            bool isIdValid = int.TryParse(id, out int animeId);
            if (isIdValid)
            {
                animeDetails = await this.dbContext
                    .Animes
                    .AsNoTracking()
                    .Where(a => a.Id == animeId)
                    .Select(a => new AnimeDetailsViewModel()
                    {
                        Id = a.Id.ToString(),
                        Title = a.Title,
                        AirDate = a.AirDate.ToString(ApplicationDateFormat),
                        EndDate = a.EndDate.HasValue
                                            ? a.EndDate.Value.ToString(ApplicationDateFormat)
                                            : "???",
                        Synopsis = a.Synopsis,
                        ImageUrl = a.ImageUrl,
                        Episodes = a.Episodes,
                        Genres = a.AnimeGenres
                                  .Select(ag => new Genre
                                  {
                                      Id = ag.GenreId,
                                      Name = ag.Genre.Name
                                  })
                                  .ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            return animeDetails;
        }
    }
   
}
