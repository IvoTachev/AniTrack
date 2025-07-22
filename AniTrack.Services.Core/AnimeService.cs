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

        public async Task<EditAnimeFormModel?> GetAnimeDetailsByIdAsync(string? id)
        {
            EditAnimeFormModel? animeDetails = null; 
            bool isIdValid = int.TryParse(id, out int animeId);
            if (isIdValid)
            {
                animeDetails = await this.dbContext
                    .Animes
                    .AsNoTracking()
                    .Where(a => a.Id == animeId)
                    .Select(a => new EditAnimeFormModel()
                    {
                        Id = a.Id.ToString(),
                        Title = a.Title,
                        AirDate = a.AirDate.ToString(ApplicationDateFormat),
                        EndDate = a.EndDate.HasValue
                                            ? a.EndDate.Value.ToString(ApplicationDateFormat)
                                            : null,
                        Synopsis = a.Synopsis,
                        ImageUrl = a.ImageUrl,
                        Episodes = a.Episodes,
                        SelectedGenreIds = a.AnimeGenres
                                  .Select(ag => ag.GenreId)
                                  .ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            return animeDetails;
        }

        public async Task<bool> EditAnimeAsync(EditAnimeFormModel inputModel)
        {
            // Validate the input model
            if (!int.TryParse(inputModel.Id, out int animeId))
            {
                return false;
            }
            // Fetch the anime to edit, including its genres
            Anime? editableAnime = await this.dbContext
                .Animes
                .Include(a => a.AnimeGenres)
                .SingleOrDefaultAsync(a => a.Id == animeId);
            // If the anime does not exist, return false
            if (editableAnime == null)
            {
                return false;
            }
            // Update main properties
            editableAnime.Title = inputModel.Title;
            editableAnime.Episodes = inputModel.Episodes;
            editableAnime.AirDate = DateOnly.ParseExact(inputModel.AirDate, ApplicationDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            editableAnime.EndDate = string.IsNullOrEmpty(inputModel.EndDate)
                ? null
                : DateOnly.ParseExact(inputModel.EndDate, ApplicationDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            editableAnime.Synopsis = inputModel.Synopsis;
            editableAnime.ImageUrl = inputModel.ImageUrl;

            // Get all AnimeGenre entries for this anime, including deleted ones
            var allGenres = await this.dbContext.AnimesGenres
                .IgnoreQueryFilters()
                .Where(ag => ag.AnimeId == animeId)
                .ToListAsync();

            // Mark genres as deleted if not in selected
            foreach (var ag in allGenres.Where(ag => !inputModel.SelectedGenreIds.Contains(ag.GenreId) && !ag.IsDeleted))
            {
                ag.IsDeleted = true;
            }

            // For each selected genre, add or undelete as needed
            foreach (var genreId in inputModel.SelectedGenreIds)
            {
                var ag = allGenres.FirstOrDefault(x => x.GenreId == genreId);
                if (ag == null)
                {
                    // Create new AnimeGenre
                    var newAnimeGenre = new AnimeGenre
                    {
                        AnimeId = animeId,
                        GenreId = genreId,
                        IsDeleted = false
                    };
                    await this.dbContext.AnimesGenres.AddAsync(newAnimeGenre);
                }
                else if (ag.IsDeleted)
                {
                    // Undelete existing AnimeGenre
                    ag.IsDeleted = false;
                }
                // else: already present and not deleted, do nothing
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
   
}
