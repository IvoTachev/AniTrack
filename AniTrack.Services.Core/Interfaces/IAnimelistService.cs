namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Animelist;
    public interface IAnimelistService
    {
        Task<IEnumerable<AnimelistViewModel>> GetAnimelistAsync(string username);

        Task<bool> AddAnimeToUserAnimelistAsync(string? animeId, string? userId);

        Task<bool> RemoveAnimeFromUserAnimelistAsync(string? animeId, string? userId);

        Task<bool> IsAnimeInUserAnimelist(string? userId, string? animeId);
    }

}
