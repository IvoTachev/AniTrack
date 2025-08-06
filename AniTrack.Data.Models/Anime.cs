namespace AniTrack.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Anime series in the system")]
    public class Anime
    {
        [Comment("Unique identifier for the anime series")]
        public int Id { get; set; }
        [Comment("Title of the anime series")]
        public string Title { get; set; } = null!;
        [Comment("Number of episodes of the anime series")]
        public int Episodes { get; set; }
        [Comment("The date when the anime series first aired")]
        public DateOnly AirDate { get; set; }
        [Comment("The date when the anime series ended, if ended")]
        public DateOnly? EndDate { get; set; }
        [Comment("Synopsis or description of the anime series")]
        public string Synopsis { get; set; } = null!;
        [Comment("URL of the anime series poster")]
        public string ImageUrl { get; set; } = null!;
        [Comment("Indicates whether the anime series is deleted")]
        public bool IsDeleted { get; set; }


        // Navigation property for genres
        public ICollection<AnimeGenre> AnimeGenres { get; set; } = new List<AnimeGenre>();
        // Navigation property for user watchlists
        public virtual ICollection<UserAnime> UserWatchlists { get; set; } = new List<UserAnime>();
        // Navigation property for reviews
        public virtual ICollection<AnimeReview> Reviews { get; set; } = new List<AnimeReview>();
    }
}
