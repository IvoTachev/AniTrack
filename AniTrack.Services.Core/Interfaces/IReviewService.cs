namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Review;

    public interface IReviewService
    {
        // Gets a view model for the anime review index page and uses pagination
        Task<ReviewPageViewModel> GetAllReviewsPagedAsync(int page, int pageSize);
        // Gets a view model for the users anime review page by the user's username and uses pagination
        Task<ReviewUserPageViewModel> GetUserReviewsPagedAsync(string username, int page, int pageSize);
        // Gets a view model by the anime's id for the write a review form 
        Task<ReviewWriteViewModel> GetWriteFormAsync (string animeId);
        // Writes a review for an anime by the user's id
        Task WriteReviewAsync(ReviewWriteViewModel inputModel, string userId);
        // Gets a view model for the edit review form by the anime's id and the author's id
        Task<ReviewEditViewModel?> GetEditFormAsync(string animeId,string authordId);
        // Edits a review for an anime
        Task<bool> EditReviewAsync(ReviewEditViewModel inputModel);
        // Gets a view model for the delete review confirmation page by the anime's id and the author's name
        Task<ReviewDeleteViewModel?> GetReviewDetailsForDeleteAsync(string animeId, string authorName);
        // Soft deletes a review for an anime by the anime's id and the author's name
        Task<bool> DeleteReviewAsync(string animeId, string authorName);
    }
}
