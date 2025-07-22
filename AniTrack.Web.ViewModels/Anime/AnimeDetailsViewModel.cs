namespace AniTrack.Web.ViewModels.Anime
{
    using AniTrack.Data.Models;
    public class AnimeDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string AirDate { get; set; } = null!;
        public string? EndDate { get; set; }
        public string Synopsis { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Episodes { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
