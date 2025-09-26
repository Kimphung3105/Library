using System.Text.Json;
using api;
using api.Services;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("the app options are: " + JsonSerializer.Serialize(appOptions));

builder.Services.AddScoped<ILibraryService, LibraryService>();

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});
builder.Services.AddControllers();
    builder.Services.AddOpenApiDocument();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    
    builder.Services.AddCors();
    
    var app = builder.Build();
    
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


