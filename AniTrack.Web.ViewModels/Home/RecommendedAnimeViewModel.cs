namespace AniTrack.Web.ViewModels.Home
{
    public class RecommendedAnimeViewModel
    {
        public string AnimeTitle { get; set; } = null!;

        public string AnimeId { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Episodes { get; set; }

        public string AirDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;
    }
}
