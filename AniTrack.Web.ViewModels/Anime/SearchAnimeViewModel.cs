namespace AniTrack.Web.ViewModels.Anime
{
    using AniTrack.Data.Models;
    public class SearchAnimeViewModel
    {
        public string? SearchTerm { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public IEnumerable<Anime> SearchResults { get; set; } = new List<Anime>();
    }
}
