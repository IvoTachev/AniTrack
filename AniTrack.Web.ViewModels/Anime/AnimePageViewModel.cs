namespace AniTrack.Web.ViewModels.Anime
{ 
    public class AnimePageViewModel
    {
        public IEnumerable<TopAnimesViewModel> Animes { get; set; } = Enumerable.Empty<TopAnimesViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}