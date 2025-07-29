namespace AniTrack.Web.ViewModels.Admin.Home
{
    using AniTrack.Data.Models;
    public class RestoreAnimesViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string AirDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Episodes { get; set; }
    }
}
