
namespace efscaffold.Entities;

public partial class Library
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Priority { get; set; }

    public bool Isdone { get; set; }
}
