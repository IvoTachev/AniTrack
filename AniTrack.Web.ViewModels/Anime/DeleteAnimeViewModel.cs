
namespace AniTrack.Web.ViewModels.Anime
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteAnimeViewModel
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
    }
}
