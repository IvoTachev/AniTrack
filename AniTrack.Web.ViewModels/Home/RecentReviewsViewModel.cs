namespace AniTrack.Web.ViewModels.Home
{
    public class RecentReviewsViewModel
    {
        public string AuthorName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string AnimeId { get; set; } = null!;
        public string AnimeTitle { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsRecommended { get; set; }
    }
}
