using AniTrack.Web.ViewModels.Home;

namespace AniTrack.Services.Core.Interfaces
{
    public interface IGenreService
    {
        Task<GenreViewModel> GetAnimesDetailsByGenreNameAsync(string genreName);

        Task<bool> AddGenreAsync(string genreName);
    }
}
