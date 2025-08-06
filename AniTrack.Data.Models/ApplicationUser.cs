namespace AniTrack.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        // Navigation property for user watchlists
        public virtual ICollection<UserAnime> Animelist { get; set; } = new List<UserAnime>();
        // Navigation property for anime reviews
        public virtual ICollection<AnimeReview> Reviews { get; set; } = new List<AnimeReview>();
    }
}
