namespace AniTrack.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    public class AnimeReview
    {
        [Comment("Unique identifier for the review author")]
        public string AuthorId { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
        [Comment("Unique identifier for the anime being reviewed")]
        public int AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;
        [Comment("Content of the review")]
        public string Content { get; set; } = null!;
        [Comment("Indicates whether the review is deleted")]
        public bool IsDeleted { get; set; }
        [Comment("Indicates whether the review recommends the anime")]
        public bool isAnimeRecommended { get; set; }
        [Comment("The date when the review was created")]
        public DateTime CreatedOn { get; set; }
    }
}
