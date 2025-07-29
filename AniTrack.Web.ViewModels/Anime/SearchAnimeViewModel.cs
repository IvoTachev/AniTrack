
using AniTrack.Data.Models;

namespace AniTrack.Web.ViewModels.Anime
{
    public class SearchAnimeViewModel
    {
        public string? SearchTerm { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
