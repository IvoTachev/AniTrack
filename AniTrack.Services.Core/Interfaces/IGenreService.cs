namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Genre;
    using AniTrack.Web.ViewModels.Home;

    public interface IGenreService
    {
        // Gets a view model for when you filter animesb by genre
        Task<GenreViewModel> GetAnimesDetailsByGenreNameAsync(string genreName);
        // Adds a new genre to the database
        Task<bool> AddGenreAsync(string genreName);
        // Gets a view model for the delete genre confirmation page by the genre's name
        Task<DeleteGenreViewModel?> GetAllGenreDetailsForDeleteAsync(string? selectedGenre);
        // Soft deletes a genre by its name
        Task<bool> DeleteGenreByNameAsync(string selectedGenre);
        // Gets a view model for the restore genre confirmation page by the genre's name
        Task<RestoreGenreViewModel?> GetAllGenreDetailsForRestoreAsync(string? selectedGenre);
        // Restores a soft-deleted genre by its name
        Task<bool> RestoreGenreByNameAsync(string selectedGenre);

    }
}
