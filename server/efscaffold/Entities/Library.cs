using System.ComponentModel.DataAnnotations.Schema;
using Dataaccess;

namespace efscaffold.Entities;

[Table("library", Schema = "library")]
public class Library
{
    [Column("id")]
    public string Id { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("priority")]
    public int Priority { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}