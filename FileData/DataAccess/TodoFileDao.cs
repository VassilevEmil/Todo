﻿using Domain.Contracts;
using Domain.Models;

namespace FileData.DataAccess;

public class TodoFileDao : ITodoHome
{

    private FileContext fileContext;


    public TodoFileDao(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }

    public async Task<ICollection<Todo>> GetAsync()
    {
        ICollection<Todo> todos = fileContext.Todos;
        return todos;
    }

    public async Task<Todo> GetByIdAsync(int id)
    {
        return fileContext.Todos.First(t => t.Id == id);
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        int largestId = fileContext.Todos.Max(t => t.Id);
        int nextId = largestId + 1;
        todo.Id = nextId;
        fileContext.Todos.Add(todo);
        fileContext.SaveChanges();
        return todo;
    }

    public async Task DeleteAsync(int id)
    {
        Todo delete = fileContext.Todos.First(t => t.Id == id);
        fileContext.Todos.Remove(delete);
        fileContext.SaveChanges();
    }

    public Task UpdateAsync(Todo todo)
    {
        Todo toUpdate = fileContext.Todos.First(t => t.Id == todo.Id);
        toUpdate.IsCompleted = todo.IsCompleted;
        toUpdate.OwnerId = todo.OwnerId;
        toUpdate.Title = todo.Title;
        fileContext.SaveChanges();
        return Task.CompletedTask;

    }
}