namespace AniTrack.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<RecentReviewsViewModel> RecentReviews { get; set; } = null!;

        public List<RecommendedAnimeViewModel> RecommendedAnimes { get; set; } = null!;
    }
}
