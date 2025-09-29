using api.Services;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class LibraryController(ILibraryService libraryService) : ControllerBase
{
    [Route(nameof(GetAllLibraries))]
    [HttpGet]
    public async Task<ActionResult<List<Library>>> GetAllLibraries()
    {
        var libaries = await libraryService.GetAllLibraries();
        return libaries;
    }
    [Route(nameof(CreateLibrary))]
    [HttpPost]
    public async Task<ActionResult<Library>> CreateLibrary([FromBody]CreateLibraryDto dto)
    {
        var result = await libraryService.CreateLibrary(dto);
        return result;
    }

    [Route(nameof(ToggleDone))]
    [HttpPut]
    public async Task<ActionResult<Library>> ToggleDone([FromBody] Library t)
    {
        var result = await libraryService.ToggleLibrary(t);
        return result;
    }
}

