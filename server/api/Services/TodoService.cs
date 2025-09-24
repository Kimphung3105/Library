using System.ComponentModel.DataAnnotations;
using api.Dtos;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;

namespace api.Services;

public class TodoService(MyDbContext dbContext) : ITodoService
{
    public async Task<Todo> CreateTodo(CreateTodoDto dto)
    {
        var myTodo = new Todo()
        {
            Description = dto.description,
            Title = dto.title,
            Id = Guid.NewGuid().ToString(),
            Isdone = false,
            Priority = dto.priority
        };
        
        dbContext.Todos.Add(myTodo);
        dbContext.SaveChanges();
        return myTodo;
    }

    public async Task<List<Todo>> GetAllTodos()
    {
        return dbContext.Todos.ToList();
    }

    public async Task<Todo> ToggleTodo(Todo todo)
    {
        var currentObject = dbContext.Todos.FirstOrDefault(todo => todo.Id == todo.Id) ??
                            throw new ValidationException("Could not ne found");
        currentObject.Isdone = !currentObject.Isdone;
        dbContext.Todos.Update(currentObject);
        dbContext.SaveChanges();
        return currentObject;
    }
}