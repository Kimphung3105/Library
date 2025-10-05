namespace efscaffold.Entities;

public partial class Genre
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }

    // Navigation property
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}