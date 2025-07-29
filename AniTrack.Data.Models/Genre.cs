namespace AniTrack.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Represents a Genre in the AniTrack database.")]
    public class Genre
    {
        [Comment("The unique identifier for the Genre.")]
        public int Id { get; set; }
        [Comment("The name of the Genre.")]
        public string Name { get; set; } = null!;
        public ICollection<AnimeGenre> AnimesGenre { get; set; } = new List<AnimeGenre>();
        [Comment("Indicates whether this Genre record is deleted (soft delete).")]
        public bool IsDeleted { get; set; }
    }
}
