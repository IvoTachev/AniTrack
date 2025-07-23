using AniTrack.Data.Models;

namespace AniTrack.Web.ViewModels.Animelist
{
    public class AnimelistViewModel
    {
        public string AnimeId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
        public int Episodes { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
