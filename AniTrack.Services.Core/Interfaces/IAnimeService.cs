namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Anime;

    public interface IAnimeService
    {
        // Gets a view model including all animes ordered by how many users have them in their animelist
        Task<IEnumerable<TopAnimesViewModel>> GetTopAnimesAsync();
        // Gets a view model for the add anime form
        Task<AddAnimeFormModel> GetAddAnimeViewModelAsync();
        // Adds an anime to the database
        Task AddAnimeAsync(AddAnimeFormModel inputModel);
        // Gets a view model for the anime details page by the anime's id
        Task<AnimeDetailsViewModel?> GetAnimeDetailsAsync(string? id);
        // Gets a view model for the anime details page by the anime's id including reviews for this anime
        Task<AnimeDetailsWithReviewViewModel> GetAnimeDetailsWithReviewViewModelAsync(string? id);
        // Gets a view model for the edit anime form by the anime's id
        Task<EditAnimeFormModel?> GetAnimeDetailsByIdAsync(string? id);
        // Edits an anime in the database
        Task<bool> EditAnimeAsync(EditAnimeFormModel inputModel);
        // Gets a view model for the delete anime confirmation page by the anime's id
        Task<DeleteAnimeViewModel?> GetAnimeDetailsForDeleteByIdAsync(string? id);
        // Soft deletes an anime by its id
        Task<bool> SoftDeleteAnimeAsync(string? id);
        // Hard deletes an anime by its id
        Task<bool> HardDeleteAnimeAsync(string? id);
        // Gets a view model for the restore anime confirmation page by the anime's id
        Task<DeleteAnimeViewModel?> GetAnimeDetailsForRestoreByIdAsync(string? id);
        // Restores a soft-deleted anime by its id
        Task<bool> RestoreAnimeAsync(string? id);
        // Gets a view model for the search anime page by a search term
        Task<SearchAnimeViewModel> SearchAnimeByWordAsync(string? searchTerm);
        // Uses the GetTopAnimesAsync method and adds pagination
        Task<AnimePageViewModel> GetPagedAnimesAsync(int page, int pageSize);
    }
}
