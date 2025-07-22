using Microsoft.EntityFrameworkCore;

namespace AniTrack.Data.Models
{
    public class AnimeGenre
    {
        [Comment("The unique identifier for the Anime associated with this Genre.")]
        public int AnimeId { get; set; }
        [Comment("The Anime associated with this Genre.")]
        public Anime Anime { get; set; } = null!;
        [Comment("The unique identifier for the Genre associated with this Anime.")]
        public int GenreId { get; set; }
        [Comment("The Genre associated with this Anime.")]
        public Genre Genre { get; set; } = null!;
        [Comment("Indicates whether this AnimeGenre record is deleted (soft delete).")]
        public bool IsDeleted { get; set; }
    }
}
