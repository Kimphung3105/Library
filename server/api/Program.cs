using System.Text.Json;
using api;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine(JsonSerializer.Serialize(appOptions));

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);

    var app = builder.Build();

    app.MapGet("/", (
        [FromServices] IOptionsMonitor<AppOptions> appOptions,
        [FromServices] MyDbContext dbContext) =>
    {
        var myTodo = new Todo()
        {
            Description = "test",
            Title = "test title",
            Id = Guid.NewGuid().ToString(),
            Isdone = false,
            Priority = 5
        };

        dbContext.Todos.Add(myTodo);
        dbContext.SaveChanges();
        var objects = dbContext.Todos.ToList();
        return objects;
    });

    app.Run();
});

