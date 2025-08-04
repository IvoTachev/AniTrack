namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Review;

    public interface IReviewService
    {

        Task<ReviewPageViewModel> GetAllReviewsPagedAsync(int page, int pageSize);

        Task<ReviewUserPageViewModel> GetUserReviewsPagedAsync(string username, int page, int pageSize);

        Task<ReviewWriteViewModel> GetWriteFormAsync (string animeId);

        Task WriteReviewAsync(ReviewWriteViewModel inputModel, string userId);

        Task<ReviewEditViewModel?> GetEditFormAsync(string animeId,string authordId);

        Task<bool> EditReviewAsync(ReviewEditViewModel inputModel);

        Task<ReviewDeleteViewModel?> GetReviewDetailsForDeleteAsync(string animeId, string authorName);
        Task<bool> DeleteReviewAsync(string animeId, string authorName);
    }
}
