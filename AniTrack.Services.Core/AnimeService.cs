namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using Web.ViewModels.Anime;
    using static AniTrack.GCommon.ApplicationConstants;
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository animeRepository;
        private readonly IAnimeGenreRepository animeGenreRepository;
        private readonly IGenreRepository genreRepository;

        public AnimeService(IAnimeRepository animeRepository,
            IAnimeGenreRepository animeGenreRepository, IGenreRepository genreRepository)
        {
            this.animeRepository = animeRepository;
            this.animeGenreRepository = animeGenreRepository;
            this.genreRepository = genreRepository;
        }
        public async Task<IEnumerable<TopAnimesViewModel>> GetTopAnimesAsync()
        {
            IEnumerable<TopAnimesViewModel> topAnimes = await this.animeRepository
                .GetAllAttached()
                .AsNoTracking()
                .OrderByDescending(a => a.UserWatchlists.Count(uw => uw.IsDeleted == false)) //UserWatchlists is a collection of users who have watched this anime. Higher count = more popular
                .ThenBy(a => a.Title)
                .Select(a => new TopAnimesViewModel()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    ImageUrl = a.ImageUrl
                })
                .ToListAsync();

            return topAnimes;
        }

        public async Task<AnimePageViewModel> GetPagedAnimesAsync(int page, int pageSize)
        {
            List<TopAnimesViewModel> allAnimes = (await GetTopAnimesAsync()).ToList();
            int totalAnimes = allAnimes.Count;
            int totalPages = (int)Math.Ceiling(totalAnimes / (double)pageSize);

            List<TopAnimesViewModel> pagedAnimes = allAnimes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new AnimePageViewModel
            {
                Animes = pagedAnimes,
                CurrentPage = page,
                TotalPages = totalPages
            };
        }
        public async Task AddAnimeAsync(AddAnimeFormModel inputModel)
        {
            Anime newAnime = new Anime
            {
                Title = inputModel.Title,
                Episodes = inputModel.Episodes,
                AirDate = DateOnly.ParseExact(inputModel.AirDate, ApplicationDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                EndDate = string.IsNullOrEmpty(inputModel.EndDate) ? null : DateOnly.ParseExact(inputModel.EndDate, ApplicationDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
                Synopsis = inputModel.Synopsis,
                ImageUrl = inputModel.ImageUrl
            };

            await this.animeRepository.AddAsync(newAnime);


            // Add AnimeGenre entries for each selected genre
            foreach (int genreId in inputModel.SelectedGenreIds)
            {
                AnimeGenre animeGenre = new AnimeGenre
                {
                    AnimeId = newAnime.Id,
                    GenreId = genreId
                };
                await this.animeGenreRepository.AddAsync(animeGenre);
            }
        }

        public async Task<AnimeDetailsViewModel?> GetAnimeDetailsAsync(string? id)
        {
            AnimeDetailsViewModel? animeDetails = null;
            bool isIdValid = int.TryParse(id, out int animeId);
            if (isIdValid)
            {
                animeDetails = await this.animeRepository
                    .GetAllAttached()
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
                animeDetails = await this.animeRepository
                    .GetAllAttached()
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

            // Fetch the anime to edit
            Anime? editableAnime = await this.animeRepository.GetByIdAsync(animeId);
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

            // Update the anime entity in the repository
            await this.animeRepository.UpdateAsync(editableAnime);

            // Get all AnimeGenre entries for this anime, including deleted ones
            List<AnimeGenre> allGenres = await this.animeGenreRepository.GetByAnimeIdAsync(animeId, true);

            // Mark genres as deleted if not in selected
            foreach (AnimeGenre ag in allGenres.Where(ag => !inputModel.SelectedGenreIds.Contains(ag.GenreId) && !ag.IsDeleted))
            {
                ag.IsDeleted = true;
                await this.animeGenreRepository.UpdateAsync(ag);
            }

            // For each selected genre, add or undelete as needed
            foreach (int genreId in inputModel.SelectedGenreIds)
            {
                AnimeGenre? ag = allGenres.FirstOrDefault(x => x.GenreId == genreId);
                if (ag == null)
                {
                    AnimeGenre newAnimeGenre = new AnimeGenre
                    {
                        AnimeId = animeId,
                        GenreId = genreId,
                        IsDeleted = false
                    };
                    await this.animeGenreRepository.AddAsync(newAnimeGenre);
                }
                else if (ag.IsDeleted)
                {
                    ag.IsDeleted = false;
                    await this.animeGenreRepository.UpdateAsync(ag);
                }
                // else: already present and not deleted, do nothing
            }

            return true;
        }

        public async Task<DeleteAnimeViewModel?> GetAnimeDetailsForDeleteByIdAsync(string? id)
        {
            DeleteAnimeViewModel? animeDetails = null;
            bool isIdValid = int.TryParse(id, out int animeId);
            if (isIdValid)
            {
                animeDetails = await this.animeRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(a => a.Id == animeId)
                    .Select(a => new DeleteAnimeViewModel()
                    {
                        Id = a.Id.ToString(),
                        Title = a.Title,
                        ImageUrl = a.ImageUrl
                    })
                    .SingleOrDefaultAsync();
            }
            return animeDetails;
        }
        public async Task<bool> SoftDeleteAnimeAsync(string? id)
        {
            bool isIdValid = int.TryParse(id, out int animeId);
            if (!isIdValid)
            {
                return false;
            }

            Anime? animeForDelete = await this.animeRepository.GetByIdAsync(animeId);

            if (animeForDelete == null)
            {
                return false;
            }


            // Soft delete related AnimeGenre entries
            List<AnimeGenre> relatedGenres = await this.animeGenreRepository.GetByAnimeIdAsync(animeId, false);

            foreach (AnimeGenre ag in relatedGenres)
            {
                ag.IsDeleted = true;
                await this.animeGenreRepository.UpdateAsync(ag);
            }

            await this.animeRepository.DeleteAsync(animeForDelete);

            return true;
        }

        public async Task<DeleteAnimeViewModel?> GetAnimeDetailsForRestoreByIdAsync(string? id)
        {
            DeleteAnimeViewModel? animeDetails = null;
            bool isIdValid = int.TryParse(id, out int animeId);
            if (isIdValid)
            {
                animeDetails = await this.animeRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Where(a => a.Id == animeId && a.IsDeleted == true)
                    .Select(a => new DeleteAnimeViewModel()
                    {
                        Id = a.Id.ToString(),
                        Title = a.Title,
                        ImageUrl = a.ImageUrl
                    })
                    .SingleOrDefaultAsync();
            }
            return animeDetails;
        }

        public async Task<bool> RestoreAnimeAsync(string? id)
        {
            bool isIdValid = int.TryParse(id, out int animeId);
            if (!isIdValid)
            {
                return false;
            }

            Anime? animeForRestore = await this.animeRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.Id == animeId);
            if (animeForRestore == null || !animeForRestore.IsDeleted)
            {
                // Anime not found or not soft-deleted
                return false;
            }
            animeForRestore.IsDeleted = false;
            // Restore related AnimeGenre entries
            List<AnimeGenre> relatedGenres = await this.animeGenreRepository.GetByAnimeIdAsync(animeId, true);

            foreach (AnimeGenre ag in relatedGenres)
            {
                ag.IsDeleted = false;
                await this.animeGenreRepository.UpdateAsync(ag);
            }

            await this.animeRepository.UpdateAsync(animeForRestore);

            return true;
        }

        public async Task<bool> HardDeleteAnimeAsync(string? id)
        {
            bool isIdValid = int.TryParse(id, out int animeId);
            if (!isIdValid)
            {
                return false;
            }

            Anime? animeForDelete = await this.animeRepository.GetByIdAsync(animeId);

            if (animeForDelete == null)
            {
                return false;
            }

            // Remove all related AnimeGenre entries (including soft-deleted)
            List<AnimeGenre> relatedGenres = await this.animeGenreRepository.GetByAnimeIdAsync(animeId, true);

            await this.animeGenreRepository.HardDeleteList(relatedGenres);

            // Remove the anime itself
            await this.animeRepository.HardDeleteAsync(animeForDelete);

            return true;
        }

        public async Task<SearchAnimeViewModel> SearchAnimeByWordAsync(string? searchTerm)
        {
            SearchAnimeViewModel viewModel = new SearchAnimeViewModel
            {
                SearchTerm = searchTerm,
                Genres = await this.genreRepository.GetAllGenresWithCountAsync(),
                SearchResults = new List<Anime>()
            };
            if (searchTerm != null)
            {
                List<Anime> searchedAnimes = await this.animeRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(a => a.Title.ToLower().Contains(searchTerm.ToLower()))
                    .ToListAsync();
                if (searchedAnimes.Count > 0)
                {
                    viewModel.SearchResults = searchedAnimes;
                }
            }
            return viewModel;
        }
    }
}
