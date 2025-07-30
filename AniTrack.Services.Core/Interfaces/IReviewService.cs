namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Review;

    public interface IReviewService
    {

        Task<ReviewPageViewModel> GetAllReviewsPagedAsync(int page, int pageSize);

        Task<ReviewPageViewModel> GetUserReviewsPagedAsync(string username, int page, int pageSize);

        Task<ReviewWriteViewModel> GetWriteFormAsync (string animeId);

        Task WriteReviewAsync(ReviewWriteViewModel inputModel, string userId);
    }
}
