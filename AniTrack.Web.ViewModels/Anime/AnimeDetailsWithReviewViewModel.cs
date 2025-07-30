namespace AniTrack.Web.ViewModels.Anime
{
    using AniTrack.Web.ViewModels.Review;
    public class AnimeDetailsWithReviewViewModel
   {
        public AnimeDetailsViewModel? AnimeDetails = new AnimeDetailsViewModel();

        public AnimeReviewViewModel? ReviewDetails = new AnimeReviewViewModel();
   }
}
