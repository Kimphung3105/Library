using api.Dtos;
using Dataaccess;
using efscaffold.Entities;
using Microsoft.EntityFrameworkCore;

public class LibraryService : ILibraryService
{
    private readonly MyDbContext _context;

    public LibraryService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Library>> GetAllLibraries()
    {
        return await _context.Libraries.ToListAsync();
    }

    public async Task<List<BookDto>> GetAllBooks()
    {
        var books = await _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Authors)
            .ToListAsync();

        var dtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Pages = b.Pages,
            Genre = b.Genre?.Name,
            Author = b.Authors?.Select(a => a.Name).ToList()
        }).ToList();

        return dtos;
    }

    public async Task SeedBooks()
    {
        if (!await _context.Books.AnyAsync())
        {
            var genre = new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Fantasy"
            };

            var author = new Author
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe"
            };

            _context.Genres.Add(genre);
            _context.Authors.Add(author);

            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Debug Book 1",
                Pages = 100,
                Genre = genre,
                Authors = new List<Author> { author }
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}