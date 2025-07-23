namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Animelist;
    public interface IAnimelistService
    {
        Task<IEnumerable<AnimelistViewModel>> GetAnimelistAsync(string userId);

        Task<bool> AddAnimeToUserAnimelistAsync(string? animeId, string? userId);
    }

}
