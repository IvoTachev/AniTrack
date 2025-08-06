namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Animelist;
    public interface IAnimelistService
    {
        // Gets the view model for the user's animelist by their username
        Task<IEnumerable<AnimelistViewModel>> GetAnimelistAsync(string username);
        // Adds an anime to the user's animelist
        Task<bool> AddAnimeToUserAnimelistAsync(string? animeId, string? userId);
        // Removes an anime from the user's animelist
        Task<bool> RemoveAnimeFromUserAnimelistAsync(string? animeId, string? userId);
        // Checks if an anime is in the user's animelist
        Task<bool> IsAnimeInUserAnimelist(string? userId, string? animeId);
    }

}
