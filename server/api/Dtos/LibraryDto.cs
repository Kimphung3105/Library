using api.Dtos;

public class LibraryDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Priority { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<BookDto> Books { get; set; } = new();
}