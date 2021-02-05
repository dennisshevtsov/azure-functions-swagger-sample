// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;

  /// <summary>Provides a simple API to operate within the TODO list domain.</summary>
  public sealed class TodoService : ITodoService
  {
    private readonly IDictionary<Guid, TodoListDocument> _todoListDictionary;
    private readonly IDictionary<Guid, IList<TodoListTaskDocument>> _todoListTaskDictionary;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Services.TodoService"/> class.</summary>
    public TodoService()
    {
      _todoListDictionary = new Dictionary<Guid, TodoListDocument>();
      _todoListTaskDictionary = new Dictionary<Guid, IList<TodoListTaskDocument>>();
    }

    /// <summary>Gets a TODO list by its ID.</summary>
    /// <param name="todoListId"></param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<GetTodoListResponseDto> GetTodoListAsync(Guid todoListId, CancellationToken cancellationToken)
    {
      GetTodoListResponseDto reponseDto = null;

      if (_todoListDictionary.TryGetValue(todoListId, out var todoListDocument))
      {
        reponseDto = new GetTodoListResponseDto
        {
          TodoListId = todoListDocument.TodoListId,
          Title = todoListDocument.Title,
          Description = todoListDocument.Description,
          Tasks = todoListDocument.Tasks.Select(document => new GetTodoListTaskResponseDto
          {
            TaskId = document.TaskId,
            Title = document.Title,
            Description = document.Description,
            Deadline = document.Deadline,
            Completed = document.Completed,
          }),
        };
      }

      return Task.FromResult(reponseDto);
    }

    /// <summary>Gets a collection of TODO lists.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken)
    {
      var responseDto = new GetTodoListsResponseDto
      {
        Items = _todoListDictionary.Select(document => new GetTodoListsItemResponseDto
        {
          TodoListId = document.Key,
          Title = document.Value.Title,
        }),
      };

      return Task.FromResult(responseDto);
    }

    /// <summary>Creates a TODO list.</summary>
    /// <param name="command">An object that represents data to create a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<CreateTodoListResponseDto> CreateTodoListAsync(CreateTodoListRequestDto command, CancellationToken cancellationToken)
    {
      var todoListId = Guid.NewGuid();
      var totoListTasks = new List<TodoListTaskDocument>();
      var todoListDocument = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = command.Title,
        Description = command.Description,
        Tasks = totoListTasks,
      };

      _todoListDictionary.Add(todoListId, todoListDocument);
      _todoListTaskDictionary.Add(todoListId, totoListTasks);

      var responseDto = new CreateTodoListResponseDto
      {
        TodoListId = todoListId,
      };

      return Task.FromResult(responseDto);
    }

    /// <summary>Updates a TODO list.</summary>
    /// <param name="command">An object that represents data to update a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task UpdateTodoListAsync(UpdateTodoListRequestDto command, CancellationToken cancellationToken)
    {
      if (_todoListDictionary.TryGetValue(command.TodoListId, out var todoListDocument))
      {
        todoListDocument.Title = command.Title;
        todoListDocument.Description = command.Description;
      }

      throw new Exception();
    }

    /// <summary>Creates a TODO list task.</summary>
    /// <param name="command">An object that represents data to create a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      if (!_todoListTaskDictionary.TryGetValue(command.TodoListId, out var todoListTaskDocuments))
      {
        throw new Exception();
      }

      var todoListTaskDocument = new TodoListTaskDocument
      {
        TaskId = Guid.NewGuid(),
        Title = command.Title,
        Description = command.Description,
        Deadline = command.Deadline,
      };

      todoListTaskDocuments.Add(todoListTaskDocument);

      var responseDto = new CreateTodoListTaskResponseDto
      {
        TaskId = todoListTaskDocument.TaskId,
      };

      return Task.FromResult(responseDto);
    }

    /// <summary>Completes a TODO list task.</summary>
    /// <param name="command">An object that represents data to complete a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      if (_todoListTaskDictionary.TryGetValue(command.TodoListId, out var todoListTaskDocuments))
      {
        var todoListTaskDocument = todoListTaskDocuments.FirstOrDefault(document => document.TaskId == command.TaskId);

        if (todoListTaskDocument != null)
        {
          todoListTaskDocument.Completed = true;

        }
      }

      return Task.CompletedTask;
    }
  }
}
