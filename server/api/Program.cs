using api;
using api.Services;
using Dataaccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(appOptions.DbConnectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
);
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(appOptions.DbConnectionString, b =>
        b.MigrationsAssembly("api")));

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();
    await libraryService.SeedBooks();

    var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    if (!await db.Books.AnyAsync())
    {
        await db.SaveChangesAsync();
        Console.WriteLine("Debug books seeded.");
    }

    var booksAfterSeed = await db.Books.ToListAsync();
    Console.WriteLine($"Books fetched from DB: {booksAfterSeed.Count}");
    foreach (var b in booksAfterSeed)
    {
        Console.WriteLine("Book: " + b.Title + ", Pages: " + b.Pages);

    }

    app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    app.MapControllers();
    app.UseOpenApi();
    app.UseSwaggerUi();
    await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");
    app.Run();
}

public partial interface ILibraryService
{
}