namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Review;

    public interface IReviewService
    {
        Task<ReviewPageViewModel> GetAllReviewsPagedAsync(int page, int pageSize);
    }
}
