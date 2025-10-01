namespace api.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string? Genre { get; set; }
       public object Author { get; set; }
    }
}