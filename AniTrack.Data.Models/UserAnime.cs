namespace AniTrack.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Represents User Watchlist relation in database")]
    public class UserAnime
    {
        [Comment("Foreign Key reference to AspNetUser")]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        [Comment("Foreign Key reference to Anime")]
        public int AnimeId { get; set; }
        public virtual Anime Anime { get; set; } = null!;
        [Comment("Indicates whether this UserAnime record is deleted (soft delete).")]
        public bool IsDeleted { get; set; }
    }
}
