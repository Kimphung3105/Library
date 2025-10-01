public class Genre
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}