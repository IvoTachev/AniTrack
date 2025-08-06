namespace AniTrack.Web.ViewModels.Anime
{
    using AniTrack.Data.Models;
    using AniTrack.Web.ViewModels.Review;
    public class AnimeDetailsWithReviewViewModel
   {
        public AnimeDetailsViewModel? AnimeDetails { get; set; } = new AnimeDetailsViewModel();

        public AnimeReviewViewModel? ReviewDetails { get; set; } = new AnimeReviewViewModel();

        public List<AnimeReview> AllReviews { get; set;} = new List<AnimeReview>();

        public int TotalReviewsCount { get; set; } 

        public int RecommendedReviewsCount { get; set; } 

    }
}
