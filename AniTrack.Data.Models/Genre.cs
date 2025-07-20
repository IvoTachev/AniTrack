namespace AniTrack.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AnimeGenre> AnimesGenre { get; set; } = new List<AnimeGenre>();
    }
}
