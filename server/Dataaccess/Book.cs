public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int Pages { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Genreid { get; set; }

    Genre = b.Genre != null ? b.Genre.Name : null,
    Authors = b.Authors != null ? b.Authors.Select(a => a.Name).ToList() : new List<string>()
}