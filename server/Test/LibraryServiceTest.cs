using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using api.Services;
using api.Dtos;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class LibraryServiceTests
{
  
    private MyDbContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new MyDbContext(options);
    }

    [Fact]
    public async Task CreateLibrary_AddsLibraryToDb()
    {
        var db = GetInMemoryDb();
        var service = new LibraryService(db);

        var dto = new CreateLibraryDto
        {
            Title = "Test Library",
            Description = "Test Description",
            Priority = 1
        };

        var result = await service.CreateLibrary(dto);
        var library = result.Value; 

        var libraries = await db.Libraries.ToListAsync();
        Assert.Single(libraries);
        Assert.Equal(dto.Title, library.Title);
        Assert.Equal(dto.Description, library.Description);
        Assert.False(library.Isdone);
    }

    [Fact]
    public async Task GetAllLibraries_ReturnsAllLibraries()
    {
        var db = GetInMemoryDb();
        var service = new LibraryService(db);

        db.Libraries.AddRange(new List<Library>
        {
            new Library { Id = Guid.NewGuid().ToString(), Title="A", Description="Desc A", Priority=1, Isdone=false },
            new Library { Id = Guid.NewGuid().ToString(), Title="B", Description="Desc B", Priority=2, Isdone=true }
        });
        await db.SaveChangesAsync();

        var result = await service.GetAllLibraries();
        var libraries = result.Value; 

        Assert.Equal(2, libraries.Count);
        Assert.Contains(libraries, l => l.Title == "A");
        Assert.Contains(libraries, l => l.Title == "B");
    }

    [Fact]
    public async Task ToggleLibrary_TogglesIsdone()
    {
        var db = GetInMemoryDb();
        var service = new LibraryService(db);

        var library = new Library
        {
            Id = Guid.NewGuid().ToString(),
            Title = "Test",
            Description = "Desc",
            Priority = 1,
            Isdone = false
        };
        db.Libraries.Add(library);
        await db.SaveChangesAsync();

        var result = await service.ToggleLibrary(library);
        var toggled = result.Value; 

        Assert.True(toggled.Isdone);

        var dbLibrary = await db.Libraries.FirstAsync();
        Assert.True(dbLibrary.Isdone);
    }

    [Fact]
    public async Task ToggleLibrary_ThrowsIfNotFound()
    {
        var db = GetInMemoryDb();
        var service = new LibraryService(db);

        var library = new Library
        {
            Id = "nonexistent",
            Title = "X",
            Description = "Y",
            Priority = 1,
            Isdone = false
        };

        await Assert.ThrowsAsync<ValidationException>(() => service.ToggleLibrary(library));
    }

    [Fact]
    public async Task GetAllLibraries_ReturnsEmptyList_WhenNoLibraries()
    {
        var db = GetInMemoryDb();
        var service = new LibraryService(db);

        var result = await service.GetAllLibraries();
        var libraries = result.Value;

        Assert.Empty(libraries);
    }
}
