namespace efscaffold.Entities;

public partial class Author
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }

   
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}