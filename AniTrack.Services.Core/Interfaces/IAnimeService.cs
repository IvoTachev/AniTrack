namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Anime;

    public interface IAnimeService
    {
        Task<IEnumerable<TopAnimesViewModel>> GetTopAnimesAsync();

        Task AddAnimeAsync(AddAnimeFormModel inputModel);

        Task<AnimeDetailsViewModel?> GetAnimeDetailsAsync(string? id);

        Task<AnimeDetailsWithReviewViewModel> GetAnimeDetailsWithReviewViewModelAsync(string? id);

        Task<EditAnimeFormModel?> GetAnimeDetailsByIdAsync(string? id);

        Task<bool> EditAnimeAsync(EditAnimeFormModel inputModel);

        Task<DeleteAnimeViewModel?> GetAnimeDetailsForDeleteByIdAsync(string? id);
        Task<bool> SoftDeleteAnimeAsync(string? id);

        Task<bool> HardDeleteAnimeAsync(string? id);

        Task<DeleteAnimeViewModel?> GetAnimeDetailsForRestoreByIdAsync(string? id);
        Task<bool> RestoreAnimeAsync(string? id);

        Task<SearchAnimeViewModel> SearchAnimeByWordAsync(string? searchTerm);

        Task<AnimePageViewModel> GetPagedAnimesAsync(int page, int pageSize);
    }
}
