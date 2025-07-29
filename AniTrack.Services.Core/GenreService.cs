namespace AniTrack.Services.Core
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.ViewModels.Home;
    using System.Threading.Tasks;

    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
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
    }
}
