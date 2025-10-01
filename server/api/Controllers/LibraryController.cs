using api.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _service;

    public LibraryController(ILibraryService service)
    {
        _service = service;
    }

    [HttpGet("GetBooks")]
    public async Task<ActionResult<List<BookDto>>> GetBooks()
    {
        var books = await _service.GetAllBooks();

        var dtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Pages = b.Pages,
            Genre = b.Genre,                      
            Author = b.Author                      
        }).ToList();

        return Ok(dtos);
    }
}