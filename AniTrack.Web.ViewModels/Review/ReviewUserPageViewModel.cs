namespace AniTrack.Web.ViewModels.Review
{
    public class ReviewUserPageViewModel
    {
        public IEnumerable<AnimeReviewViewModel> Reviews { get; set; } = new List<AnimeReviewViewModel>();
        public string AuthorName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
