using System.ComponentModel.DataAnnotations;
using api.Dtos;
using Dataaccess;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class LibraryService : ILibraryService
{
    private readonly MyDbContext _context;

    public LibraryService(MyDbContext context)
    {
        _context = context;
    }

    
    public async Task<ActionResult<LibraryDto>> CreateLibrary(CreateLibraryDto dto)
    {
        var library = new Library
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
        };

        await _context.Libraries.AddAsync(library);
        await _context.SaveChangesAsync();

        return new LibraryDto
        {
            Id = library.Id,
            Title = library.Title,
            Description = library.Description,
            Priority = library.Priority,
            CreatedAt = library.CreatedAt,
            Books = new List<BookDto>() 
        };
    }

    
    public async Task<List<Library>> GetAllLibraries()

    {
        var libraries = await _context.Libraries.ToListAsync();
        var libraryDtos = new List<LibraryDto>();

        foreach (var lib in libraries)
        {
            var books = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Authors)
                .ToListAsync();

            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Pages = b.Pages,
                Genre = b.Genre?.Name,
                Author = b.Authors?.Select(a => a.Name).ToList()
            }).ToList();

            libraryDtos.Add(new LibraryDto
            {
                Id = lib.Id,
                Title = lib.Title,
                Description = lib.Description,
                Priority = lib.Priority,
                CreatedAt = lib.CreatedAt,
                Books = bookDtos
            });
        }

        return await _context.Libraries.ToListAsync();
    }

    
    public async Task<List<BookDto>> GetAllBooks()
    {
        var books = await _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Authors)
            .ToListAsync();

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Pages = b.Pages,
            Genre = b.Genre?.Name,
            Author = b.Authors?.Select(a => a.Name).ToList()
        }).ToList();
    }

    
    public async Task<ActionResult<LibraryDto>> ToggleLibrary(LibraryDto libraryDto)
    {
        var library = await _context.Libraries
            .FirstOrDefaultAsync(l => l.Id == libraryDto.Id)
            ?? throw new ValidationException("Library not found");

        library.Priority = library.Priority == 0 ? 1 : 0;

        _context.Libraries.Update(library);
        await _context.SaveChangesAsync();

        return new LibraryDto
        {
            Id = library.Id,
            Title = library.Title,
            Description = library.Description,
            Priority = library.Priority,
            CreatedAt = library.CreatedAt,
            Books = new List<BookDto>() 
        };
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
