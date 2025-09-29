using System.Text.Json;
using api;
using api.Services;
using Dataaccess;
using efscaffold.Entities;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("the app options are: " + JsonSerializer.Serialize(appOptions));
Console.WriteLine("DB Connection: " + appOptions.DbConnectionString);

builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(
        appOptions.DbConnectionString
        )
);

builder.Services.AddControllers();
    builder.Services.AddOpenApiDocument();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    
    builder.Services.AddCors();
    
    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        try
        {
            var canConnect = await db.Database.CanConnectAsync();
            Console.WriteLine($"Kan forbinde til databasen: {canConnect}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(" DB-forbindelse fejlede: " + ex.Message);
        }
        var testLibrary = new Library
        {
            Id = Guid.NewGuid().ToString(),
            Title = "Test Library",
            Description = "This is a test",
            Priority = 1,
        };


        db.Libraries.Add(testLibrary);
        await db.SaveChangesAsync();

        
        var libraries = await db.Libraries.ToListAsync();
        Console.WriteLine($"Libraries in DB: {libraries.Count}");
    }

    app.UseExceptionHandler();
    
    app.UseCors(config => config
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .SetIsOriginAllowed (x => true));
    
    app.MapControllers();
    
    app.UseOpenApi();
    app.UseSwaggerUi();
    await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");
    
    
    app.Run();

    


