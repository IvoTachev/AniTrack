namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Genre;
    using AniTrack.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<bool> AddGenreAsync(string genreName)
        {
            Genre? existingGenre = await this.genreRepository.GetByNameAsync(genreName);

            if (existingGenre != null)
            {
                return false; // Genre already exists
            }

            Genre genre = new Genre
            {
                Name = genreName
            };
            await this.genreRepository.AddAsync(genre);
            return true; // Genre added successfully
        }

        public async Task<DeleteGenreViewModel?> GetAllGenreDetailsForDeleteAsync(string? selectedGenre)
        {
            // Retrieve all genres from the repository
            List<Genre> allGenres = await this.genreRepository.GetAllAttached().ToListAsync();
            // Genre can be null or empty, but if it is not null or empty, we need to check if the genre exists in the database.
            if (!string.IsNullOrEmpty(selectedGenre))
            { 
                bool doesGenreExist = allGenres.Any(g => g.Name == selectedGenre);
                if (!doesGenreExist)
                {
                    // If the selected genre does not exist, return null
                    return null;
                }
            }
            // If the selected genre is either null,empty or exists in the database, we proceed to create the view model.
            DeleteGenreViewModel deleteGenreViewModel = new DeleteGenreViewModel
            {
                Genres = allGenres,
                SelectedGenreName = selectedGenre
            };

            return deleteGenreViewModel;
        }



        public async Task<GenreViewModel> GetAnimesDetailsByGenreNameAsync(string genreName)
        {
            
            GenreViewModel genreViewModel = null!;
            Genre? genre = await this.genreRepository.GetByNameAsync(genreName);
            // If Genre with this name was not found, return null
            if (genre == null)
            {
                return genreViewModel;
            }
            // If Genre with this name was found,  proceed to create the view model.
            List<GenreAnimeViewModel> genreAnimeViewModel = genre.AnimesGenre
                .Select(ag => new GenreAnimeViewModel
            {
                Id = ag.Anime.Id.ToString(),
                Title = ag.Anime.Title,
                ImageUrl = ag.Anime.ImageUrl,
                AirDate = ag.Anime.AirDate.ToString("yyyy-MM-dd"),
                EndDate = ag.Anime.EndDate?.ToString("yyyy-MM-dd") ?? "???",
                Episodes = ag.Anime.Episodes.ToString()
            }).ToList();
            // Create the GenreViewModel with the genre and its associated animes
            genreViewModel = new GenreViewModel
            {
                Genre = genre,
                Animes = genreAnimeViewModel
            };
            // If the genre has no associated animes,  still return the genre with an empty list of animes.
            return genreViewModel;
        }

        public async Task<bool> DeleteGenreByNameAsync(string selectedGenre)
        {
            Genre? genre = await this.genreRepository.GetByNameAsync(selectedGenre);
            // If Genre with this name was not found, return false
            if (genre == null)
            {
                return false;
            }
            // Try to delete the genre, returns true if successful
            bool result = await this.genreRepository.DeleteAsync(genre);
            return result;
        }

        public async Task<RestoreGenreViewModel?> GetAllGenreDetailsForRestoreAsync(string? selectedGenre)
        {
            // Retrieve all soft deleted genres from the repository
            List<Genre> allDeletedGenres = await this.genreRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Where(g => g.IsDeleted)
                .ToListAsync();
            // Selected Genre can be null or empty, but if it is not null or empty, we need to check if the genre exists in the database.
            if (!string.IsNullOrEmpty(selectedGenre))
            {
                bool doesGenreExist = allDeletedGenres.Any(g => g.Name == selectedGenre);
                if (!doesGenreExist)
                {
                    // If the selected genre does not exist, return null
                    return null;
                }
            }
            // If the selected genre is either null,empty or exists in the database, we proceed to create the view model.
            RestoreGenreViewModel restoreGenreViewModel = new RestoreGenreViewModel
            {
                Genres = allDeletedGenres,
                SelectedGenreName = selectedGenre
            };

            return restoreGenreViewModel;
        }

        public async Task<bool> RestoreGenreByNameAsync(string selectedGenre)
        {
            Genre? genre = await this.genreRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Where(g => g.IsDeleted && g.Name == selectedGenre)
                .FirstOrDefaultAsync();
            // If Genre with this name was not found, return false
            if (genre == null)
            {
                return false;
            }
            // If Genre with this name was found, we proceed to restore it.
            genre.IsDeleted = false;
            bool result = await this.genreRepository.UpdateAsync(genre);
            return result;
        }
    }
}
