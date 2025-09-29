using System.ComponentModel.DataAnnotations;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Dataaccess;

namespace api.Services;

public class LibraryService(MyDbContext dbContext) : ILibraryService
{
    
    public async Task<ActionResult<Library>> CreateLibrary(CreateLibraryDto dto)
    {
        var myLibrary = new Library
        {
            Description = dto.Description,
            Title = dto.Title,
            Id = Guid.NewGuid().ToString(),
            Priority = dto.Priority,
        };

        await dbContext.Libraries.AddAsync(myLibrary);
        await dbContext.SaveChangesAsync();

        return myLibrary;
    }

    public async Task<ActionResult<List<Library>>> GetAllLibraries()
    {
        var libraries = await dbContext.Libraries.ToListAsync();
        return libraries;
    }

    public async Task<ActionResult<Library>> ToggleLibrary(Library library)
    {
        var currentObject = await dbContext.Libraries
                                .FirstOrDefaultAsync(l => l.Id == library.Id)
                            ?? throw new ValidationException("Could not be found");
        
        dbContext.Libraries.Update(currentObject);
        await dbContext.SaveChangesAsync();

        return currentObject;
    }
}