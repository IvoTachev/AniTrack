namespace AniTrack.Web.ViewModels.Review
{
    public class AnimeReviewViewModel
    {
        public string AnimeId { get; set; } = null!;
        public string AnimeTitle { get; set; } = null!;
        public string AnimeImageUrl { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool isAnimeRecommended { get; set; }
    }
}
