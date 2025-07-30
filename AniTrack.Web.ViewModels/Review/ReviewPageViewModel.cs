namespace AniTrack.Web.ViewModels.Review
{
    public class ReviewPageViewModel
    {
        public IEnumerable<AnimeReviewViewModel> Reviews { get; set; } = new List<AnimeReviewViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

