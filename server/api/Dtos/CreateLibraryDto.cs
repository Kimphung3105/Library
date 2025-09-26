
namespace api.Dtos
{
    
}
public class CreateLibraryDto
{
    public string Description { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Priority { get; set; } 
    public bool IsDone { get; set; } = false;
    public string Id { get; set; } = null!;
}