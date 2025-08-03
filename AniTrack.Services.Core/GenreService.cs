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

            if (genre == null)
            {
                return genreViewModel;
            }
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

            genreViewModel = new GenreViewModel
            {
                Genre = genre,
                Animes = genreAnimeViewModel
            };

            return genreViewModel;
        }

        public async Task<bool> DeleteGenreByNameAsync(string selectedGenre)
        {
            Genre? genre = await this.genreRepository.GetByNameAsync(selectedGenre);
            if (genre == null)
            {
                return false;
            }
            bool result = await this.genreRepository.DeleteAsync(genre);
            return result;
        }
    }
}
