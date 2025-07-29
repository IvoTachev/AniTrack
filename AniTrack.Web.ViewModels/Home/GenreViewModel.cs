namespace AniTrack.Web.ViewModels.Home
{
    using AniTrack.Data.Models;
    public  class GenreViewModel
    {
        public Genre Genre { get; set; } = null!;

        public List<GenreAnimeViewModel> Animes { get; set; } = new List<GenreAnimeViewModel>();
    }

    public class GenreAnimeViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string AirDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string Episodes { get; set; } = null!;
    }
}
