public class JikanAnimeResponse
{
    public List<JikanAnimeDto> Data { get; set; } = new();
}

public class JikanAnimeDto
{
    public int MalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? Episodes { get; set; }
    public DateTime? AirDateFrom { get; set; }
    public DateTime? AirDateTo { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}