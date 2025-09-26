using api.Dtos;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Services;

public interface ILibraryService
{
    Task<Library> CreateLibrary(CreateLibraryDto dto)
    {
        throw new NotImplementedException();
    }

    
    Task<ActionResult<List<Library>>> GetAllLibraries();
    Task<ActionResult<Library>> ToggleLibrary(Library library);
}