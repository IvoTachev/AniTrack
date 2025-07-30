namespace AniTrack.Services.Core.Interfaces
{
    using AniTrack.Web.ViewModels.Review;

    public interface IAnimeReviewService
    {
        Task<IEnumerable<ReviewPageViewModel>> GetAllReviewsAsync();
    }
}
