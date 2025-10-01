using api.Dtos;
using Dataaccess;
using efscaffold.Entities;

public partial interface ILibraryService
{
    Task<List<Library>> GetAllLibraries();
    Task<List<BookDto>> GetAllBooks();
    Task SeedBooks();
}